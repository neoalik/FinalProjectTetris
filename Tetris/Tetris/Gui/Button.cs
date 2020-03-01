using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris.Gui
{
    class Button : GuiObject
    {
        private readonly string _name;

        public string Name
        {
            get { return _name; }
        }
        public bool IsActive { get; private set; } = false;

        public string Label
        {
            get { return _textLine.Label; }
            set { _textLine.Label = value; }
        }

        private Frame _notActiveFrame;
        private Frame _activeFrame;

        private TextLine _textLine;

        public event Action<Button> OnEnter;

        public Button(string name, int x, int y, int width, int height, string buttonText, char frameButton = '+', char frameActiveButton = '#') : base(x, y, width, height)
        {
            _name = name;
            _notActiveFrame = new Frame(x, y, width, height, frameButton);
            _activeFrame = new Frame(x, y, width, height, frameActiveButton);

            _textLine = new TextLine(x + 1, y + 1 + ((height - 2) / 2), width - 2, buttonText);
        }

        public override void Render()
        {
            if (IsActive)
            {
                _activeFrame.X = X;
                _activeFrame.Y = Y;
                _activeFrame.Render();
            }
            else
            {
                _notActiveFrame.X = X;
                _notActiveFrame.Y = Y;
                _notActiveFrame.Render();
            }

            _textLine.X = X + 1;
            _textLine.Y = Y + 1 + ((Height - 2) / 2);
            _textLine.Render();
        }

        public void SetActive()
        {
            IsActive = true;
        }

        public void NoActive()
        {
            IsActive = false;
        }

        public void Enter()
        {
            if(OnEnter != null)
            {
                OnEnter(this);
            }
        }

        public override void Clear()
        {
            //TODO:clear button
            //Label = string.Empty;

            _notActiveFrame.X = _activeFrame.X = X;
            _notActiveFrame.Y = _activeFrame.Y = Y;

            
            _activeFrame.Clear();
            _notActiveFrame.Clear();
        }
    }
}
