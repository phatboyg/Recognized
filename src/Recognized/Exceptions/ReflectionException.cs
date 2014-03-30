namespace Recognized
{
    using System;
    using System.Runtime.Serialization;


    [Serializable]
    public class ReflectionException :
        RecognizedException
    {
        public ReflectionException()
        {
        }

        public ReflectionException(string message)
            : base(message)
        {
        }

        public ReflectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ReflectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}