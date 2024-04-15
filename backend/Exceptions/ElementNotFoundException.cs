using System;

namespace backend.Exceptions {
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() : base("Element doesn't exist")
        {
        }

        public ElementNotFoundException(string message) : base(message)
        {
        }

        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}