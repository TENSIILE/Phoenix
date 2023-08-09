using System.Collections.Generic;
using Phoenix.Extentions;
using Phoenix.Helpers;

namespace Phoenix._System
{
    /// <summary>
    /// A private dictionary that allows you to only get data from the dictionary.
    /// </summary>
    public class PrivateDictionary<T>
    {
        protected Dictionary<string, T> _dict = new Dictionary<string, T>();

        public PrivateDictionary(Dictionary<string, T> dict)
        {
            if (TypeMatchers.IsNull(dict))
                return;

            _dict.Spread(dict);
        }

        /// <summary>
        /// Method for obtaining data by key.
        /// </summary>
        public T Get(string key)
        {
            return _dict[key];
        }

        internal void Add(string key, T value)
        {
            _dict.Add(key, value);
        }
    }
}
