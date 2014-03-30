namespace Recognized
{
    public interface IValue
    {
        /// <summary>
        ///     True if the value is present
        /// </summary>
        bool IsPresent { get; }


        bool TryGetValue<T>(ITypeConverter<T> typeConverter, out T value);
    }
}