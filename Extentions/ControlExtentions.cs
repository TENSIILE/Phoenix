using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Phoenix.Core;

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

        /// <summary>
        /// Method to add guards for text field components.
        /// </summary>
        public static void AddGuards(this TextBox textBox, params GuardDelegate[] guardDelegates) 
        {
            guardDelegates.ToList().ForEach(guardDelegate => guardDelegate(textBox));
        }

        /// <summary>
        /// A method that sets a value to a component.
        /// </summary>
        public static void SetState(this Control control, string key)
        {
            PhoenixForm form = control.FindForm() as PhoenixForm;

            Store store = form.Store;

            Memo memo = new Memo(store);

            Ensurer ensurer = new Ensurer(form);

            Action action = () =>
            {
                ensurer.Insure<string>(key, (value) =>
                {
                    control.Text = value;
                }, StoreTypes.LOCAL);
            };

            if (store.GetState.ContainsKey(key) && control.Text.ToString() != Convert.ToString(store.GetState[key]))
            {
                action();
                return;
            }

            memo.Memoize(action, Memo.Watch(key));
        }
    }
}
