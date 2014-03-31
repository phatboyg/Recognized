namespace Recognized.Fragments
{
    using System.Collections;
    using System.Collections.Generic;


    /// <summary>
    ///     A parsed fragment is a simple fragment that cannot be parsed any further internally
    /// </summary>
    public class ParsedFragment :
        Fragment
    {
        readonly TextRef _text;

        public ParsedFragment(TextRef text)
        {
            _text = text;
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
            yield break;
        }

        public bool TryGetFragment(int index, out Fragment fragment)
        {
            fragment = default(Fragment);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}