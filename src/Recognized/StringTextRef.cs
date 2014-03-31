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
        readonly int _offset;
        readonly string _text;
        Func<string> _getString;
        string _substring;

        public StringTextRef(string text, int offset, int count)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _text = text;
            _offset = offset;
            _count = count;

            _getString = ExtractSubstring;
        }

        public StringTextRef(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _text = text;
            _offset = 0;
            _count = text.Length;

            _getString = GetString;
        }

        /// <summary>
        ///     The number of characters from the source text included in this reference
        /// </summary>
        int TextRef.Count
        {
            get { return _count; }
        }

        /// <summary>
        ///     The offset into the source text for this reference
        /// </summary>
        int TextRef.Offset
        {
            get { return _offset; }
        }

        /// <summary>
        ///     The source text referred to by the reference
        /// </summary>
        string TextRef.Text
        {
            get { return _text; }
        }

        string TextRef.GetString()
        {
            return _getString();
        }

        string GetString()
        {
            return _text;
        }

        string ExtractSubstring()
        {
            _substring = _text.Substring(_offset, _count);

            _getString = GetSubstring;

            return _substring;
        }

        string GetSubstring()
        {
            return _substring;
        }
    }
}