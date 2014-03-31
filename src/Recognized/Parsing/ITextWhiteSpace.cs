namespace Recognized.Parsing
{
    public interface ITextWhiteSpace
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
        ///     Returns the position of the last non-whitespace character prior to the specified offset
        /// </summary>
        /// <param name="index"></param>
        /// <param name="text"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int TrimWhiteSpace(int index, string text, int offset, int count);
    }
}