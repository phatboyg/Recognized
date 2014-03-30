namespace Recognized
{
    using System;
    using System.Runtime.Serialization;


    [Serializable]
    public class ValueException :
        RecognizedException
    {
        public ValueException()
        {
        }

        public ValueException(string message)
            : base(message)
        {
        }

        public ValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}