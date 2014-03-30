namespace Recognized
{
    using System;
    using System.Runtime.Serialization;


    [Serializable]
    public class NullValueException :
        ValueException
    {
        public NullValueException()
        {
        }

        public NullValueException(Type valueType)
            : base(GetMessage(valueType))
        {
        }

        public NullValueException(Type valueType, Exception innerException)
            : base(GetMessage(valueType), innerException)
        {
        }

        protected NullValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        static string GetMessage(Type valueType)
        {
            return string.Format("The value of {0} is null", valueType.Name);
        }
    }
}