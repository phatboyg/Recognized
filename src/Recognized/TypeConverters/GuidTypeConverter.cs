namespace Recognized.TypeConverters
{
    using System;


    public class GuidTypeConverter :
        ITypeConverter<Guid>
    {
        bool ITypeConverter<Guid>.TryGetValue(StringCursor text, out Guid value)
        {
            return Guid.TryParse(text.String, out value);
        }
    }
}