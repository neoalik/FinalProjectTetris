using System;
using System.IO;
using Tetris.Data;
using Tetris.Gui;
using Tetris.MenuGui;

namespace Tetris.Game
{
    class GameController : GuiObject
    {
        private Random rnd = new Random();

        public event Action<Menu, MenuOperations> OnMenuLeave;//action for menu control

        private GameScreen gameScreen;

        private Player player;

        private GridData gridData;

        private int _speedGame = 60;

        private int _level = 0;

        private int _currentTetromine = -1;
        private int _nextTetromine = -1;

        private int _bestScore = 0;
        const string FILE_SCORE_NAME = "score.txt";

        public GameController(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }

        /// <summary>
        /// Start Game with initialization
        /// </summary>
        public void StartGame()
        {
            //init game
            InitGame();

            // render loop
            StartGameLoop();
        }

        /// <summary>
        /// Game initialization
        /// </summary>
        private void InitGame()
        {
            _bestScore = ReadBestScoreFromFile();

            gameScreen = new GameScreen(X, Y, Width, Height, '░');
            gameScreen.SetBestScore(_bestScore);

            player = new Player();

            gridData = new GridData(gameScreen.GetTetrisBoardX, gameScreen.GetTetrisBoardY);

            _currentTetromine = GetIndexRandomShape();
            _nextTetromine = GetIndexRandomShape();
            gameScreen.NextShape(_nextTetromine, _level);
        }

        private int ReadBestScoreFromFile()
        {
            using (var src = File.OpenText(FILE_SCORE_NAME))
            {
                string score = "";
                while ((score = src.ReadLine()) != null)
                {
                    int number;

                    bool success = Int32.TryParse(score, out number);
                    return success ? number : 0;
                }
            }

            return 0;
        }

        private void SaveNewBestScoreToFile(int score)
        {
            System.IO.File.WriteAllText(FILE_SCORE_NAME, score.ToString());
        }

        /// <summary>
        /// Reseting game for reply
        /// </summary>
        public void ResetGame()
        {
            _speedGame = 60;
            _level = 0;

            InitGame();

            StartGameLoop();
        }

        //private DateTime _lastPressTime = DateTime.MinValue;

        /// <summary>
        /// Game loop
        /// </summary>
        private void StartGameLoop()
        {
            bool needToRender = true;
            int _speedCounter = 0;

            while (needToRender)
            {
                while (!Console.KeyAvailable)
                {
                    System.Threading.Thread.Sleep(5);

                    if (_speedCounter < _speedGame)
                    {
                        _speedCounter++;
                    }
                    else
                    {
                        if (IsGameOver()) 
                        {
                            needToRender = false;

                            GameOverMenuCall();

                            break;
                        }

                        ShapeMoveDown();
                        Render();

                        _speedCounter = 0;
                    }
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Escape:
                        needToRender = false;
                        OnMenuLeave?.Invoke(null, MenuOperations.GotoGameOverMenu);
                        break;
                    case ConsoleKey.RightArrow:
                        ShapeMoveRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        ShapeMoveLeft();
                        break;
                    case ConsoleKey.DownArrow:
                        ShapeMoveDown();
                        break;
                    case ConsoleKey.Spacebar:
                        //Todo: rotate shape tetris
                        ShapeRotate();
                        break;
                    default:
                        break;
                }
            }
        }

        private void GameOverMenuCall()
        {
            Console.Clear();
            GameOverMenu gameOverMenu = new GameOverMenu(X, Y, Width, Height);
            gameOverMenu.TitleMenu.Label = $"Game over. Best score {player.GetPlayerScore}";

            gameOverMenu.OnMenuLeave += (sender, args) =>
            {
                OnMenuLeave?.Invoke(gameOverMenu, args);
            };

            gameOverMenu.Show();
        }

        private void ShapeMoveDown()
        {
            gameScreen.GetShape(_currentTetromine, _level);

            if (!gameScreen.ShapeLanded && gameScreen.CanShapeMove(MoveDirections.DOWN) && CheckGridData(gameScreen.GetShapeObjectX, gameScreen.GetShapeObjectY + 1, gameScreen.GetShapeObjectData/*, MoveDirections.DOWN*/))
            {
                gameScreen.ShapeMoveDown();
            }
            else
            {
                gameScreen.SetShapeLanded(true);

                FillGridData(gameScreen.GetShapeObjectX, gameScreen.GetShapeObjectY, gameScreen.GetShapeObjectData, gameScreen.GetShapeObjectColor);

                CheckTetrisGridData();

                _currentTetromine = _nextTetromine;
                _nextTetromine = GetIndexRandomShape();
                gameScreen.NextShape(_nextTetromine, _level);

                //shape is landing and set null 
                gameScreen.SetShapeLanded(false);
            }
        }

        private bool IsGameOver()
        {
            var _data = gridData.GetGridData;

            for(int j = 0; j < _data.GetLength(1); j++)
            {
                if(_data[0, j] != 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckTetrisGridData()
        {
            
            if(gridData.CheckGridData() != null)
            {
                gameScreen.ReDrawingTetrisGrid(gridData.GetGridData);

                player.AddScore(gridData.LastCombo);
                gameScreen.SetScore(player.GetPlayerScore);

                CheckNewBestScore(player.GetPlayerScore);

                gameScreen.SetLineCounter(player.GetTotalIncreseLines);
                SetLevelChange(player.GetPlayerLevel);
            }
        }

        private void CheckNewBestScore(int score)
        {
            if(score > _bestScore)
            {
                //have new record and need to save
                _bestScore = score;

                gameScreen.SetBestScore(_bestScore);

                SaveNewBestScoreToFile(_bestScore);

                gameScreen.SetAttentionHaveNewBestScore();
            }
        }

        private void SetLevelChange(int level)
        {
            if (_level != level)
            {
                _level = level;
                _speedGame -= 2;
                gameScreen.SetLevel(level);
            }
        }

        private void FillGridData(int x, int y, int[,] data, int color)
        {
            if (data != null)
            {
                gridData.Fill(x, y, data, color);
            }
        }

        private bool CheckGridData(int x, int y, int[,] data)
        {
            return gridData.IsSpaceInGrid(x, y, data);
        }

        private void ShapeMoveRight()
        {
            if (!gameScreen.ShapeLanded 
                && gameScreen.CanShapeMove(MoveDirections.RIGHT) 
                && CheckGridData(gameScreen.GetShapeObjectX + 2, gameScreen.GetShapeObjectY, gameScreen.GetShapeObjectData))
            {
                gameScreen.ShapeMoveRight();
            }
        }

        private void ShapeMoveLeft()
        {
            //Todo: check colision

            if (!gameScreen.ShapeLanded 
                && gameScreen.CanShapeMove(MoveDirections.LEFT) 
                && CheckGridData(gameScreen.GetShapeObjectX - 2, gameScreen.GetShapeObjectY, gameScreen.GetShapeObjectData))
            {
                gameScreen.ShapeMoveLeft();
            }
        }

        private void ShapeRotate()
        {
            int[,] _potentialData = gameScreen.GetShapeObjectNextOrientationData();

            if (_potentialData != null)
            {

                // 1. Check board colision X and Y

                int _x = gameScreen.GetCorrectedPosX(_potentialData);
                int _y = gameScreen.GetCorrectedPosY(_potentialData);


                // 2. Check array board collision other figure X and Y
                if (!gameScreen.ShapeLanded && CheckGridData(_x, _y, _potentialData))
                {
                    //if all ok, rotate object

                    gameScreen.SetShapeObjectXY(_x, _y);
                    gameScreen.ShapeRotate();
                }
            }
        }

        private int GetIndexRandomShape()
        {
            int _len = Enum.GetNames(typeof(Shape)).Length;
            return rnd.Next(1, _len + 1);
        }

        public override void Render()
        {
            //TODO: GameControl Render neaprasytas
        }

        public override void Clear()
        {
            //TODO: GameControl Clear neaprasytas
        }
    }
}
