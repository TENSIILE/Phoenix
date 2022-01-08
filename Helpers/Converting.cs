using System;
using Phoenix.Core;

namespace Phoenix.Helpers
{
    public static class Converting
    {
        /// <summary>
        /// A method that converts an object to a boolean.
        /// </summary>
        public static bool ObjectToBoolean(object value)
        {
            if (TypeMatchers.IsBool(value))
            {
                return Convert.ToBoolean(value);
            }

            return !TypeMatchers.IsNullOrEmpty(value);
        }

        /// <summary>
        /// A method that converts an object to a double.
        /// </summary>
        public static double ObjectToDouble(object value)
        {
            if (Utils.CompareAnd(!TypeMatchers.IsNullOrEmpty(value), !TypeMatchers.IsString(value)))
            {
                return Convert.ToDouble(value);
            }

            return double.NaN;
        }

        /// <summary>
        /// A method for converting a boolean value in a string to a boolean type.
        /// </summary>
        public static bool StringParseToBoolean(string value)
        {
            string _value = value.ToLower();

            switch (_value)
            {
                case "true":
                case "false":
                    return _value == "true";
                default:
                    throw new PhoenixException(
                        "Unable to parse input string to Boolean!",
                        new ArgumentException()
                    );
            }
        }

        /// <summary>
        /// A method that converts a string to a boolean value.
        /// </summary>
        public static bool StringToBoolean(string value)
        {
            return !TypeMatchers.IsEmpty(value);
        }

        /// <summary>
        ///A method that converts an integer and double to a boolean value.
        /// </summary>
        public static bool NumberToBoolean<T>(T value, bool outflank = false)
        {
            Utils.ProtectedConstraintOnNumber(value, outflank);

            return !(value.ToString() == "0");
        }

        /// <summary>
        /// A method that converts an null to a boolean.
        /// </summary>
        public static bool NullToBoolean(object value)
        {
            return !TypeMatchers.IsNull(value);
        }

        /// <summary>
        /// A method that converts an null to a string.
        /// </summary>
        public static string NullToString(object value)
        {
            return TypeMatchers.IsNull(value) ? "" : Convert.ToString(value);
        }

        /// <summary>
        /// A method that converts an error to false when it is triggered.
        /// </summary>
        public static bool ErrorToBoolean<T>(DelegateErrorToBoolean callback) where T : Exception
        {
            try
            {
                callback();
                return true;
            }
            catch (T)
            {
                return false;
            }
        }

        public delegate object DelegateErrorToBoolean();

        /// <summary>
        /// Returns an object of the specified type whose value is equivalent to the specified type.
        /// </summary>
        public static T ToType<T>(dynamic argument)
        {
            return (T)Convert.ChangeType(argument, typeof(T));
        }
    }
}
