namespace Recognized
{
    public interface ITextParser
    {
        /// <summary>
        ///     Returns the position of the first non-whitespace starting at the ParseOffset
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int SkipWhiteSpace(string text, int offset, int count);

        /// <summary>
        ///     Returns the position of the last non-whitespace character prior to the specified index
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        int TrimWhiteSpace(string text, int offset, int count, int index);

        /// <summary>
        ///     Find the next separator
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="separatorLength"></param>
        /// <returns></returns>
        int FindSeparator(string text, int offset, int count, out int separatorLength);
    }
}