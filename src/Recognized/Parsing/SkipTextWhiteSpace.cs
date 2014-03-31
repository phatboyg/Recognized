namespace Recognized.Parsing
{
    using System.Globalization;


    public class SkipTextWhiteSpace :
        ITextWhiteSpace
    {
        public int SkipWhiteSpace(string text, int offset, int count)
        {
            int end = offset + count;

            while (offset < end && IsWhiteSpace(text[offset]))
                offset++;

            return offset;
        }

        public int TrimWhiteSpace(int index, string text, int offset, int count)
        {
            if (index > offset + count)
                return index;

            while (index > offset && char.IsWhiteSpace(text[index - 1]))
                index--;

            return index;
        }

        bool IsWhiteSpace(char c)
        {
            if (c <= '\x00ff')
            {
                return c == ' ' || c == '\x0009' || c == '\x000b' || c == '\x000c' || c == '\x00a0';
            }

            UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);

            return unicodeCategory == (UnicodeCategory.SpaceSeparator) || unicodeCategory == (UnicodeCategory.ParagraphSeparator);
        }
    }
}