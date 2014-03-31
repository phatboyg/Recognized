namespace Recognized.Parsing
{
    public class CommaDelimitedTextParser :
        ITextParser
    {
        static readonly TextParserSettings _defaultSettings = new TextParserSettings();
        readonly ITextSeparator _textSeparator;
        readonly ITextWhiteSpace _textWhiteSpace;

        public CommaDelimitedTextParser()
            : this(_defaultSettings)
        {
        }

        public CommaDelimitedTextParser(TextParserSettings settings)
        {
            _textWhiteSpace = settings.WhiteSpaceHandling == WhiteSpaceHandling.Remove
                ? (ITextWhiteSpace) new SkipTextWhiteSpace()
                : new RetainTextWhiteSpace();

            _textSeparator = new DelimiterTextSeparator(',');
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