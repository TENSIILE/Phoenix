using System;

namespace Phoenix.Helpers
{
    public class Converting
    {
        /// <summary>
        /// A method that converts an object to a boolean.
        /// </summary>
        public static bool ObjectToBoolean(object value)
        {
            if (TypeMatchers.IsBool(value))
            {
                throw new ArgumentException("This method does not accept Boolean types! Can't convert Boolean to Boolean!");
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
                    throw new ArgumentException("Unable to parse input string to Boolean!");
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
        public static bool IntOrDoubleToBoolean<T>(T value, bool outflank = false)
        {
            Utils.ProtectedConstraintOnIntOrDouble(value, outflank);

            return !(value.ToString() == 0.ToString());
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
    }
}
