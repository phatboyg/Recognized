namespace Recognized.Parsing
{
    public class TextParserSettings
    {
        public TextParserSettings()
        {
            WhiteSpaceHandling = WhiteSpaceHandling.Remove;
        }

        public WhiteSpaceHandling WhiteSpaceHandling { get; set; }
    }
}