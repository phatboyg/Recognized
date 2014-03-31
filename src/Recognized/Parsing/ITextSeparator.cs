namespace Recognized.Parsing
{
    public interface ITextSeparator
    {
        /// <summary>
        /// Finds the separator in the specified TextRef
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="separatorLength">The length of the separator, so it can be skipped</param>
        /// <returns>The position of the separator, if found, or -1 if not</returns>
        int FindSeparator(string text, int offset, int count, out int separatorLength);
    }
}