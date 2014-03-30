namespace Recognized
{
    using System;


    /// <summary>
    ///     Refers to a block of text from an input string
    /// </summary>
    public class TextRef
    {
        /// <summary>
        ///     The number of characters from the source text included in this reference
        /// </summary>
        public readonly int Count;

        /// <summary>
        ///     Get the string referred to by the text reference
        /// </summary>
        public readonly Func<string> GetString;

        /// <summary>
        ///     The offset into the source text for this reference
        /// </summary>
        public readonly int Offset;

        /// <summary>
        ///     The source text referred to by the reference
        /// </summary>
        public readonly string Text;

        public TextRef(string text, int offset, int count)
        {
            Text = text;
            Offset = offset;
            Count = count;

            string s = null;
            bool computed = false;
            GetString = () =>
                {
                    if (computed)
                        return s;

                    s = text.Substring(offset, count);
                    computed = true;

                    return s;
                };
        }

        public TextRef(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            Text = text;
            Offset = 0;
            Count = text.Length;

            GetString = () => text;
        }

        public static implicit operator string(TextRef text)
        {
            return text.GetString();
        }
    }
}