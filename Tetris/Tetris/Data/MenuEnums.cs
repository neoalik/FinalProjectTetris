using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Data
{
    public enum MenuDirections
    {
        UP,
        Down,
        Left,
        Right
    }

    public enum MenuOperations
    {
        Play,
        Replay,
        GotoMainMenu,
        GotoGameOverMenu,
        Quit
    }
}
