namespace Recognized.Parsing
{
    public class NewLineTextParserSettings
    {
        public NewLineTextParserSettings()
        {
            WhiteSpaceHandling = WhiteSpaceHandling.Remove;
        }

        public WhiteSpaceHandling WhiteSpaceHandling { get; set; }
    }
}