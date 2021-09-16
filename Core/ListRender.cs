using System;
using System.Collections.Generic;

namespace Phoenix.Core
{
    public static class ListRender
    {
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
