namespace Recognized.Parsing
{
    public class DelimiterTextSeparator :
        ITextSeparator
    {
        readonly char _separator;

        public DelimiterTextSeparator(char separator)
        {
            _separator = separator;
        }

        public int FindSeparator(string text, int offset, int count, out int separatorLength)
        {
            separatorLength = 1;
            return text.IndexOf(_separator, offset, count);
        }
    }
}