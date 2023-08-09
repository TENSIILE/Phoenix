using System;
using System.Linq;
using Phoenix.Core;
using Phoenix.Extentions;

namespace Phoenix.Addons
{
    public static class Path
    {
        private static char _backSlash = '\\';

        /// <summary>
        /// A method combining local and global paths.
        /// </summary>
        public static string Join(string path)
        {
            return DIRNAME + _backSlash + path;
        }

        /// <summary>
        /// A method that allows you to combine the passed strings into a full path.
        /// </summary>
        public static string Combine(params string[] parameters)
        {
            return string.Join(_backSlash.ToString(), parameters);
        }

        /// <summary>
        /// The method exits the folder backwards according to the transferred count.
        /// </summary>
        public static string Back(string path, int count)
        {
            string[] list = path.Split(_backSlash);

            if (list.Length < count || count < 0)
            {
                throw new PhoenixException(
                    "The count cannot be more than the number of folders in the path. And also less than zero!",
                    new ArgumentOutOfRangeException()
                );
            }

            return Combine(list.ToList().Slice(0, list.Length - count).ToArray());
        }

        /// <summary>
        /// The accessor returns the full path to the folder.
        /// </summary>
        public static string DIRNAME => System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
    }
}
