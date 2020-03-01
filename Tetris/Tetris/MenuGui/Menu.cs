using Tetris.Data;
using Tetris.Gui;
using System;
using System.Collections.Generic;

namespace Tetris.MenuGui
{
    abstract class Menu : GuiObject, IMenu
    {
        private bool _waitKeyPress = false;

        private List<Button> _menuButtons = new List<Button>();

        private TextLine _titleMenu;

        public TextLine TitleMenu
        {
            get
            {
                return _titleMenu;
            }
            set
            {
                _titleMenu = value;
                _titleMenu.Render();
            }
        }

        private int _widthMenuButton = 18;//default
        private int _heightMenuButton = 5;//default

        public int WidthMenuButton
        {
            get { return _widthMenuButton; }
            set { _widthMenuButton = value; }
        }

        public int HeightMenuButton
        {
            get { return _heightMenuButton; }
            set { _heightMenuButton = value; }
        }

        public Menu(int x, int y, int width, int height) : base(x, y, width, height)
        {
            Init();
        }

        public void Show()
        {
            _titleMenu?.Render();
            DefaultMenuButtonSetActive();
            RenderMenuButton();

            WaitKeyPressInMenu();
        }

        /// <summary>
        /// Set default button active
        /// </summary>
        private void DefaultMenuButtonSetActive()
        {
            if (_menuButtons.Count > 0)
            {
                _menuButtons.ForEach(mb => mb.NoActive());
                _menuButtons[0].SetActive();
            }
        }

        /// <summary>
        /// Laukia vartotojo mygtuko paspaudimo
        /// </summary>
        private void WaitKeyPressInMenu()
        {
            _waitKeyPress = true;
            RunMenuKeyPressLoop();
        }

        /// <summary>
        /// Add to menu buttons list
        /// </summary>
        /// <param name="buttons">Group button</param>
        public void AddButton(List<Button> buttons)
        {
            _menuButtons.AddRange(buttons);

            DefaultMenuButtonSetActive();
            RenderMenuButton();
        }

        /// <summary>
        /// Add to buttons list button
        /// </summary>
        /// <param name="button">One button</param>
        public void AddButton(Button button)
        {
            _menuButtons.Add(button);
            DefaultMenuButtonSetActive();
            RenderMenuButton();
        }

        /// <summary>
        /// Wait key press loop
        /// </summary>
        private void RunMenuKeyPressLoop()
        {
            do
            {
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedChar = Console.ReadKey(true);
                    //int hashCode = pressedChar.Key.GetHashCode();

                    MenuKeyPress(pressedChar);

                }

                System.Threading.Thread.Sleep(200);
            } while (_waitKeyPress);
        }

        private void RenderMenuButton()
        {
            _menuButtons.ForEach(button => button.Render());
        }

        /// <summary>
        /// Get index of button from list
        /// </summary>
        /// <returns></returns>
        private int GetIndexActiveButtonMenu()
        {
            for (int i = 0; i < _menuButtons.Count; i++)
            {
                if (_menuButtons[i].IsActive)
                {
                    return i;
                }
            }

            return -1;
        }
        /// <summary>
        /// control by key menu
        /// </summary>
        /// <param name="menuDirection"></param>
        private void SelectMenu(MenuDirections menuDirection)
        {
            /*
             *       0    1
             *      [2]   3     [0]    1 
             *       4    5
             */

            int maxMenuButton = _menuButtons.Count;

            int horizontal = 1;
            int vertical = (maxMenuButton + 1) / 2 - 1;

            int index = GetIndexActiveButtonMenu();

            int nextMenuButtonIndex = index;

            switch (menuDirection)
            {
                case MenuDirections.UP:
                    nextMenuButtonIndex = index - vertical;

                    if (nextMenuButtonIndex < 0)
                    {
                        nextMenuButtonIndex = Math.Abs(maxMenuButton + nextMenuButtonIndex);
                    }

                    break;
                case MenuDirections.Down:
                    nextMenuButtonIndex = index + vertical;

                    if (nextMenuButtonIndex >= maxMenuButton)
                    {
                        nextMenuButtonIndex = Math.Abs(maxMenuButton - nextMenuButtonIndex);
                    }
                    break;
                case MenuDirections.Left:

                    nextMenuButtonIndex = index - horizontal;

                    if(nextMenuButtonIndex < 0)
                    {
                        nextMenuButtonIndex = maxMenuButton - 1;
                    }
                    break;
                case MenuDirections.Right:
                    nextMenuButtonIndex = index + horizontal;

                    if (nextMenuButtonIndex >= maxMenuButton)
                    {
                        // 7 = 4 + 3
                        nextMenuButtonIndex = 0;// Math.Abs(maxMenuButton - 1 - nextMenuButtonIndex);
                    }
                    break;

                    /*
                     *       0    3
                     *      [1]   4
                     *       2    5
                     */
            }

            _menuButtons[index].NoActive();
            _menuButtons[nextMenuButtonIndex].SetActive();
            RenderMenuButton();
        }

        public void MenuLeftMove()
        {
            SelectMenu(MenuDirections.Left);
        }

        public void MenuRightMove()
        {
            SelectMenu(MenuDirections.Right);
        }

        public void MenuUpMove()
        {
            SelectMenu(MenuDirections.UP);
        }

        public void MenuDownMove()
        {
            SelectMenu(MenuDirections.Down);
        }

        public void MenuEnterButton()
        {
            int index = GetIndexActiveButtonMenu();

            if (index >= 0)
                _menuButtons[index].Enter();
        }

        /// <summary>
        /// Exit from menu
        /// </summary>
        protected void ExitMenu()
        {
            _waitKeyPress = false;
        }

        /// <summary>
        /// Handle key pressed
        /// </summary>
        /// <param name="pressedChar"></param>
        abstract public void MenuKeyPress(ConsoleKeyInfo pressedChar);
        abstract public void Init();

        abstract public event Action<Menu, MenuOperations> OnMenuLeave;

        abstract public void CloseMenu();
    }
}
