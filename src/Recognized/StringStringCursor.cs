namespace Recognized
{
    using System;


    public class StringStringCursor :
        StringCursor
    {
        readonly int _count;
        readonly string _data;

        public StringStringCursor(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            _data = data;
            _count = data.Length;
        }

        public int Count
        {
            get { return _count; }
        }

        public int Offset
        {
            get { return 0; }
        }

        public string Data
        {
            get { return _data; }
        }

        string StringCursor.GetString()
        {
            return _data;
        }
    }
}