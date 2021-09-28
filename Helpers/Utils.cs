using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Phoenix.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// A method of comparing all passed values as true.
        /// </summary>
        public static bool CompareAnd(params bool[] expressions)
        {
            return !string.Join(" ", expressions).Contains("False");
        }

        /// <summary>
        /// A method for comparing all passed arguments and finding at least one true one.
        /// </summary>
        public static bool CompareOr(params bool[] expressions)
        {
            return string.Join(" ", expressions).Contains("True");
        }

        /// <summary>
        /// The method of comparing elements and finding one true one.
        /// </summary>
        public static bool CompareOrEqual<T>(T obj, List<T> expressions)
        {
            return expressions.FindIndex(el => el.ToString() == obj.ToString()) != -1;
        }

        internal static void ProtectedConstraintOnNumber<T>(T value, bool outflank = false)
        {
            if (!CompareOr(TypeMatchers.IsInt(value), TypeMatchers.IsDouble(value), TypeMatchers.IsFloat(value)) && !outflank)
            {
                throw new ArgumentException("This method only accepts Int or Double or Float types!");
            }
        }

        /// <summary>
        /// A method that checks the existence of a method for the passed type.
        /// </summary>
        public static bool ExistsMethod<T>(string methodName)
        {
            Type type = typeof(T);

            MethodInfo[] methodInfo = type.GetMethods();

            foreach (MethodInfo info in methodInfo)
            {
                if (info.Name == methodName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// A method that checks for the existence of a property for the passed type.
        /// </summary>
        public static bool ExistsProperty<T>(string propertyName)
        {
            Type type = typeof(T);

            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            return !TypeMatchers.IsNull(propertyInfo);
        }

        /// <summary>
        /// A method that generates a unique id.
        /// </summary>
        public static string GetUniqueId(string substring = null)
        {
            DateTime date = new DateTime(1970, 1, 1);

            DateTime current = DateTime.Now;

            TimeSpan span = current - date;

            span.TotalMilliseconds.ToString();

            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = GetHash(sha256Hash, span.TotalMilliseconds.ToString() + substring);

                return hash;
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
