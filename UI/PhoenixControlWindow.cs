using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Phoenix.UI
{
    public partial class PhoenixControlWindow : UserControl
    {
        private Color _mouseEnterColorButtons = Color.FromArgb(33, 33, 33);
        private Color _mouseDownColorButtons = Color.DodgerBlue;
        private Color _backColorHeader = Color.FromArgb(22, 22, 22);
        private Color _foreColorTextCaption = Color.White;
        private string _textCaption = "Новое окно";

        private double _opacity = 1;
        private Point _mouseLocation;

        private bool _isEnableCloseButton = true;
        private bool _isEnableMaxButton = true;
        private bool _isEnableMinButton = true;

        private bool _isSetCursorPointer = false;

        private Image _logo;

        private Image _iconClose = Properties.Resources.icon_close;
        private Image _iconMax = Properties.Resources.icon_maximize;
        private Image _iconMin = Properties.Resources.icon_minimize;

        /// <summary>
        /// The color that appears when you hover over the form control buttons.
        /// </summary>
        [Description("The color that appears when you hover over the form control buttons.")]
        [DefaultValue("33;33;33")]
        public Color MouseEnterColorButtons
        {
            get => _mouseEnterColorButtons;
            set => _mouseEnterColorButtons = value;
        }

        /// <summary>
        /// The color displayed when you click on the form control buttons.
        /// </summary>
        [Description("The color displayed when you click on the form control buttons.")]
        [DefaultValue("DodgerBlue")]
        public Color MouseDownColorButtons
        {
            get => _mouseDownColorButtons;
            set => _mouseDownColorButtons = value;
        }

        /// <summary>
        /// The standard color of the form header.
        /// </summary>
        [Description("The standard color of the form header.")]
        [DefaultValue("White")]
        public Color ForeColorTextCaption
        {
            get => _foreColorTextCaption;
            set => _foreColorTextCaption = value;
        }

        /// <summary>
        /// The transparency of the form as it moves across the screen.
        /// </summary>
        [Description("The transparency of the form as it moves across the screen.")]
        [DefaultValue(1)]
        public double OpacityForm
        {
            get => _opacity;
            set => _opacity = value;
        }

        /// <summary>
        /// The default background color for the entire component container.
        /// </summary>
        [Description("The default background color for the entire component container.")]
        [DefaultValue("22;22;22")]
        public Color BackColorHeader
        {
            get => _backColorHeader;
            set
            {
                _backColorHeader = value;
                menu.BackColor = _backColorHeader;
                Invalidate();
            }
        }

        /// <summary>
        /// The standard name for the form.
        /// </summary>
        [Description("The standard name for the form.")]
        [DefaultValue("Новое окно")]
        public string TextCaption
        {
            get => _textCaption;
            set 
            {
                _textCaption = value;
                labelCaption.Text = _textCaption;
                Invalidate();
            }
        }

        /// <summary>
        /// Sets whether to display the form close button.
        /// </summary>
        [Description("Sets whether to display the form close button.")]
        [DefaultValue(true)]
        public bool IsEnableCloseButton
        {
            get => _isEnableCloseButton;
            set
            {
                _isEnableCloseButton = value;
                pictureClose.Enabled = _isEnableCloseButton;
                pictureClose.Visible = _isEnableCloseButton;
                Invalidate();
            }
        }

        /// <summary>
        /// Sets whether to display the expand button of the form.
        /// </summary>
        [Description("Sets whether to display the expand button of the form.")]
        [DefaultValue(true)]
        public bool IsEnableMaxButton
        {
            get => _isEnableMaxButton;
            set
            {
                _isEnableMaxButton = value;
                pictureMaximize.Enabled = _isEnableMaxButton;
                pictureMaximize.Visible = _isEnableMaxButton;
                Invalidate();
            } 
        }

        /// <summary>
        /// Sets whether to show the hide button of the form.
        /// </summary>
        [Description("Sets whether to show the hide button of the form.")]
        [DefaultValue(true)]
        public bool IsEnableMinButton
        {
            get => _isEnableMinButton;
            set
            {
                _isEnableMinButton = value;
                pictureMinimize.Enabled = _isEnableMinButton;
                pictureMinimize.Visible = _isEnableMinButton;
                Invalidate();
            }
        }

        /// <summary>
        /// Sets whether the cursor is displayed as a pointer when hovering over form control buttons.
        /// </summary>
        [Description("Sets whether the cursor is displayed as a pointer when hovering over form control buttons.")]
        [DefaultValue(false)]
        public bool IsSetCursorPointer
        {
            get => _isSetCursorPointer;
            set
            {
                _isSetCursorPointer = value;

                if (_isSetCursorPointer)
                {
                    pictureClose.Cursor = Cursors.Hand;
                    pictureMaximize.Cursor = Cursors.Hand;
                    pictureMinimize.Cursor = Cursors.Hand;
                }
                else
                {
                    pictureClose.Cursor = Cursors.Default;
                    pictureMaximize.Cursor = Cursors.Default;
                    pictureMinimize.Cursor = Cursors.Default;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// Logo form.
        /// </summary>
        [Description("Logo form.")]
        public Image Logo
        {
            get => _logo;
            set
            {
                _logo = value;
                pictureIcon.Image = _logo;
                
                Invalidate();
            }
        }

        /// <summary>
        /// Icon of the button to close the form.
        /// </summary>
        [Description("Icon of the button to close the form.")]
        public Image IconCloseForm
        {
            get => _iconClose;
            set
            {
                _iconClose = value;
                pictureClose.Image = _iconClose;
                Invalidate();
            }
        }

        /// <summary>
        /// Icon of the button to maximize the form.
        /// </summary>
        [Description("Icon of the button to maximize the form.")]
        public Image IconMaxForm
        {
            get => _iconMax;
            set
            {
                _iconMax = value;
                pictureMaximize.Image = _iconMax;
                Invalidate();
            }
        }

        /// <summary>
        /// Icon of the button to hide the form.
        /// </summary>
        [Description("Icon of the button to hide the form.")]
        public Image IconMinForm
        {
            get => _iconMin;
            set
            {
                _iconMin = value;
                pictureMinimize.Image = _iconMin;
                Invalidate();
            }
        }

        public PhoenixControlWindow()
        {
            InitializeComponent();

            menu.BackColor = _backColorHeader;
            pictureIcon.Enabled = false;
            Dock = DockStyle.Top;
        }

        private void ChangeSizeForm()
        {
            if (Form.ActiveForm.WindowState == FormWindowState.Maximized)
            {
                Form.ActiveForm.WindowState = FormWindowState.Normal;
            }
            else
            {
                Form.ActiveForm.WindowState = FormWindowState.Maximized;
            }
        }

        private void PictureClose_MouseEnter(object sender, EventArgs e)
        {
            pictureClose.BackColor = _mouseEnterColorButtons;
        }

        private void PictureClose_MouseLeave(object sender, EventArgs e)
        {
            pictureClose.BackColor = Color.Transparent;
        }

        private void PictureClose_MouseDown(object sender, MouseEventArgs e)
        {
            pictureClose.BackColor = Color.Crimson;
        }

        private void PictureClose_MouseUp(object sender, MouseEventArgs e)
        {
            pictureClose.BackColor = Color.Transparent;
            Form.ActiveForm.Close();
        }

        private void PictureMaximize_MouseEnter(object sender, EventArgs e)
        {
            pictureMaximize.BackColor = _mouseEnterColorButtons;
        }

        private void PictureMaximize_MouseLeave(object sender, EventArgs e)
        {
            pictureMaximize.BackColor = Color.Transparent;
        }

        private void PictureMaximize_MouseDown(object sender, MouseEventArgs e)
        {
            pictureMaximize.BackColor = _mouseDownColorButtons;
        }

        private void PictureMaximize_MouseUp(object sender, MouseEventArgs e)
        {
            pictureMaximize.BackColor = Color.Transparent;

            ChangeSizeForm();
        }

        private void PictureMinimize_MouseEnter(object sender, EventArgs e)
        {
            pictureMinimize.BackColor = _mouseEnterColorButtons;
        }

        private void PictureMinimize_MouseLeave(object sender, EventArgs e)
        {
            pictureMinimize.BackColor = Color.Transparent;
        }

        private void PictureMinimize_MouseDown(object sender, MouseEventArgs e)
        {
            pictureMinimize.BackColor = _mouseDownColorButtons;
        }

        private void PictureMinimize_MouseUp(object sender, MouseEventArgs e)
        {
            pictureMinimize.BackColor = Color.Transparent;
            Form.ActiveForm.WindowState = FormWindowState.Minimized;
        }

        private void LabelCaption_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLocation = new Point(-e.X, -e.Y);
            Form.ActiveForm.Opacity = OpacityForm;
        }

        private void LabelCaption_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = MousePosition;
                mousePos.Offset(_mouseLocation.X, _mouseLocation.Y);
                Form.ActiveForm.Location = mousePos;
            }
        }

        private void LabelCaption_MouseUp(object sender, MouseEventArgs e)
        {
            Form.ActiveForm.Opacity = 1;
        }

        private void LabelCaption_DoubleClick(object sender, EventArgs e)
        {
            if (Form.ActiveForm.MaximizeBox)
            {
                ChangeSizeForm();
            }
        }
    }
}
