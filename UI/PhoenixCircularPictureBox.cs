using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Phoenix.UI
{
    public class PhoenixCircularPictureBox : PictureBox
    {
        private int _borderSize = 2;
        private Color _borderColor = Color.Crimson;
        private DashStyle _borderLineStyle = DashStyle.Solid;

        public int BorderSize
        {
            get => _borderSize;
            set
            {
                _borderSize = value;
                Invalidate();
            }
        }
        
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }
        
        public PhoenixCircularPictureBox()
        {
            Size = new Size(100, 100);
            SizeMode = PictureBoxSizeMode.StretchImage;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(Width, Width);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            var graph = pe.Graphics;
            var rectContourSmooth = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var rectBorder = Rectangle.Inflate(rectContourSmooth, - _borderSize, - _borderSize);
            var smoothSize = _borderSize > 0 ? _borderSize * 3 : 1;

            using (var borderGColor = new LinearGradientBrush(rectBorder, _borderColor, _borderColor, 50f))
            using (var pathRegion = new GraphicsPath())
            using (var penSmooth = new Pen(Parent.BackColor, smoothSize))
            using (var penBorder = new Pen(_borderColor, _borderSize))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                penBorder.DashStyle = _borderLineStyle;
                pathRegion.AddEllipse(rectContourSmooth);
               
                Region = new Region(pathRegion);
               
                graph.DrawEllipse(penSmooth, rectContourSmooth);

                if (_borderSize > 0)
                {
                    graph.DrawEllipse(penBorder, rectBorder);
                }
            }
        }
    }
}
