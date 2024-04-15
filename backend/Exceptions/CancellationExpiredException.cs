using System;

namespace backend.Exceptions {
    public class CancellationExpiredException : Exception
    {
        public CancellationExpiredException() : base("Cancellation period has expired.")
        {
        }

        public CancellationExpiredException(string message) : base(message)
        {
        }

        public CancellationExpiredException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}