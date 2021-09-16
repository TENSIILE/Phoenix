using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Phoenix.Extentions;

namespace Phoenix.Core.Mounting
{
    public static class Mounter
    {
        private static Dictionary<string, OptionsForm> _optionsForm = new Dictionary<string, OptionsForm>();

        /// <summary>
        /// A method for attaching a form component to a specified panel.
        /// </summary>
        public static void MountComponent(Panel panel, PhoenixForm childForm)
        {
            UnmountComponent(panel);

            OptionsForm options = new OptionsForm(childForm.FormBorderStyle);

            _optionsForm.AddWithReplacement(childForm.Name, options);

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel.Controls.Add(childForm);
            panel.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// A method to detach a form component from a specified panel.
        /// </summary>
        public static void UnmountComponent(Panel panel)
        {
            if (panel.Controls.Count.ToBool())
            {
                PhoenixForm form = panel.Controls.OfType<PhoenixForm>().First();

                form.FormBorderStyle = _optionsForm.Get(form.Name).FormBorderStyle;

                form.Hide();

                panel.Controls.Clear();
                form.TopLevel = true;

                form.Dock = DockStyle.None;
            }

            panel.Controls.Clear();
            panel.Tag = null;
        }

        /// <summary>
        /// A method that checks whether a component is mounted or not.
        /// </summary>
        public static bool IsMountedComponent(Panel panel)
        {
            return panel.Controls.Count > 0 ? true : false;
        }
    }
}
