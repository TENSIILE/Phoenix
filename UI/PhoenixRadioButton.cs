using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Phoenix.UI
{
    public class PhoenixRadioButton : RadioButton
    {
        private Color _checkedColor = Color.Crimson;
        private Color _unCheckedColor = Color.Gray;

        public Color CheckedColor
        {
            get => _checkedColor;
            set
            {
                _checkedColor = value;
                Invalidate();
            }
        }

        public Color UnCheckedColor
        {
            get => _unCheckedColor;
            set
            {
                _unCheckedColor = value;
                Invalidate();
            }
        }

        public PhoenixRadioButton()
        {
            MinimumSize = new Size(0, 21);
            Padding = new Padding(10, 0, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            float rbBorderSize = 18F;
            float rbCheckSize = 12F;

            RectangleF rectRbBorder = new RectangleF()
            {
                X = 0.5F,
                Y = (Height - rbBorderSize) / 2,
                Width = rbBorderSize,
                Height = rbBorderSize
            };

            RectangleF rectRbCheck = new RectangleF()
            {
                X = rectRbBorder.X + ((rectRbBorder.Width - rbCheckSize) / 2),
                Y = (Height - rbCheckSize) / 2, 
                Width = rbCheckSize,
                Height = rbCheckSize
            };

            using (Pen penBorder = new Pen(_checkedColor, 1.6F))
            using (SolidBrush brushRbCheck = new SolidBrush(_checkedColor))
            using (SolidBrush brushText = new SolidBrush(ForeColor))
            {
                graphics.Clear(BackColor);

                if (Checked)
                {
                    graphics.DrawEllipse(penBorder, rectRbBorder);
                    graphics.FillEllipse(brushRbCheck, rectRbCheck);
                }
                else
                {
                    penBorder.Color = _unCheckedColor;
                    graphics.DrawEllipse(penBorder, rectRbBorder);
                }
               
                graphics.DrawString(Text, Font, brushText, rbBorderSize + 8, (Height - TextRenderer.MeasureText(Text, Font).Height) / 2);
            }
        }
    }
}
