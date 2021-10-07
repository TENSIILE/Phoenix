using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Phoenix.UI
{
    public class PhoenixTextBox : TextBox
    {
        private bool _isPlaceHolder = false;
        private bool _enableNumericInput = false;
        private bool _isChangeHeight = false;

        private Color _colorText;

        private string _placeHolderText;

        public PhoenixTextBox()
        {
            GotFocus += RemovePlaceHolderHandler;
            LostFocus += SetPlaceholderHandler;
            KeyPress += KeyPressTextBoxHandler;

            _colorText = ForeColor;
        }

        [DefaultValue(null)]
        public string PlaceHolderText
        {
            get => _placeHolderText;
            set
            {
                _placeHolderText = value;
                SetPlaceholder();
            }
        }

        [DefaultValue(false)]
        public bool EnableNumericInput
        {
            get => _enableNumericInput;
            set => _enableNumericInput = value;
        }

        [DefaultValue(false)]
        public bool IsChangeHeight
        {
            get => _isChangeHeight;
            set
            {
                Multiline = value;
                WordWrap = !value;
                _isChangeHeight = value;
            }
        }

        public new string Text
        {
            get => _isPlaceHolder ? string.Empty : base.Text;
            set
            {
                if (!string.IsNullOrEmpty(value) && !_isPlaceHolder)
                {
                    RemovePlaceHolder();
                    base.Text = value;
                }
            }
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrEmpty(base.Text))
            {
                base.Text = _placeHolderText;
                ForeColor = Color.Gray;
                _isPlaceHolder = true;
            }
        }

        private void RemovePlaceHolder()
        {
            if (_isPlaceHolder)
            {
                base.Text = "";

                ForeColor = _colorText;
                Font = new Font(Font, FontStyle.Regular);
                _isPlaceHolder = false;
            }
        }

        private void InputNumbers(KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void SetPlaceholderHandler(object sender, EventArgs e)
        {
            SetPlaceholder();
        }

        private void RemovePlaceHolderHandler(object sender, EventArgs e)
        {
            RemovePlaceHolder();
        }

        private void KeyPressTextBoxHandler(object sender, KeyPressEventArgs e)
        {
            if (_enableNumericInput)
            {
                InputNumbers(e);
            }

            if (_isChangeHeight && e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
            }
        }
    }
}
