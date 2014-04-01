namespace Recognized.Fragments
{
    using System.Collections;
    using System.Collections.Generic;


    public class DelimitedFragment :
        Fragment
    {
        readonly ITextSplitter _parser;
        readonly IFragmentFactory _fragmentFactory;

        public DelimitedFragment(ITextSplitter parser, IFragmentFactory fragmentFactory)
        {
            _parser = parser;
            _fragmentFactory = fragmentFactory;
        }

        public IEnumerator<Fragment> GetEnumerator()
        {
            for (int index = 0;; index++)
            {
                Fragment fragment;
                if (!TryGetFragment(index, out fragment))
                    yield break;

                yield return fragment;
            }
        }

        public bool TryGetFragment(int index, out Fragment fragment)
        {
            StringCursor text;
            if (_parser.TryGetText(index, out text))
            {
                return _fragmentFactory.TryCreateFragment(text, out fragment);
            }

            fragment = default(Fragment);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}