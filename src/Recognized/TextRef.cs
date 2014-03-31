namespace Recognized
{
    /// <summary>
    ///     A TextRef is used to avoid string duplication and substring creation when it is not necessary. Many
    ///     string processing functions accept and offset and count, so duplicating a substring is rarely needed.
    ///     Using a reference reduces memory usage and exploits the immutability of string values
    /// </summary>
    public interface TextRef
    {
        /// <summary>
        ///     The number of characters from the source text included in this reference
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     The offset into the source text for this reference
        /// </summary>
        int Offset { get; }

        /// <summary>
        ///     The source text referred to by the reference
        /// </summary>
        string Text { get; }

        /// <summary>
        ///     Return the substring for this reference, which is cached in case it is requested multiple times
        /// </summary>
        /// <returns></returns>
        string GetString();
    }
}