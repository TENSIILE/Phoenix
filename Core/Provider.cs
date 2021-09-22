using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public class Provider
    {
        private Dictionary<string, dynamic> _provider = new Dictionary<string, dynamic>();

        /// <summary>
        /// A method that adds any data to the provider.
        /// </summary>
        public void Add(string key, dynamic value)
        {
            _provider.Add(key, value);
        }

        /// <summary>
        /// The method adds a component to the provider.
        /// </summary>
        public void Add(Control control)
        {
            _provider.Add(control.Name, control);
        }

        /// <summary>
        /// The method adds an unlimited number of components to the provider.
        /// </summary>
        public void Add(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                _provider.Add(control.Name, control);
            }
        }

        /// <summary>
        /// A method that returns data from the provider using a unique key.
        /// </summary>
        public T Take<T>(string key)
        {
            try
            {
                return (T)Convert.ChangeType(_provider.Get(key), typeof(T));
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($@"The provider does not have an object with such a key - {key}!");
            }
        }
    }
}