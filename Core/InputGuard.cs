using System;
using System.Windows.Forms;

namespace Phoenix.Core
{
    public delegate void GuardDelegate(TextBox textBox);

    public static class InputGuard
    {
        private static void Defend(TextBox textBox, Action<KeyPressEventArgs> action)
        {
            textBox.KeyPress += (object sender, KeyPressEventArgs e) =>
            {
                if (Convert.ToInt32(e.KeyChar) == 8)
                    return;

                action(e);
            };
        }

        /// <summary>
        /// A method that restricts input to anything but numbers.
        /// </summary>
        public static void OnlyDigit(TextBox textBox)
        {
            Defend(textBox, (e) =>
            {
                if (!Char.IsDigit(e.KeyChar))
                    e.Handled = true;
            });
        }

        /// <summary>
        /// Method restricting the input of only numbers in a text box.
        /// </summary>
        public static void WithoutDigit(TextBox textBox)
        {
            Defend(textBox, (e) =>
            {
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = true;
            });
        }
    }
}
