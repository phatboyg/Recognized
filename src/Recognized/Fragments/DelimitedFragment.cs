namespace Recognized.Fragments
{
    using System.Collections;
    using System.Collections.Generic;


    public class DelimitedFragment :
        Fragment
    {
        readonly IFragmentParser _parser;
        readonly TextRef _text;

        public DelimitedFragment(TextRef text, IFragmentParser parser)
        {
            _text = text;
            _parser = parser;
        }

        public int Count
        {
            get { return _text.Count; }
        }

        public int Offset
        {
            get { return _text.Offset; }
        }

        public string Text
        {
            get { return _text.Text; }
        }

        public string GetString()
        {
            return _text.GetString();
        }

        public IEnumerator<Fragment> GetEnumerator()
        {
            for (int index = 0;; index++)
            {
                Fragment fragment;
                if (!_parser.TryGetFragment(index, out fragment))
                    yield break;

                yield return fragment;
            }
        }

        public bool TryGetFragment(int index, out Fragment fragment)
        {
            return _parser.TryGetFragment(index, out fragment);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}