using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.Data
{
    public class GridData
    {
        private int[,] _tetrisGridArr = new int[22, 10];

        private int _startArrIndexRow;
        private int _startArrIndexCol;
        private int _lastCombo;
        public GridData(int x, int y)
        {
            _startArrIndexCol = x;
            _startArrIndexRow = y;
        }

        public int[,] GetGridData
        {
            get
            {
                return _tetrisGridArr;
            }
        }

        public int LastCombo
        {
            get
            {
                return _lastCombo;
            }
        }

        public void Fill(int x, int y, int[,] data, int color)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                int _x = 0;
                int _y = 0;

                for (int j = 0; j < data.GetLength(1); j++)
                {
                    _x = x + (int)data[i, j] * 2;

                    _y = y + (int)data[i, ++j];

                    int[] indexes = ConvertCoordToArrIndex(_x, _y);

                    _tetrisGridArr[indexes[0], indexes[1]] = color;//saugoti spalva
                }
            }
        }

        private int[] ConvertCoordToArrIndex(int x, int y)
        {
            int _newCol = ((x % 2 != 0) ? x - 1 : x) - _startArrIndexCol;
            int _newRow = y - _startArrIndexRow;

            return new int[] { _newRow, _newCol / 2 };
        }

        public bool IsSpaceInGrid(int x, int y, int[,] data/*, MoveDirections direction*/)
        {
            #region code
            for (int i = 0; i < data.GetLength(0); i++)
            {
                int _x = 0;
                int _y = 0;

                for (int j = 0; j < data.GetLength(1); j++)
                {
                    _x = ((x % 2 != 0) ? x - 1 : x) + (int)data[i, j] * 2;

                    _y = y + (int)data[i, ++j];

                    int[] indexes = ConvertCoordToArrIndex(_x, _y);

                    if (_tetrisGridArr[indexes[0], indexes[1]] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
            #endregion
        }

        public int[,] CheckGridData()
        {
            int[,] _newGridData = new int[22, 10];

            List<int> _rowsRemove = new List<int>();

            for (int i = 0; i < _tetrisGridArr.GetLength(0); i++)
            {
                bool _isFilled = true;

                for (int j = 0; j < _tetrisGridArr.GetLength(1); j++)
                {
                    if (_tetrisGridArr[i, j] == 0)
                    {
                        _isFilled = false;
                        break;
                    } 
                }

                if(_isFilled)
                {
                    //throw new Exception("remove line because is full");
                    _rowsRemove.Add(i);
                }
            }

            if (_rowsRemove.Count > 0)
            {
                _lastCombo = _rowsRemove.Count;
                for (int z = 0; z < _lastCombo; z++)
                {
                    _tetrisGridArr = TrimArray(_rowsRemove[z], _tetrisGridArr);
                }

                return _tetrisGridArr;
            }

            return null;
        }

        public int[,] TrimArray(int rowToRemove, int[,] originalArray)
        {
            int[,] result = new int[originalArray.GetLength(0), originalArray.GetLength(1)];

            for (int i = originalArray.GetLength(0) - 1, j = originalArray.GetLength(0) - 1; i > 0; i--)
            {
                if(i == rowToRemove)
                {
                    i--;
                }

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j--;
            }

            return result;
        }
    }
}
