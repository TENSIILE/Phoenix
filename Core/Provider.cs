using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Phoenix.Extentions;
using Phoenix.Json;

namespace Phoenix.Core
{
    public class Provider
    {
        private readonly Dictionary<string, dynamic> _provider = new Dictionary<string, dynamic>();
        private readonly List<UpdatedCallback> providerUpdatedCallbacks = new List<UpdatedCallback>();

        /// <summary>
        /// A method that adds any data to the provider.
        /// </summary>
        public void Add(string key, object value)
        {
            _provider.AddWithReplacement(key, value);

            providerUpdatedCallbacks.ForEach(callback => callback(key));
        }

        /// <summary>
        /// The method adds a component to the provider.
        /// </summary>
        public void Add(Control control)
        {
            Add(control.Name, control);
        }

        /// <summary>
        /// The method adds an unlimited number of components to the provider.
        /// </summary>
        public void Add(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                Add(control.Name, control);
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

        /// <summary>
        /// The delegate for the method UpdatedFor.
        /// </summary>
        public delegate void UpdatedCallback(string key);

        /// <summary>
        /// Method subscribing to data updates in the provider.
        /// </summary>
        public void UpdatedFor(UpdatedCallback callback)
        {
            providerUpdatedCallbacks.Add(callback);
        }

        /// <summary>
        /// A method that converts provider data into a string.
        /// </summary>
        public override string ToString()
        {
            return _provider.ToJson();
        }
    }
}