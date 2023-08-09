using System;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography;
using Phoenix.Core;

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

        /// <summary>
        /// The method of comparing elements and finding one true one.
        /// </summary>
        public static bool CompareOrEqual<T>(T obj, params T[] expressions)
        {
            return expressions.ToList().FindIndex(el => el.ToString() == obj.ToString()) != -1;
        }

        internal static void ProtectedConstraintOnNumber<T>(T value, bool outflank = false)
        {
            if (
                !CompareOr(
                    TypeMatchers.IsInt(value),
                    TypeMatchers.IsDouble(value),
                    TypeMatchers.IsString(value),
                    TypeMatchers.IsFloat(value)
                ) && !outflank
            )
            {
                throw new PhoenixException(
                    "This method only accepts Int or Double or Float or String types!",
                    new ArgumentException()
                );
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
        /// Method generating unique id by type.
        /// </summary>
        public static Guid GuidForType<T>()
        {
            return System.Runtime.InteropServices.Marshal.GenerateGuidForType(typeof(T));
        }

        /// <summary>
        /// Method that generates a unique ID from a timestamp.
        /// </summary>
        public static string UuidV1(string substring = null)
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
