using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tetris.Data
{
    public enum TetrisColor
    {
        Aqua = 1,
        Blue,
        LightGray,
        Yellow,
        Lime,
        Magenta,
        Red,
        Chartreuse = 8,
        GreenYellow,
        LawnGreen,
        LightGreen,
        LimeGreen,
        PaleGreen,
        SpringGreen
    }

    public class Colors
    {
        public static ConsoleColor GetColorByName(TetrisColor name)
        {
            return FromColor(Color.FromName(name.ToString()));
        }

        public static ConsoleColor GetColorByIndex(int index)
        {
            TetrisColor name = (TetrisColor)index;
            return FromColor(Color.FromName(name.ToString()));
        }

        public static ConsoleColor FromColor(System.Drawing.Color color)
        {
            int index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0; 
            index |= (color.R > 64) ? 4 : 0; 
            index |= (color.G > 64) ? 2 : 0; 
            index |= (color.B > 64) ? 1 : 0; 
            return (ConsoleColor)index;
        }
    }
}
