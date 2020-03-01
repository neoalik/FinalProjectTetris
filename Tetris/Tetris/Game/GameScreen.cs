using System;
using Tetris.Data;
using Tetris.Gui;
using Tetris.MenuGui;

namespace Tetris.Game
{
    class GameScreen : Window
    {
        private TetrisBoard _tetrisBoard;
        private ShapeObject _shapeObject;

        private ShapeObject _nextShapeObject;
        private ShapeObject _currentShapeObject;

        private int[,] _tetrisGridArr = new int[22, 10];

        private bool _shapeLanded = false;
        //private int _shapeColor = 1;

        public GameScreen(int x, int y, int width, int height, char borderChar) : base(x, y, width, height, borderChar)
        {
            _tetrisBoard = new TetrisBoard(x, y, width, height);

            //bool checkTest = _tetrisBoard.IsPointInGridPlace(58,6);
        }

        public int GetTetrisBoardX
        {
            get
            {
                return _tetrisBoard.GetCollisionMinX();
            }
        }

        public int GetTetrisBoardY
        {
            get
            {
                return _tetrisBoard.GetCollisionMinY();
            }
        }

        public int GetShapeObjectX
        {
            get
            {
                return (_shapeObject != null) ? _shapeObject.X : 0;
            }
        }

        public int GetShapeObjectY
        {
            get
            {
                return (_shapeObject != null) ? _shapeObject.Y : 0;
            }
        }

        public int[,] GetShapeObjectData
        {
            get
            {
                return (_shapeObject != null) ? _shapeObject.GetCurrentShapeData : null;
            }
        }

        public bool ShapeLanded 
        { 
            get => _shapeLanded; 
            private set => _shapeLanded = value; 
        }

        public int GetShapeObjectColor 
        {
            get
            {
                return _shapeObject.ShapeColor;
            }
        }

        public void SetBestScore(int bestscore)
        {
            _tetrisBoard.SetBestScore(bestscore);
        }

        public void SetScore(int score)
        {
            _tetrisBoard.SetScore(score);
        }

        public void SetLineCounter(int line)
        {
            _tetrisBoard.SetLineCounter(line);
        }

        public void SetLevel(int level)
        {
            _tetrisBoard.SetLevel(level);
        }

        public void SetAttentionHaveNewBestScore()
        {
            _tetrisBoard.SetAttentionHaveNewBestScore();
        }

        public void ShapeRotate()
        {
            //get current shape
            if (_shapeObject != null)
            {
                _shapeObject.Rotate();
            }
        }

        public int[,] GetShapeObjectNextOrientationData()
        {
            if (_shapeObject != null)
            {
                return _shapeObject.ShapeNextOrientationData;
            }

            return null;
        }

        public void SetShapeObjectXY(int x, int y)
        {
            if (_shapeObject != null)
            {
                _shapeObject.Clear();
                _shapeObject.X = x;
                _shapeObject.Y = y;
            }
        }

        public void ShapeMoveDown()
        {
            //get current shape
            if (_shapeObject != null)
            {
                _shapeObject.MoveDown();
            }
        }

        public void GetShape(int randShape, int level)
        {
            if (_shapeObject == null)
            {
                _shapeObject = new ShapeObject(_tetrisBoard.GetXStartPos, _tetrisBoard.GetYStartPos + 1, 0, 0, (Data.Shape)randShape, level);
                CurrentShape(randShape, level);
            }
        }

        public void CurrentShape(int randShape, int level)
        {
            int[] _coord = _tetrisBoard.DisplayCurrentFigure;
            _currentShapeObject?.Clear();

            _currentShapeObject = new ShapeObject(_coord[0], _coord[1], 0, 0, (Data.Shape)randShape, level);
        }

        public void NextShape(int randShape, int level)
        {
            int[] _coord = _tetrisBoard.DisplayNextFigure;
            
            _nextShapeObject?.Clear();

            _nextShapeObject = new ShapeObject(_coord[0], _coord[1], 0, 0, (Data.Shape)randShape, level);
        }

        public void SetShapeLanded(bool stop)
        {
            if (stop)
            {
                _shapeLanded = true;
            }
            else
            {
                _shapeObject = null;
                _shapeLanded = false;
            }
        }

        public void ShapeMoveRight()
        {
            //get current shape
            if (_shapeObject != null)
            {
                _shapeObject.MoveRight();
            }
        }

        public void ShapeMoveLeft()
        {
            //get current shape
            if (_shapeObject != null)
            {
                _shapeObject.MoveLeft();
            }
        }

        public bool CanShapeMove(MoveDirections direction)
        {
            if (_shapeObject != null)
            {
                return CheckCanMoveShape(_shapeObject.X, _shapeObject.Y, direction);
            }

            return false;
        }

        public void ReDrawingTetrisGrid(int[,] gridData)
        {
            _tetrisBoard.Clear();
            _tetrisBoard.GridRender(gridData);
        }

        private bool CheckCanMoveShape(int x, int y, MoveDirections direction)
        {
            int[,] _currentShape = _shapeObject.GetCurrentShapeData;
            bool _canMove = false;

            for (int i = 0; i < _currentShape.GetLength(0); i++)
            {
                int _x = 0;
                int _y = 0;

                for (int j = 0; j < _currentShape.GetLength(1); j++)
                {
                    //if (j % 2 != 0) continue;

                    if (_shapeObject.X % 2 != 0)
                    {
                        _shapeObject.X -= 1;
                    }

                    _x = x + (int)_currentShape[i, j] * 2;

                    _y = y + (int)_currentShape[i, ++j];

                    if (direction == MoveDirections.LEFT && _x - 2 >= _tetrisBoard.GetCollisionMinX())
                    {
                        // obj.X - 2
                        _canMove = true;
                    }
                    else if (direction == MoveDirections.RIGHT && _x + 2 <= _tetrisBoard.GetcollisionMaxX())
                    {
                        // obj.X + 2
                        _canMove = true;
                    }
                    else if (direction == MoveDirections.DOWN && _y + 1 <= _tetrisBoard.GetcollisionMaxY())
                    {
                        _canMove = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }


            return _canMove;
        }


        public void GridClear()
        {
            _tetrisBoard.Clear();
        }

        public int GetCorrectedPosX(int[,] data)
        {
            if (_shapeObject != null)
            {

                int _gridMinX = _tetrisBoard.GetCollisionMinX();
                int _gridMaxX = _tetrisBoard.GetcollisionMaxX();

                int _newX = _shapeObject.X;

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    int _x = 0;

                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        if (j % 2 != 0) continue;

                        if (_shapeObject.X % 2 != 0)
                        {
                            _shapeObject.X -= 1;
                        }

                        int _test = (int)data[i, j] * 2;

                        _x = _shapeObject.X + (int)data[i, j] * 2;

                        if (_x < _gridMinX)
                        {
                            _newX = _shapeObject.X + _gridMinX - _x;// +cols
                            continue;
                        }

                        if (_x > _gridMaxX)
                        {
                            _newX = _shapeObject.X + _gridMaxX - _x;//-cols
                        }
                    }
                }

                return _newX;
            }

            return 0;
        }

        public int GetCorrectedPosY(int[,] data)
        {
            if (_shapeObject != null)
            {
                int _gridMinY = _tetrisBoard.GetCollisionMinY();
                int _gridMaxY = _tetrisBoard.GetcollisionMaxY();

                int _newY = _shapeObject.Y;

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    int _y = 0;

                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        _y = _shapeObject.Y + (int)data[i, ++j];

                        if (_y < _gridMinY)
                        {
                            _newY = _shapeObject.Y + _gridMinY - _y;// +rows
                        }

                        if (_y > _gridMaxY)
                        {
                            _newY = _shapeObject.Y + _gridMaxY - _y;//-rows
                        }
                    }
                }

                return _newY;
            }
            return 0;
        }
    }
}
