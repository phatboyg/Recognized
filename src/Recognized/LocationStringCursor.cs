namespace Recognized
{
    using System;


    /// <summary>
    ///     A portion of a string, taken from a location within the source
    /// </summary>
    public class LocationStringCursor :
        StringCursor
    {
        readonly int _count;
        readonly string _data;
        readonly int _index;
        readonly int _offset;
        Func<string> _getString;
        string _substring;

        public LocationStringCursor(string data, int offset, int count, int index)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            _data = data;
            _offset = offset;
            _count = count;
            _index = index;

            _getString = ExtractSubstring;
        }

        public int Index
        {
            get { return _index; }
        }

        public int Count
        {
            get { return _count; }
        }

        public int Offset
        {
            get { return _offset; }
        }

        public string Data
        {
            get { return _data; }
        }

        public string GetString()
        {
            return _getString();
        }

        string ExtractSubstring()
        {
            _substring = _data.Substring(_offset, _count);

            _getString = GetSubstring;

            return _substring;
        }

        string GetSubstring()
        {
            return _substring;
        }
    }
}