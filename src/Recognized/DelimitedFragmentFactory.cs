namespace Recognized
{
    using Fragments;
    using Parsing;


    public class DelimitedFragmentFactory :
        IFragmentFactory
    {
        readonly ParsedFragmentFactory _fragmentFactory;
        readonly ITextParser _textParser;

        public DelimitedFragmentFactory(ParsedFragmentFactory fragmentFactory, ITextParser textParser)
        {
            _fragmentFactory = fragmentFactory;
            _textParser = textParser;
        }

        public bool TryCreateFragment(TextRef text, out Fragment fragment)
        {
            IFragmentParser fragmentParser = new TextFragmentParser(text, _textParser, _fragmentFactory);

            fragment = new DelimitedFragment(text, fragmentParser);
            return true;
        }
    }
}