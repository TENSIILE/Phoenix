using System.Collections.Generic;

namespace Phoenix.Extentions
{
    public static class DictionaryExtentions
    {
        /// <summary>
        /// Checks for the presence of a key in the dictionary. Returns if found, otherwise returns <paramref name="false" />.
        /// </summary>
        public static object Has<T, R>(this Dictionary<T, R> dict, T key)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return false;
        }

        /// <summary>
        /// Returns the value for the given key.
        /// </summary>
        public static R Get<T, R>(this Dictionary<T, R> dict, T key)
        {
            return dict[key];
        }

        /// <summary>
        /// Converts a dictionary to a two-dimensional array.
        /// </summary>
        public static dynamic[] Entries<T, R>(this Dictionary<T, R> dict)
        {
            List<dynamic[]> result = new List<dynamic[]>();

            foreach (T key in dict.Keys)
            {
                List<dynamic> item = new List<dynamic>();

                item.Add(key);
                item.Add(dict[key]);

                result.Add(item.ToArray());
            }

            return result.ToArray();
        }

        /// <summary>
        /// Adds a new item to the dictionary. If a value with the same key is found, it replaces the value with the new one.
        /// </summary>
        public static void AddWithReplacement<T>(this Dictionary<string, T> dict, string key, T value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
                return;
            }

            dict[key] = value;
        }

        /// <summary>
        /// Returns a new dictionary.
        /// </summary>
        public static Dictionary<T, R> Spread<T, R>(this Dictionary<T, R> dict)
        {
            return _Spread(dict);
        }

        /// <summary>
        /// Returns a new dictionary, expanding the old dictionary with new ones.
        /// </summary>
        public static Dictionary<T, R> Spread<T, R>(this Dictionary<T, R> dict, params Dictionary<T, R>[] joiningDicts)
        {
            Dictionary<T, R> result = _Spread(dict);

            foreach (Dictionary<T, R> additionalDict in joiningDicts)
            {
                foreach (KeyValuePair<T, R> item in additionalDict)
                {
                    if (item.Key.GetType() == typeof(string) && result.ContainsKey(item.Key))
                    {
                        result[item.Key] = item.Value;
                    }
                    else
                    {
                        result.Add(item.Key, item.Value);
                    }
                }
            }

            return result;
        }

        private static Dictionary<T, R> _Spread<T, R>(Dictionary<T, R> dict)
        {
            Dictionary<T, R> result = new Dictionary<T, R>();

            foreach (KeyValuePair<T, R> item in dict)
            {
                result.Add(item.Key, item.Value);
            }

            return result;
        }
    }
}
