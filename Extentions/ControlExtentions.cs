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
        /// Method that sets the value for the component to the desired property.
        /// </summary>
        public static void SetState<T>(this Control control, Observer<T> observerState, string property = "Text")
        {
            _SetState<T>(control, observerState.Name, property);
        }

        /// <summary>
        /// Method that sets the value for the component to the desired property.
        /// </summary>
        public static void SetState<T>(this Control control, string name, string property = "Text")
        {
            _SetState<T>(control, name, property);
        }

        private static void _SetState<T>(this Control control, string name, string property)
        {
            PhoenixForm form = control.FindForm() as PhoenixForm;

            Store store = form.Store;

            Memo memo = new Memo(store);

            Ensurer ensurer = new Ensurer(form);

            Action action = () =>
            {
                ensurer.Insure<T>(name, (value) =>
                {
                    control.GetType().GetProperty(property).SetValue(control, value);
                }, StoreTypes.LOCAL);
            };

            if
            (
                store.GetState.ContainsKey(name)
                && control.GetType().GetProperty(property).GetValue(control).ToString() != Convert.ToString(store.GetState[name])
            )
            {
                action();
                return;
            }

            memo.Memoize(action, Memo.Watch(name));
        }
    }
}
