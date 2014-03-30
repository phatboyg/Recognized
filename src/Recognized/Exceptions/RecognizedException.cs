namespace Recognized
{
    using System;
    using System.Runtime.Serialization;


    [Serializable]
    public class RecognizedException :
        Exception
    {
        public RecognizedException()
        {
        }

        public RecognizedException(string message)
            : base(message)
        {
        }

        public RecognizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RecognizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}