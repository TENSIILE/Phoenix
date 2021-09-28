using System.Windows.Forms;

namespace Phoenix.Core.Mounting
{
    internal class FormSettings
    {
        public FormBorderStyle FormBorderStyle { get; private set; }

        public FormSettings(FormBorderStyle formBorderStyle)
        {
            FormBorderStyle = formBorderStyle;
        }
    }
}
