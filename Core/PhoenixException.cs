using System;

namespace Phoenix.Core
{
    public class PhoenixException : Exception
    {
        public PhoenixException(string message, Exception innerException = null)
            : base(message, innerException) { }
    }
}
