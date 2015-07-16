using System;

namespace DKD.Framework.Data
{
    public class ContextException : Exception
    {
        public ContextException() { }
        public ContextException(string message) : base(message) { }
        public ContextException(string message, Exception inner) : base(message, inner) { }
        protected ContextException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
