namespace Recognized
{
    using Fragments;
    using Parsing;


    public class DelimitedFragmentFactory :
        IFragmentFactory
    {
        readonly ParsedFragmentFactory _fragmentFactory;

        public DelimitedFragmentFactory(ParsedFragmentFactory fragmentFactory)
        {
            _fragmentFactory = fragmentFactory;
        }

        public bool TryCreateFragment(StringCursor text, out Fragment fragment)
        {
            ITextSplitter textSplitter = new SeparatorTextSplitter(text, new[] {'\r', '\n'});

            fragment = new DelimitedFragment(textSplitter, _fragmentFactory);
            return true;
        }
    }
}