using System;

namespace DKD.Core.Message
{
    public class EmailException : Exception
    {
        public EmailException() { }
        public EmailException(string message) : base(message) { }
        public EmailException(string message, Exception inner) : base(message, inner) { }
        protected EmailException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
