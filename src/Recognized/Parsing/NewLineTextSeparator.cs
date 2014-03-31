namespace Recognized.Parsing
{
    public class NewLineTextSeparator :
        ITextSeparator
    {
        char _separator;

        public NewLineTextSeparator()
        {
            _separator = '\r';
        }

        public int FindSeparator(string text, int offset, int count, out int separatorLength)
        {
            int remaining = count;

            int index;
            if ((index = text.IndexOf(_separator, offset, remaining)) <= 0)
            {
                index = text.IndexOf('\n', offset, remaining);
                if (index > 0)
                    _separator = '\n';
            }

            if (index > 0)
            {
                separatorLength = 1;

                // skip over the \n if \r was found first (windows, CR+LF)
                if (index < offset + count && _separator == '\r' && text[index + 1] == '\n')
                    separatorLength = separatorLength + 1;

                return index;
            }

            separatorLength = 0;
            return -1;
        }
    }
}