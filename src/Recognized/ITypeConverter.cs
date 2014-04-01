namespace Recognized
{
    /// <summary>
    ///     Provides a converter from a string to the value type
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public interface ITypeConverter<T>
    {
        /// <summary>
        ///     Try to convert the string to the value type
        /// </summary>
        /// <param name="text">The input text</param>
        /// <param name="value">The resulting value</param>
        /// <returns>True if the conversion could be performed, otherwise false</returns>
        bool TryGetValue(StringCursor text, out T value);
    }
}