using System;
using System.Collections.Generic;
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
        /// A method that returns data from the provider using a unique key.
        /// </summary>
        public T Get<T>(string key)
        {
            return (T)Convert.ChangeType(_provider.Get(key), typeof(T));
        }
    }
}