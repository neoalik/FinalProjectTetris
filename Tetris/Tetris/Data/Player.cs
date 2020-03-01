using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Data
{
    public class Player
    {
        private int _score = 0;

        private int _level = 0;

        private int _increseLines = 0;

        private int _totalIncreseLines = 0;

        public int GetPlayerLevel
        {
            get
            {
                return _level;
            }
        }

        public int GetPlayerScore
        {
            get
            {
                return _score;
            }
        }

        public int GetTotalIncreseLines
        {
            get
            {
                return _totalIncreseLines;
            }
        }
        
        public Player()
        {

        }

        public void AddScore(int combo)
        {
            _increseLines += combo;
            _totalIncreseLines += combo;
            _score += Score.ValueByIndex[combo - 1];
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            if(_increseLines >= 10)
            {
                _increseLines = 0;
                _level++;
            }
        }
    }
}
