namespace Recognized
{
    /// <summary>
    /// Converts the value to a string representation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITextConverter<in T>
    {
        bool TryConvert(T value, out string text);
    }
}