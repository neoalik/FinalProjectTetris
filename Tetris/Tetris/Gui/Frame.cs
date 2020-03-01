using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Gui
{
    class Frame : GuiObject
    {
        private char _left_Top_Corner;
        private char _right_Top_Corner;

        private char _horizontal_Top_Line;
        private char _horizontal_Bottom_Line;

        private char _vertical_Left_Line;
        private char _vertical_Right_Line;

        private char _left_Bottom_Corner;
        private char _right_Bottom_Corner;

        public Frame(int x, int y, int width, int height, char borderChar) : base(x, y, width, height)
        {
            _left_Top_Corner = _right_Top_Corner = _horizontal_Top_Line = _horizontal_Bottom_Line = _vertical_Left_Line = _vertical_Right_Line = _left_Bottom_Corner = _right_Bottom_Corner = borderChar;
        }

        public Frame(int x, int y, int width, int height, 
            char leftTopCorner = '┌', 
            char rightTopCorner = '┐', 
            char horizontalTopLine = '─',
            char horizontalBottomLine = '─',
            char verticalLeftLine = '│',
            char verticalRightLine = '│',
            char leftBottomCorner = '└',
            char rightBottomCorner = '┘') : base(x,y, width, height)
        {
            _left_Top_Corner = leftTopCorner;
            _right_Top_Corner = rightTopCorner;
            _horizontal_Top_Line = horizontalTopLine;
            _horizontal_Bottom_Line = horizontalBottomLine;
            _vertical_Left_Line = verticalLeftLine;
            _vertical_Right_Line = verticalRightLine;
            _left_Bottom_Corner = leftBottomCorner;
            _right_Bottom_Corner = rightBottomCorner;
        }

        public override void Clear()
        {
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(X, Y + i);

                for (int j = 0; j < Width; j++)
                {
                    Console.Write(' ');
                }
            }
        }

        public override void Render()
        {
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(X, Y + i);

                if (i == 0)
                {
                    Console.Write(_left_Top_Corner);
                    for (int j = 0; j < Width - 2; j++)
                    {
                        Console.Write(_horizontal_Top_Line);
                    }
                    Console.Write(_right_Top_Corner);
                }
                else if (i == Height - 1)
                {
                    Console.Write(_left_Bottom_Corner);
                    for (int j = 0; j < Width - 2; j++)
                    {
                        Console.Write(_horizontal_Bottom_Line);
                    }

                    Console.Write(_right_Bottom_Corner);
                }
                else
                {
                    Console.Write(_vertical_Left_Line);
                    for (int j = 0; j < Width - 2; j++)
                    {
                        Console.Write(' ');
                    }

                    Console.Write(_vertical_Right_Line);
                }
            }
        }
    }
}
