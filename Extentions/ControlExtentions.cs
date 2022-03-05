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
        public static void SetState(this Control control, string name)
        {
            _SetState(control, "Text", name);
        }

        /// <summary>
        /// Method that sets the value for the component to the desired property.
        /// </summary>
        public static void SetState(this Control control, string property, string name)
        {
            _SetState(control, property, name);
        }

        private static void _SetState(Control control, string property, string name)
        {
            PhoenixForm form = control.FindForm() as PhoenixForm;

            Store store = form.Store;

            Memo memo = new Memo(store);

            Ensurer ensurer = new Ensurer(form);

            Action action = () =>
            {
                ensurer.Insure<string>(name, (value) =>
                {
                    control.GetType().GetProperty(property).SetValue(control, value);
                    control.Text = value;
                }, StoreTypes.LOCAL);
            };

            if (store.GetState.ContainsKey(name) && control.Text.ToString() != Convert.ToString(store.GetState[name]))
            {
                action();
                return;
            }

            memo.Memoize(action, Memo.Watch(name));
        }
    }
}
