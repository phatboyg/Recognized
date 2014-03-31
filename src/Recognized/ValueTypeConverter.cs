namespace Recognized
{
    using System;
    using TypeConverters;


    public static class ValueTypeConverter
    {
        static readonly Lazy<ITypeConverter<Guid>> _guid = new Lazy<ITypeConverter<Guid>>(() => new GuidTypeConverter());

        public static ITypeConverter<Guid> Guid
        {
            get { return _guid.Value; }
        }
    }
}