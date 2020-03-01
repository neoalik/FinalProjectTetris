using System;
using System.Drawing;
using Tetris.Data;

namespace Tetris.Gui
{
    class ShapeObject : GuiObject
    {
        private readonly Shape _shape;
        private int[][,] _shapeTemplate;

        private int _shapeOrientation = 0;
        private TetrisColor _shapeColor;

        private int _shapeLevel;

        public int[,] GetCurrentShapeData
        {
            get
            {
                return _shapeTemplate[_shapeOrientation];
            }
        }

        public int[,] ShapeNextOrientationData
        {
            get
            {
                return _shapeTemplate[RotateShape()];
            }
        }

        public int ShapeOrientation
        {
            get
            {
                return _shapeOrientation;
            }
        }

        public int PointX 
        {
            get 
            {
                return 0;
            }
        }
        public int PointY
        {
            get
            {
                return 0;
            }
        }

        public int ShapeColor
        {
            get
            {
                return (int)_shapeColor;
            }
        }

        public ShapeObject(int x, int y, int width, int height, Shape shape, int level) : base(x, y, width, height)
        {
            _shape = shape;
            _shapeLevel = level;
            DrawTetrominoe(_shape);
        }

        private void DrawTetrominoe(Shape shape)
        {
            switch (shape)
            {
                case Shape.T_Block:
                    _shapeTemplate = Tetrominoes.T_Block;
                    break;
                case Shape.S_Block:
                    _shapeTemplate = Tetrominoes.S_Block;
                    break;
                case Shape.Z_Block:
                    _shapeTemplate = Tetrominoes.Z_Block;
                    break;
                case Shape.I_Block:
                    _shapeTemplate = Tetrominoes.I_Block;
                    break;
                case Shape.L_Block:
                    _shapeTemplate = Tetrominoes.L_Block;
                    break;
                case Shape.J_Block:
                    _shapeTemplate = Tetrominoes.J_Block;
                    break;
                case Shape.O_Block:
                    _shapeTemplate = Tetrominoes.O_Block;
                    break;
                default:
                    throw new Exception("Tetrominoe not exits.");
            }

            _shapeColor = (TetrisColor)((int)shape/* * _shapeLevel*/);

            Y += GetStartYPos();

            Render();
        }

        private int GetStartYPos()
        {
            int _minY = Y;

            for (int i = 0; i < _shapeTemplate[_shapeOrientation].GetLength(0); i++)
            {
                int _y = 0;

                for (int j = 0; j < _shapeTemplate[_shapeOrientation].GetLength(1); j++)
                {
                    _y = Y + _shapeTemplate[_shapeOrientation][i, ++j];

                    if(_y < Y)
                    {
                        _minY = _y;
                    }
                }
            }

            _minY = Y - _minY;

            return _minY;
        }

        public override void Clear()
        {
            for (int i = 0; i < _shapeTemplate[_shapeOrientation].GetLength(0); i++)
            {
                int _x = 0;
                int _y = 0;

                for (int j = 0; j < _shapeTemplate[_shapeOrientation].GetLength(1); j++)
                {
                    _x = X + _shapeTemplate[_shapeOrientation][i, j] * 2;

                    _y = Y + _shapeTemplate[_shapeOrientation][i, ++j];

                    Console.SetCursorPosition(_x, _y);
                    Console.Write("  ");
                }
            }
        }

        public void MoveRight()
        {
            Clear();
            X += 2;
            Render();
        }

        public void MoveLeft()
        {
            Clear();
            X -= 2;
            Render();
        }

        public void MoveDown()
        {
            Clear();

            Y++;

            Render();
        }

        public override void Render()
        {
            for (int i = 0; i < _shapeTemplate[_shapeOrientation].GetLength(0); i++)
            {
                for (int j = 0; j < _shapeTemplate[_shapeOrientation].GetLength(1); j++)
                {
                    int _x = X + (int)_shapeTemplate[_shapeOrientation][i, j] * 2;
                    int _y = Y + _shapeTemplate[_shapeOrientation][i, ++j];

                    Console.SetCursorPosition(_x, _y);
                    Console.ForegroundColor = Colors.GetColorByName(_shapeColor);
                    Console.Write(Tetrominoes.TetrominSymbol);//■
                    Console.ResetColor();
                }
            }
        }

        private int RotateShape()
        {
            int _nextOrientation = _shapeOrientation;

            if (_nextOrientation + 1 > _shapeTemplate.Length - 1)
            {
                _nextOrientation = 0;
            }
            else
            {
                _nextOrientation++;
            }

            return _nextOrientation;
        }

        public void Rotate()
        {
            Clear();

            #region kol kas hidden
            //_shapeTemplate[_shapeOrientation] = RotateMatrix(_shapeTemplate[_shapeOrientation], 4);
            //Render();

            /*for (int i = 0; i < _shapeTemplate[_shapeOrientation].GetUpperBound(0); i++)
            {
                for (int j = 0; j < _shapeTemplate[_shapeOrientation].GetUpperBound(1); j++)
                {
                    if (_shapeTemplate[_shapeOrientation][i, j] != 0)
                    {
                        Console.SetCursorPosition(_shapeTemplate[_shapeOrientation][i, j], Y + 1 + i);
                        Console.Write("■");
                    }
                }
            }*/
            /*
            int _centerX = 0;
            int _centerY = 0;

            for (int i = 0; i < _shapeTemplate[_shapeOrientation].GetLength(0); i++)
            {

                if(i == 0)
                {
                    _centerX = X + _shapeTemplate[_shapeOrientation][i, 0];
                    _centerY = Y + _shapeTemplate[_shapeOrientation][i, 1];
                    Console.SetCursorPosition(_centerX, _centerY);
                    Console.Write("■");
                    continue;
                }

                int _x = 0;
                int _y = 0;

                _x = X + (int)_shapeTemplate[_shapeOrientation][i, 0];
                _y = Y + _shapeTemplate[_shapeOrientation][i, 1];

                int _rotX = _x - _centerX;
                int _rotY = _y - _centerY;

                int _rotXX = 0 * _rotX + (-1) * _rotY;
                int _rotYY = 1 * _rotX + 0 * _rotY;

                //sum

                int _newX = _centerX + _rotXX * 2;
                int _newY = _centerY + _rotYY;


                Console.SetCursorPosition(_newX, _newY);
                Console.Write("■");
            }*/

            //_shapeTemplate[_shapeOrientation] = RotateMatrix(_shapeTemplate[_shapeOrientation], 2);

            #endregion

            _shapeOrientation = RotateShape();

            Render();
        }
    }
}
