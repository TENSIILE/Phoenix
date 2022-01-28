using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;

namespace Phoenix.Core
{
    public static class ListRender
    {
        /// <summary>
        /// Method for filtering and searching in real time.
        /// </summary>
        /// <param name="list">List of elements to search for.</param>
        /// <param name="text">The text that will be checked against the list items.</param>
        /// <param name="component">Component in which similar found elements will be displayed.</param>
        public static void Search(List<string> list, string text, dynamic component)
        {
            List<string> searched = list.ToList().FindAll(v => v.ToLower().Contains(text.ToLower()));

            try
            {
                component.Items.Clear();
                component.Items.AddRange(searched.ToArray());
            }
            catch (RuntimeBinderException)
            {
                throw new PhoenixException("The required methods were not found for the component!", new ArgumentException());
            }
        }

        /// <summary>
        /// Method for optimized rendering of items in the list.
        /// </summary>
        public static void OptimizeAdd<T, R>(State<List<T>> state, R[] list, Action<int> callback)
        {
            for (int i = 0; i < state.Value.Count; i++)
            {
                if (ShouldUpdate(list, i))
                {
                    callback(i);
                }
            }
        }

        private static bool ShouldUpdate<T>(T[] list, int index)
        {
            return list.Length > index ? false : true;
        }
    }
}
