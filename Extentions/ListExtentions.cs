using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.Extentions
{
    public static class ListExtentions
    {
        /// <summary>
        /// Adds all passed parameters to the list.
        /// </summary>
        public static void AddMultiple<T>(this List<T> list, params T[] parameters)
        {
            foreach (T item in parameters)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Concatenates the elements of the list into a string.
        /// </summary>
        public static string Unite<T>(this List<T> list, string separator)
        {
            return string.Join(separator, list);
        }

        /// <summary>
        /// Applies the function <see сref="DelegateMap{T,R}" /> <paramref name="@delegate" /> to each item in its argument list,
        /// by issuing a list of results as a return value.
        /// </summary>
        public static List<R> Map<T, R>(this List<T> list, DelegateMap<T, R> @delegate)
        {
            List<R> result = new List<R>();

            foreach (T item in list)
            {
                result.Add(@delegate.Invoke(item));
            }

            return result;
        }

        public delegate R DelegateMap<T, R>(T value);

        /// <summary>
        /// Used to create a new iterator from an existing iterable object,
        /// which effectively filters the elements using the provided function <see cref="DelegateFilter {T}" /> <paramref name="@ delegateFilter" />.
        /// </summary>
        public static List<T> Filter<T>(this List<T> list, DelegateFilter<T> @delegateFilter)
        {
            List<T> result = new List<T>();

            foreach (T item in list)
            {
                if (@delegateFilter.Invoke(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public delegate bool DelegateFilter<T>(T value);

        /// <summary>
        /// Returns a new list, expanding the passed lists into it.
        /// </summary>
        public static List<T> Spread<T>(this List<T> list, params List<T>[] joiningItem)
        {
            List<T> result = _Spread(list);

            foreach (List<T> listItem in joiningItem)
            {
                foreach (T item in listItem)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a new list with the addition of new elements of the same type.
        /// </summary>
        public static List<T> Spread<T>(this List<T> list, params T[] joiningItem)
        {
            List<T> result = _Spread(list);

            foreach (T item in joiningItem)
            {
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Returns a new list.
        /// </summary>
        public static List<T> Spread<T>(this List<T> list)
        {
            return _Spread(list);
        }

        private static List<T> _Spread<T>(List<T> list)
        {
            List<T> result = new List<T>();

            foreach (T item in list) result.Add(item);

            return result;
        }

        /// <summary>
        /// Returns a new inverted list.
        /// </summary>
        public static List<T> Reversed<T>(this List<T> list)
        {
            List<T> result = list.Spread();

            result.Reverse();

            return result;
        }

        /// <summary>
        /// Returns a new flipped list, reordering the elements in the specified range.
        /// </summary>
        public static List<T> Reversed<T>(this List<T> list, int index, int count)
        {
            List<T> result = list.Spread();

            result.Reverse(index, count);

            return result;
        }

        /// <summary>
        /// The method returns a new array containing a copy of a portion of the original array.
        /// </summary>
        public static List<T> Slice<T>(this List<T> list, int indexFrom, int indexTo)
        {
            int length;

            if (indexTo < 0)
            {
                length = (list.Count + indexTo) - indexFrom;
            }
            else
            {
                length = indexTo - indexFrom;
            }

            T[] result = new T[length];

            Array.Copy(list.ToArray(), indexFrom, result, 0, length);

            return result.ToList();
        }

        public delegate T ReduceCallback<T, R>(T acc, R element, int index);

        /// <summary>
        /// The method applies the reducer function to each element of the array (from left to right), returning one result value.
        /// </summary>
        public static R Reduce<T, R>(this List<T> list, ReduceCallback<R, T> callback, R acc)
        {
            R result = acc;

            for (int i = 0; i < list.Count; i++)
            {
                result = callback(result, list[i], i);
            }

            return result;
        }
    }
}