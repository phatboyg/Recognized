namespace Recognized
{
    using System;
    using System.Runtime.Serialization;


    [Serializable]
    public class EmptyValueException :
        ValueException
    {
        public EmptyValueException()
        {
        }

        public EmptyValueException(Type valueType)
            : base(GetMessage(valueType))
        {
        }

        public EmptyValueException(Type valueType, Exception innerException)
            : base(GetMessage(valueType), innerException)
        {
        }

        protected EmptyValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        static string GetMessage(Type valueType)
        {
            return string.Format("The value of {0} is empty", valueType.Name);
        }
    }
}