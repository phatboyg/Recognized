namespace Recognized
{
    using System;


    /// <summary>
    ///     Refers to a block of text from an input string
    /// </summary>
    public class StringTextRef :
        TextRef
    {
        readonly int _count;
        readonly Func<string> _getString;
        readonly int _offset;
        readonly string _text;

        public StringTextRef(string text, int offset, int count)
        {
            _text = text;
            _offset = offset;
            _count = count;

            string s = null;
            bool computed = false;
            _getString = () =>
                {
                    if (computed)
                        return s;

                    s = text.Substring(offset, count);
                    computed = true;

                    return s;
                };
        }

        public StringTextRef(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _text = text;
            _offset = 0;
            _count = text.Length;

            _getString = () => text;
        }

        /// <summary>
        ///     The number of characters from the source text included in this reference
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        ///     The offset into the source text for this reference
        /// </summary>
        public int Offset
        {
            get { return _offset; }
        }

        /// <summary>
        ///     The source text referred to by the reference
        /// </summary>
        public string Text
        {
            get { return _text; }
        }

        public string GetString()
        {
            return _getString();
        }

        public static implicit operator string(StringTextRef text)
        {
            return text.GetString();
        }
    }
}