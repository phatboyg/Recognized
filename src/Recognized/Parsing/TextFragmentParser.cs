namespace Recognized.Parsing
{
    using System;
    using System.Collections.Generic;


    public class TextFragmentParser :
        IFragmentParser
    {
        readonly IFragmentFactory _fragmentFactory;
        readonly List<Fragment> _fragments;
        readonly int _parseEnd;
        readonly TextRef _text;
        readonly ITextParser _textParser;
        int _parseOffset;

        public TextFragmentParser(TextRef text, ITextParser textParser, IFragmentFactory fragmentFactory)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (textParser == null)
                throw new ArgumentNullException("textParser");
            if (fragmentFactory == null)
                throw new ArgumentNullException("fragmentFactory");

            _text = text;
            _textParser = textParser;
            _fragmentFactory = fragmentFactory;

            _parseOffset = text.Offset;
            _parseEnd = text.Offset + text.Count;

            _fragments = new List<Fragment>();
        }

        public bool TryGetFragment(int index, out Fragment fragment)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Index must be >= 0");

            if (index < _fragments.Count)
            {
                fragment = _fragments[index];
                return true;
            }

            _parseOffset = _textParser.SkipWhiteSpace(_text.Text, _parseOffset, _parseEnd - _parseOffset);

            int remaining;
            while ((remaining = _parseEnd - _parseOffset) > 0 && index >= _fragments.Count)
            {
                int separatorLength;
                int separatorIndex = _textParser.FindSeparator(_text.Text, _parseOffset, remaining, out separatorLength);
                if (separatorIndex <= 0)
                    break;

                int endIndex = _textParser.TrimWhiteSpace(_text.Text, _parseOffset, remaining, separatorIndex);

                AddFragment(endIndex - _parseOffset);

                _parseOffset = separatorIndex + separatorLength;

                _parseOffset = _textParser.SkipWhiteSpace(_text.Text, _parseOffset, _parseEnd - _parseOffset);

                if (_parseEnd == _parseOffset)
                {
                    AddFragment(0);
                }
            }

            if (index >= _fragments.Count && remaining > 0)
            {
                int endIndex = _parseOffset + remaining;
                endIndex = _textParser.TrimWhiteSpace(_text.Text, _parseOffset, remaining, endIndex);

                AddFragment(endIndex - _parseOffset);

                _parseOffset += remaining;

                _parseOffset = _textParser.SkipWhiteSpace(_text.Text, _parseOffset, _parseEnd - _parseOffset);
            }

            if (index < _fragments.Count)
            {
                fragment = _fragments[index];
                return true;
            }

            fragment = default(Fragment);
            return false;
        }

        void AddFragment(int count)
        {
            TextRef text = _parseOffset == _text.Offset && count == _text.Count
                ? _text
                : new StringTextRef(_text.Text, _parseOffset, count);

            Fragment fragment;
            if (_fragmentFactory.TryCreateFragment(text, out fragment))
                _fragments.Add(fragment);
        }
    }
}