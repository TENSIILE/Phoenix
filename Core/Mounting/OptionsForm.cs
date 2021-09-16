using System.Windows.Forms;

namespace Phoenix.Core.Mounting
{
    internal class OptionsForm
    {
        public FormBorderStyle FormBorderStyle { get; private set; }

        public OptionsForm(FormBorderStyle formBorderStyle)
        {
            FormBorderStyle = formBorderStyle;
        }
    }
}
