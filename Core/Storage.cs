﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Phoenix.Json;
using Phoenix.Extentions;

namespace Phoenix.Core
{
    public class Storage : Dictionary<string, dynamic>
    {
        public Storage(Dictionary<string, dynamic> store) : base(store) { }

        /// <summary>
        /// A method to expand the entire store.
        /// </summary>
        public string Scan()
        {
            return this.ToDictionary(d => d.Key).ToJson();
        }

        /// <summary>
        /// Returns the value from storage corresponding to the passed component.
        /// </summary>
        public T GetByComponent<T>(Control component)
        {
            return this[component.Name.ToString()];
        }

        /// <summary>
        /// Returns the value from storage corresponding to the passed state.
        /// </summary>
        public T GetByState<T>(State<T> state)
        {
            return this[state.Name.ToString()];
        }

        /// <summary>
        /// Returns the value from storage corresponding to the passed reducer.
        /// </summary>
        public T GetByReducer<T>(Reducer<T> reducer)
        {
            return this[reducer.Name.ToString()];
        }

        /// <summary>
        /// Returns the value from the Storage for the given key.
        /// </summary>
        public T Take<T>(string key)
        {
            try
            {
                return (T)Convert.ChangeType(this.Get(key), typeof(T));
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException($@"Unable to convert type {((object)this.Get(key)).GetType()} to type {typeof(T).ToString()}!");
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("A value with such a key does not exist in the storage!");
            }
        }

        internal void AddWithReplace<T>(string key, T value)
        {
            this.AddWithReplacement(key, value);
        }
    }
}
