namespace Recognized.Parsing
{
    public class RetainTextWhiteSpace :
        ITextWhiteSpace
    {
        public int SkipWhiteSpace(string text, int offset, int count)
        {
            return offset;
        }

        public int TrimWhiteSpace(int index, string text, int offset, int count)
        {
            return index;
        }
    }
}