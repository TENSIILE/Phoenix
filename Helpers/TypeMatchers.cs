using System;

namespace Phoenix.Helpers
{
    public static class TypeMatchers
    {
        private const string _NUMBER = "number";
        private const string _STRING = "string";
        private const string _BOOLEAN = "bool";
        private const string _DOUBLE = "double";
        private const string _FLOAT = "float";

        private static bool Matching<T>(T value, string type)
        {
            try
            {
                if (IsNull(value)) return false;

                switch (type)
                {
                    case _NUMBER:
                        return value.GetType() == typeof(int);
                    case _STRING:
                        return value.GetType() == typeof(string);
                    case _BOOLEAN:
                        return value.GetType() == typeof(bool);
                    case _DOUBLE:
                        return value.GetType() == typeof(double);
                    case _FLOAT:
                        return value.GetType() == typeof(float);
                    default:
                        throw new ArgumentException($@"The type [{type}] does not exist!");
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Method that checks if a value is a number.
        /// </summary>
        public static bool IsInt<T>(T value)
        {
            return Matching(value, _NUMBER);
        }

        /// <summary>
        /// Method that checks if a value is a double.
        /// </summary>
        public static bool IsDouble<T>(T value)
        {
            return Matching(value, _DOUBLE);
        }

        /// <summary>
        /// Method that checks if a value is a bool.
        /// </summary>
        public static bool IsBool<T>(T value)
        {
            return Matching(value, _BOOLEAN);
        }

        /// <summary>
        /// Method that checks if a value is a string.
        /// </summary>
        public static bool IsString<T>(T value)
        {
            return Matching(value, _STRING);
        }

        /// <summary>
        /// Method that checks if a value is a float.
        /// </summary>
        public static bool IsFloat<T>(T value)
        {
            return Matching(value, _FLOAT);
        }

        /// <summary>
        /// Method that checks if a value is a null.
        /// </summary>
        public static bool IsNull<T>(T value)
        {
            return value == null;
        }

        /// <summary>
        /// Method that checks if a value is a empty.
        /// </summary>
        public static bool IsEmpty<T>(T value)
        {
            return value?.ToString() == string.Empty;
        }

        /// <summary>
        /// Method that checks if a value is a empty or null.
        /// </summary>
        public static bool IsNullOrEmpty<T>(T value)
        {
            return string.IsNullOrEmpty(value?.ToString());
        }
    }
}
