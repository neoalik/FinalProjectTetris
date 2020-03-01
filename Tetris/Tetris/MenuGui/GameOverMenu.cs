using Tetris.Data;
using Tetris.Gui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.MenuGui
{
    class GameOverMenu : Menu
    {
        private int _yPosButton = 8;

        public override event Action<Menu, MenuOperations> OnMenuLeave;

        public GameOverMenu(int x, int y, int width, int height) : base(x, y, width, height)
        {
            //
        }

        /// <summary>
        /// Menu initialization
        /// </summary>
        public override void Init()
        {
            string titleMsg = "Select from menu:";
            int titlePos = Width / 2 - titleMsg.Length / 2;// - titleMsg.Length / 2;
            
            TitleMenu = new TextLine(titlePos, _yPosButton - 2, titleMsg.Length, titleMsg);

            int space = 0;
            int firstBtn = (Width / 2 - (WidthMenuButton + space) / 2);

            Button _reply = new Button("reply", firstBtn, _yPosButton, WidthMenuButton, HeightMenuButton, "R - Replay");
            _reply.OnEnter += Buttons_OnEnter;

            Button _gotoMenu = new Button("gotoMenu", firstBtn, _yPosButton + _reply.Height, WidthMenuButton, HeightMenuButton, "M - Go to menu");
            _gotoMenu.OnEnter += Buttons_OnEnter;

            Button _quit = new Button("quit", firstBtn, _yPosButton + _reply.Height + _gotoMenu.Height, WidthMenuButton, HeightMenuButton, "Q - Quit");
            _quit.OnEnter += Buttons_OnEnter;

            AddButton(new List<Button> 
            {
                _reply,
                _gotoMenu,
                _quit,
            });
        }

        /// <summary>
        /// set title for menu
        /// </summary>
        /// <param name="msg"></param>
        public void SetTitle(string msg)
        {
            var _title = TitleMenu;
            //_title.Clear();
            _title.Label = msg;
        }

        /// <summary>
        /// Menu navigation
        /// </summary>
        /// <param name="btn"></param>
        private void Buttons_OnEnter(Button btn)
        {
            switch (btn.Name)
            {
                case "reply":
                    OnMenuLeave?.Invoke(this, MenuOperations.Replay);
                    break;
                case "gotoMenu":
                    OnMenuLeave?.Invoke(this, MenuOperations.GotoMainMenu);
                    break;
                case "quit":
                    OnMenuLeave?.Invoke(this, MenuOperations.Quit);
                    break;
                default:
                    throw new Exception("Button not found with name");
            }
            ExitMenu();
        }

        public override void MenuKeyPress(ConsoleKeyInfo pressedChar)
        {
            switch (pressedChar.Key)
            {
                case ConsoleKey.R:
                    OnMenuLeave?.Invoke(this, MenuOperations.Replay);
                    ExitMenu();
                    break;
                case ConsoleKey.M:
                    OnMenuLeave?.Invoke(this, MenuOperations.GotoMainMenu);
                    ExitMenu();
                    break;
                case ConsoleKey.Q:
                    OnMenuLeave?.Invoke(this, MenuOperations.Quit);
                    ExitMenu();
                    break;
                case ConsoleKey.Enter:
                    MenuEnterButton();
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
            //TODO: neaprasyta
        }

        public override void Clear()
        {
            //TODO: neaprasyta
        }

        /// <summary>
        /// Close menu
        /// </summary>
        public override void CloseMenu()
        {
            OnMenuLeave = null;
        }
    }
}
