using Tetris.Gui;

namespace Tetris.Game
{
    sealed class GuiController : Window
    {
        private char _borderChar;

        public GuiController(int x, int y, int width, int height, char borderChar) : base(x, y, width, height, borderChar)
        {
            _borderChar = borderChar;
        }

        public void ShowMenu()
        {
            MenuController menuController = new MenuController(X, Y, Width, Height, _borderChar);
            menuController.Show();
        }             
    }
}
