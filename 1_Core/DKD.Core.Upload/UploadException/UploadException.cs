using System;

namespace DKD.Core.Upload.UploadException
{
    public class UploadException : Exception
    {
        public UploadException() { }
        public UploadException(string message) : base(message) { }
        public UploadException(string message, Exception inner) : base(message, inner) { }
        protected UploadException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
