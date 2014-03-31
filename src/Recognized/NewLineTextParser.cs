namespace Recognized
{
    using Parsing;


    public class NewLineTextParser :
        ITextParser
    {
        readonly ITextSeparator _textSeparator;
        readonly ITextWhiteSpace _textWhiteSpace;

        public NewLineTextParser(bool removeWhiteSpace = true)
        {
            _textWhiteSpace = removeWhiteSpace
                ? (ITextWhiteSpace) new SkipTextWhiteSpace()
                : new RetainTextWhiteSpace();

            _textSeparator = new NewLineTextSeparator();
        }

        public int FindSeparator(string text, int offset, int count, out int separatorLength)
        {
            return _textSeparator.FindSeparator(text, offset, count, out separatorLength);
        }

        public int TrimWhiteSpace(string text, int offset, int count, int index)
        {
            return _textWhiteSpace.TrimWhiteSpace(index, text, offset, count);
        }

        public int SkipWhiteSpace(string text, int offset, int count)
        {
            return _textWhiteSpace.SkipWhiteSpace(text, offset, count);
        }
    }
}