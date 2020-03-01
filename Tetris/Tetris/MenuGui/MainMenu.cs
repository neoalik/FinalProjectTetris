using Tetris.Data;
using Tetris.Gui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.MenuGui
{
    class MainMenu : Menu
    {
        private int _yPosButton = 5;

        public override event Action<Menu, MenuOperations> OnMenuLeave;

        public MainMenu(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
        }

        public override void Init()
        {
            string titleMsg = "Select from menu:";
            int titlePos = Width / 2 - titleMsg.Length + 6;

            _yPosButton = Height / 2 - HeightMenuButton / 2;

            TitleMenu = new TextLine(titlePos, _yPosButton - 2, titleMsg.Length + 6, titleMsg);
            
            

            int space = 5;
            int firstBtn = (Width / 2 - (WidthMenuButton * 2 + space) / 2);

            Button _play = new Button("play", firstBtn, _yPosButton, WidthMenuButton, HeightMenuButton, "Play");
            _play.OnEnter += MenuButton_OnEnter;

            Button _quit = new Button("quit", firstBtn + WidthMenuButton + space, _yPosButton, WidthMenuButton, HeightMenuButton, "Quit");
            _quit.OnEnter += MenuButton_OnEnter;


            AddButton(new List<Button> {
                _play,
                _quit,
            });
        }

        private void MenuButton_OnEnter(Button button)
        {
            switch (button.Name)
            {
                case "play":
                    //OnMenuLeave?.Invoke(this, MenuOperations.?);
                    OnMenuLeave?.Invoke(this, MenuOperations.Play);
                    //TODO: run play game
                    break;
                case "quit":
                    OnMenuLeave?.Invoke(this, MenuOperations.Quit);
                    break;
                default:
                    throw new Exception("Not found button by name");
            }

            ExitMenu();
        }

        public override void MenuKeyPress(ConsoleKeyInfo pressedChar)
        {
            switch (pressedChar.Key)
            {
                case ConsoleKey.Escape:
                    //OnMenuLeave?.Invoke(this, MenuOperations.Quit);
                    //ExitMenu();
                    break;
                case ConsoleKey.Enter:
                    MenuEnterButton();
                    break;
                case ConsoleKey.RightArrow:
                    MenuRightMove();
                    break;
                case ConsoleKey.LeftArrow:
                    MenuLeftMove();
                    break;
                case ConsoleKey.UpArrow:
                    MenuUpMove();
                    break;
                case ConsoleKey.DownArrow:
                    MenuDownMove();
                    break;
                default:
                    break;
            }
        }

        public override void Render()
        {
            
        }

        public override void Clear()
        {
            
        }

        public override void CloseMenu()
        {
            OnMenuLeave = null;
        }
    }
}
