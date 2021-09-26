using System.Collections.Generic;
using Phoenix.Extentions;

namespace Phoenix
{
    internal class PContainerFormsType : Dictionary<string, PhoenixForm> { }

    public static class PContainer
    {
        private static PContainerFormsType _phoenixListForms = new PContainerFormsType();

        /// <summary>
        /// The method returns the form by its name.
        /// </summary>
        public static PhoenixForm Get(string nameForm)
        {
            try
            {
                return _phoenixListForms.Get(nameForm);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($@"The container does not have a form with such a key - {nameForm}");
            }
        }

        /// <summary>
        /// Adds the form to the list.
        /// </summary>
        public static void Append(string key, PhoenixForm value)
        {
            _phoenixListForms.Add(key, value);
        }

        /// <summary>
        /// Removes the form from the list.
        /// </summary>
        public static void Delete(string key)
        {
            _phoenixListForms.Remove(key);
        }
    }
}
