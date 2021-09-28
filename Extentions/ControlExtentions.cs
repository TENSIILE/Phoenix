using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Phoenix.Extentions
{
    public static class ControlExtentions
    {
        /// <summary>
        /// A method for expanding an array of components, where you can extract specific components by their name.
        /// </summary>
        public static T[] Extract<T>(this T[] controls, params string[] nameExceptions) where T : Control
        {
            List<T> list = controls.ToList();

            foreach (string exception in nameExceptions)
            {
                int index = list.FindIndex(control => control.Name == exception);

                list.RemoveAt(index);
            }

            return list.ToArray();
        }
    }
}
