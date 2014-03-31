namespace Recognized.Parsing
{
    public class SkipTextWhiteSpace :
        ITextWhiteSpace
    {
        public int SkipWhiteSpace(string text, int offset, int count)
        {
            int end = offset + count;

            while (offset < end && char.IsWhiteSpace(text, offset))
                offset++;

            return offset;
        }

        public int TrimWhiteSpace(int index, string text, int offset, int count)
        {
            if (index > offset + count)
                return index;

            while (index > offset && char.IsWhiteSpace(text, index - 1))
                index--;

            return index;
        }
    }
}