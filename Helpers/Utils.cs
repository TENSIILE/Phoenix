using System;
using System.Text;
using System.Reflection;
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

        internal static void ProtectedConstraintOnIntOrDouble<T>(T value, bool outflank = false)
        {
            if (!CompareOr(TypeMatchers.IsInt(value), TypeMatchers.IsDouble(value)) && !outflank)
            {
                throw new ArgumentException("The method only accepts Int and Double types!");
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
        public static string GetUniqueId()
        {
            DateTime date = new DateTime(1970, 1, 1);

            DateTime current = DateTime.Now;

            TimeSpan span = current - date;

            span.TotalMilliseconds.ToString();

            using (SHA256 sha256Hash = SHA256.Create())
            {
                string hash = GetHash(sha256Hash, span.TotalMilliseconds.ToString());

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
