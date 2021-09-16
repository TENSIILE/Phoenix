using System;

namespace Phoenix.Extentions
{
    public static class StringExtentions
    {
        /// <summary>
        /// Converts the string representation of a number to its floating point equivalent.
        /// </summary>
        public static double ToDouble(this string self)
        {
            return Convert.ToDouble(self);
        }

        /// <summary>
        /// Converts the string representation of a number to an equivalent 32-bit integer.
        /// </summary>
        public static int ToInt(this string self)
        {
            return Convert.ToInt32(self);
        }
    }
}