namespace Recognized.TypeConverters
{
    using System;


    public class GuidTypeConverter :
        ITypeConverter<Guid>
    {
        bool ITypeConverter<Guid>.TryGetValue(TextRef text, out Guid value)
        {
            return Guid.TryParse(text.GetString(), out value);
        }
    }
}