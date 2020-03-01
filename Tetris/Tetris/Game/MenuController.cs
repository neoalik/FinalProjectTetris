using System;
using Tetris.Data;
using Tetris.Gui;
using Tetris.MenuGui;

namespace Tetris.Game
{
    class MenuController : Window
    {
        private MainMenu mainMenu;
        private GameOverMenu gameOverMenu;
        private GameController gameController;

        public MenuController(int x, int y, int width, int height, char borderChar) : base(x, y, width, height, borderChar)
        {

        }

        public void Show()
        {
            mainMenu = new MainMenu(X, Y, Width, Height);
            mainMenu.OnMenuLeave += MainMenu_OnMenuLeave;
            mainMenu.Show();
        }

        /// <summary>
        /// Alls menu controls
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="op"></param>
        private void MainMenu_OnMenuLeave(Menu menu, MenuOperations op)
        {
            Console.Clear();
            Render();
            switch (op)
            {
                case MenuOperations.GotoMainMenu:
                    mainMenu.Show();
                    break;
                case MenuOperations.Play:
                    gameController = new GameController(X,Y, Width, Height);
                    gameController.OnMenuLeave += MainMenu_OnMenuLeave;
                    gameController.StartGame();
                    break;
                case MenuOperations.Replay:
                    Console.Clear();//start game
                    gameController.ResetGame();
                    //TODO: start again game
                    break;
                case MenuOperations.GotoGameOverMenu:
                    if (gameOverMenu == null)
                    {
                        gameOverMenu = new GameOverMenu(X, Y, Width, Height);
                        gameOverMenu.OnMenuLeave += MainMenu_OnMenuLeave;
                    }

                    gameOverMenu.Show();
                    break;
                case MenuOperations.Quit:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
