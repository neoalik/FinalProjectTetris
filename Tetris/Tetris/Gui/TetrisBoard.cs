using System;
using Tetris.Data;
using Tetris.Gui;

namespace Tetris.MenuGui
{
    class TetrisBoard : GuiObject
    {
        const int SPACE_WITH_ELEMENTS = 5;

        const int TETRIS_GRID_WIDTH = 10;
        const int TETRIS_GRID_HEIGHT = 22;

        private Frame _tetrisGrid;

        //monitoring
        private TextLine _txtBestScoreInput;
        private TextLine _txtScoreInput;
        private TextLine _txtGoalInput;
        private TextLine _txtLevelInput;
        private TextLine _txtNewBestScoreInput;

        public int GetXStartPos
        {
            get
            {
                return Width / 2 - TETRIS_GRID_WIDTH / 2 + SPACE_WITH_ELEMENTS / 2 + 1;
            }
        }

        public int GetYStartPos
        {
            get
            {
                return Height - TETRIS_GRID_HEIGHT - 4;
            }
        }

        public TetrisBoard(int x, int y, int width, int height) : base(x, y, width, height)
        {
            DrawTetrisGrid();
            DrawTetrisCurrent();
            DrawTetrisNext();
            DrawTetrisLevel(0);
            DrawTetrisScore(0);
            DrawTetrisBestScore(0);
            DrawTetrisGoal(0);

            DrawTetrisTitle();
            //DrawTetrisNewBestScore(false);

            Render();
        }

        private void DrawTetrisNewBestScore()
        {
            int _x = _tetrisGrid.X + _tetrisGrid.Width + 1;
            int _y = _tetrisGrid.Y + _tetrisGrid.Height - 10;

            _txtNewBestScoreInput = new TextLine(_x, _y - 1, 14, "Your are the top player.", ConsoleColor.Yellow);
            _txtNewBestScoreInput.Render();
        }

        private void DrawTetrisTitle()
        {
            int _x = _tetrisGrid.X + _tetrisGrid.Width / 2 - 7;
            int _y = _tetrisGrid.Y - 1;

            TextLine _txtTitle = new TextLine(_x, _y, 10, "===> TETRIS <==", ConsoleColor.Green);
        }

        private void DrawTetrisBestScore(int bestscore)
        {
            int _x = _tetrisGrid.X + _tetrisGrid.Width + 1;
            int _y = _tetrisGrid.Y + _tetrisGrid.Height - 8;

            TextLine _txtBestScore = new TextLine(_x, _y - 1, 14, "=>BEST SCORE<=", ConsoleColor.Red);

            Frame _bestscoreFrame = new Frame(_x, _y, 14, 3,
                verticalLeftLine: '█',
                verticalRightLine: '█',
                leftTopCorner: '█',
                rightTopCorner: '█',
                horizontalBottomLine: '▄',
                horizontalTopLine: '▀',
                leftBottomCorner: '█',
                rightBottomCorner: '█');

            _txtBestScoreInput = new TextLine(_x + 2, _y + 1, 12, bestscore.ToString());

            _txtBestScore.Render();
            _bestscoreFrame.Render();
        }

        private void DrawTetrisScore(int scores = 0)
        {
            int _x = _tetrisGrid.X + _tetrisGrid.Width + 1;
            int _y = _tetrisGrid.Y + _tetrisGrid.Height - 3;

            TextLine _txtLevel = new TextLine(_x, _y - 1, 14, "=>SCORE<=", ConsoleColor.Yellow);

            Frame _scoreFrame = new Frame(_x, _y, 14, 3,
                verticalLeftLine: '█',
                verticalRightLine: '█',
                leftTopCorner: '█',
                rightTopCorner: '█',
                horizontalBottomLine: '▄',
                horizontalTopLine: '▀',
                leftBottomCorner: '█',
                rightBottomCorner: '█');

            _txtScoreInput = new TextLine(_x + 2, _y + 1, 12, scores.ToString());

            _txtLevel.Render();
            _scoreFrame.Render();
        }

        private void DrawTetrisGoal(int lines = 0)
        {
            //kiek liniju sunaikino

            int _x = _tetrisGrid.X - 15;
            int _y = _tetrisGrid.Y + _tetrisGrid.Height - 3;

            TextLine _txtGoal = new TextLine(_x, _y - 1, 14, "=>LINES<=", ConsoleColor.Cyan);

            Frame _goalFrame = new Frame(_x, _y, 14, 3,
                verticalLeftLine: '█',
                verticalRightLine: '█',
                leftTopCorner: '█',
                rightTopCorner: '█',
                horizontalBottomLine: '▄',
                horizontalTopLine: '▀',
                leftBottomCorner: '█',
                rightBottomCorner: '█');

            _txtGoalInput = new TextLine(_x + 1, _y + 1, 12, lines.ToString());

            _txtGoal.Render();
            _goalFrame.Render();
        }

        private void DrawTetrisLevel(int level)
        {
            int _x = _tetrisGrid.X - 15;
            int _y = _tetrisGrid.Y + _tetrisGrid.Height - 8;

            TextLine _txtLevel = new TextLine(_x, _y - 1, 14, "=>LEVEL<=", ConsoleColor.Magenta);

            Frame _levelFrame = new Frame(_x, _y, 14, 3,
                verticalLeftLine: '█',
                verticalRightLine: '█',
                leftTopCorner: '█',
                rightTopCorner: '█',
                horizontalBottomLine: '▄',
                horizontalTopLine: '▀',
                leftBottomCorner: '█',
                rightBottomCorner: '█');

            _txtLevelInput = new TextLine(_x + 1, _y + 1, 12, level.ToString());

            _txtLevel.Render();
            _levelFrame.Render();
        }

        private void DrawTetrisCurrent()
        {
            int _x = _tetrisGrid.X - 15;
            int _y = _tetrisGrid.Y + 1;

            TextLine _txtCurrent = new TextLine(_x, _y - 1, 14, "=>HOLD<=", ConsoleColor.Cyan);

            Frame _currentFrame = new Frame(_x, _y, 14, 4,
                verticalLeftLine: '█',
                verticalRightLine: '█',
                leftTopCorner: '█',
                rightTopCorner: '█',
                horizontalBottomLine: '▄',
                horizontalTopLine: '▀',
                leftBottomCorner: '█',
                rightBottomCorner: '█');

            _txtCurrent.Render();
            _currentFrame.Render();
        }

        private void DrawTetrisNext()
        {
            int _x = _tetrisGrid.X + _tetrisGrid.Width + 1;
            int _y = _tetrisGrid.Y + 1;

            TextLine _txtNext = new TextLine(_x, _y - 1, 14, "=>NEXT<=", ConsoleColor.Cyan);

            Frame _goalFrame = new Frame(_x, _y, 14, 4,
                verticalLeftLine: '█',
                verticalRightLine: '█',
                leftTopCorner: '█',
                rightTopCorner: '█',
                horizontalBottomLine: '▄',
                horizontalTopLine: '▀',
                leftBottomCorner: '█',
                rightBottomCorner: '█');

            _txtNext.Render();
            _goalFrame.Render();
        }

        private void DrawTetrisGrid()
        {
            int _x = Width / 2 - TETRIS_GRID_WIDTH - 1;
            int _y = Height - TETRIS_GRID_HEIGHT - 4;

            _tetrisGrid = new Frame(_x, _y,
                TETRIS_GRID_WIDTH * 2 + 2,
                TETRIS_GRID_HEIGHT + 2,
                verticalLeftLine: '█',
                verticalRightLine: '█',
                leftTopCorner: '▄',
                rightTopCorner: '▄',
                horizontalBottomLine: '█',
                horizontalTopLine: '▄',
                leftBottomCorner: '█',
                rightBottomCorner: '█');
        }

        public int[] DisplayCurrentFigure
        {
            get
            {
                int _width = 4;
                int _xGrid = _tetrisGrid.X - _width - SPACE_WITH_ELEMENTS;
                int _yGrid = _tetrisGrid.Y + 2;

                return new int[] { _xGrid, _yGrid };
            }
        }

        public int[] DisplayNextFigure
        {
            get
            {
                int _width = 8;
                int _xGrid = _tetrisGrid.X + _tetrisGrid.Width + _width;
                int _yGrid = _tetrisGrid.Y + 2;

                return new int[] { _xGrid, _yGrid };
            }
        }

        public bool IsPointInGridPlace(int x, int y)
        {
            return !(_tetrisGrid.X >= x || (_tetrisGrid.X + _tetrisGrid.Width <= x) || (_tetrisGrid.Y + _tetrisGrid.Height) <= y);
        }

        public override void Clear()
        {
            //TODO: neaprasyta
            for (int j = _tetrisGrid.Y + 1; j < _tetrisGrid.Y + 1 + TETRIS_GRID_HEIGHT; j++)
            {
                for (int i = _tetrisGrid.X + 1; i < _tetrisGrid.X + 1 + TETRIS_GRID_WIDTH * 2; i++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
        }

        //monitoring set
        public void SetBestScore(int bestscore)
        {
            _txtBestScoreInput.Label = bestscore.ToString();
        }

        public void SetScore(int score)
        {
            _txtScoreInput.Label = score.ToString();
        }

        public void SetLineCounter(int line)
        {
            _txtGoalInput.Label = line.ToString();
        }

        public void SetLevel(int level)
        {
            _txtLevelInput.Label = level.ToString();
        }

        public void SetAttentionHaveNewBestScore()
        {
            DrawTetrisNewBestScore();
        }

        public int GetcollisionMaxX()
        {
            return _tetrisGrid.X + 1 + TETRIS_GRID_WIDTH * 2 - 2;
        }

        public int GetCollisionMinX()
        {
            return _tetrisGrid.X + 1;
        }


        public int GetCollisionMinY()
        {
            return _tetrisGrid.Y + 1;
        }

        public int GetcollisionMaxY()
        {
            return _tetrisGrid.Y + 1 + TETRIS_GRID_HEIGHT - 1;
        }

        public override void Render()
        {
            //TODO: neaprasyta
            _tetrisGrid.Render();

            _txtBestScoreInput.Render();
            _txtScoreInput.Render();
            _txtGoalInput.Render();
            _txtLevelInput.Render();
        }

        public void GridRender(int[,] gridData)
        {
            for (int i = 0; i < gridData.GetLength(0); i++)
            {
                for (int j = 0; j < gridData.GetLength(1); j++)
                {
                    if (gridData[i, j] > 0)
                    {
                        int[] _coords = ConvertArrIndexToCoord(j, i);

                        Console.SetCursorPosition(_coords[0], _coords[1]);
                        Console.ForegroundColor = Colors.GetColorByIndex(gridData[i, j]);
                        Console.Write(Tetrominoes.TetrominSymbol);
                        Console.ResetColor();
                    }
                }
            }
        }

        private int[] ConvertArrIndexToCoord(int indexX, int indexY)
        {

            //_tetrisBoard.GetCollisionMinX();
            //_tetrisBoard.GetCollisionMinY();

            int _newCol = /*((indexX % 2 != 0) ? x - 1 : x)*/ indexX * 2 + GetCollisionMinX();
            int _newRow = indexY + GetCollisionMinY();

            return new int[] { _newCol, _newRow };
        }
    }
}
