namespace Recognized.Parsing
{
    using System;
    using System.Collections.Generic;


    public class SeparatorTextSplitter :
        ITextSplitter
    {
        readonly List<StringCursor> _elements;
        readonly int _parseEnd;
        readonly char[] _separator;
        readonly string _text;

        int _parseOffset;

        public SeparatorTextSplitter(StringCursor text, char[] separator = null)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _separator = separator ?? new[] {'\r', '\n'};

            _text = text.Data;

            _parseOffset = text.Offset;
            _parseEnd = text.Offset + text.Count;

            _elements = new List<StringCursor>();
        }

        public bool TryGetText(int index, out StringCursor text)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Index must be >= 0");

            if (index < _elements.Count)
            {
                text = _elements[index];
                return true;
            }

            while ((_parseEnd - _parseOffset) > 0 && index >= _elements.Count)
            {
                int lineEnd = _text.IndexOfAny(_separator, _parseOffset);
                if (lineEnd < 0)
                    lineEnd = _parseEnd;

                _elements.Add(new LocationStringCursor(_text, _parseOffset, lineEnd - _parseOffset, _elements.Count));

                if (lineEnd == _parseEnd)
                {
                    _parseOffset = lineEnd;
                    break;
                }

                for (int i = 0; i < _separator.Length && lineEnd < _parseEnd; i++, lineEnd++)
                {
                    if (_text[lineEnd] != _separator[i])
                        break;
                }

                _parseOffset = lineEnd;
                if (_parseEnd == _parseOffset)
                {
                    _elements.Add(new LocationStringCursor(_text, _parseOffset, 0, _elements.Count));
                }
            }

            if (index < _elements.Count)
            {
                text = _elements[index];
                return true;
            }

            text = default(StringCursor);
            return false;
        }
    }
    public class SeparatorStringSplitter :
        IStringSplitter
    {
        readonly List<string> _elements;
        readonly int _parseEnd;
        readonly char[] _separator;
        readonly string _text;

        int _parseOffset;

        public SeparatorStringSplitter(string text, char[] separator = null)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _separator = separator ?? new[] {'\r', '\n'};

            _text = text;

            _parseOffset = 0;
            _parseEnd = text.Length;

            _elements = new List<string>();
        }

        public bool TryGetText(int index, out string text)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "Index must be >= 0");

            if (index < _elements.Count)
            {
                text = _elements[index];
                return true;
            }

            while ((_parseEnd - _parseOffset) > 0 && index >= _elements.Count)
            {
                int lineEnd = _text.IndexOfAny(_separator, _parseOffset);
                if (lineEnd < 0)
                    lineEnd = _parseEnd;

                _elements.Add(_text.Substring(_parseOffset, lineEnd - _parseOffset));

                if (lineEnd == _parseEnd)
                {
                    _parseOffset = lineEnd;
                    break;
                }

                for (int i = 0; i < _separator.Length && lineEnd < _parseEnd; i++, lineEnd++)
                {
                    if (_text[lineEnd] != _separator[i])
                        break;
                }

                _parseOffset = lineEnd;
                if (_parseEnd == _parseOffset)
                {
                    _elements.Add("");
                }
            }

            if (index < _elements.Count)
            {
                text = _elements[index];
                return true;
            }

            text = default(string);
            return false;
        }
    }
}