﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Phoenix.Json;
using Phoenix.Extentions;
using Phoenix.Helpers;

namespace Phoenix.Core
{
    public class Storage : Dictionary<string, dynamic>
    {
        public Storage(Dictionary<string, dynamic> store) : base(store) { }

        /// <summary>
        /// A method to expand the entire Store. Returns <paramref name="Json" />.
        /// </summary>
        public string Scan()
        {
            return this.ToDictionary(d => d.Key).ToJson();
        }

        /// <summary>
        /// A simplified method for expanding the entire Store. Returns <paramref name="Json" />.
        /// </summary>
        public string SimpleScan()
        {
            return this.Spread().ToJson();
        }

        /// <summary>
        /// The method returns a value from Store corresponding to the passed component.
        /// </summary>
        public string GetByComponent(Control component)
        {
            return this[component.Name.ToString()];
        }

        /// <summary>
        /// The method returns a value from Store corresponding to the passed state.
        /// </summary>
        public T GetByState<T>(State<T> state)
        {
            return this[state.Name.ToString()];
        }

        /// <summary>
        /// The method returns a value from Store corresponding to the passed reducer.
        /// </summary>
        public T GetByReducer<T>(Reducer<T> reducer)
        {
            return this[reducer.Name.ToString()];
        }

        /// <summary>
        /// A method has been created that returns a value from the Store according to the passed observing state.
        /// </summary>
        public T GetBy<T>(Observer<T> observerState)
        {
            return this[observerState.Name.ToString()];
        }

        /// <summary>
        /// The method returns a value from the Store for the given key.
        /// </summary>
        public T Take<T>(string key)
        {
            try
            {
                return Converting.ToType<T>(this.Get(key));
            }
            catch (InvalidCastException)
            {
                throw new PhoenixException(
                    $@"Unable to convert type {((object)this.Get(key)).GetType()} to type {typeof(T).ToString()}!",
                    new InvalidCastException()
                );
            }
            catch (KeyNotFoundException)
            {
                throw new PhoenixException(
                    "A value with such a key does not exist in the storage!",
                    new KeyNotFoundException()
                );
            }
        }

        internal void AddWithReplace<T>(string key, T value)
        {
            this.AddWithReplacement(key, value);
        }
    }
}
