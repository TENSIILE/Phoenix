using System.Collections.Generic;

namespace Phoenix._System
{
    /// <summary>
    /// A private list that allows you to only get data from the list.
    /// </summary>
    public class PrivateList<T>
    {
        private List<T> _list = new List<T>();

        public PrivateList(params T[] values)
        {
            _list.AddRange(values);
        }

        /// <summary>
        /// Returns an element from a list by index.
        /// </summary>
        public T Get(int index)
        {
            return _list[index];
        }

        internal void Add(T[] array)
        {
            _list.AddRange(array);
        }

        internal void Add(T element)
        {
            _list.Add(element);
        }
    }
}
