using System;

namespace DKD.Core.Lucene.LuceneException
{
    public class LuceneException : Exception
    {
        public LuceneException()
        {
        }

        public LuceneException(string message) : base(message)
        {
        }

        public LuceneException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LuceneException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}