namespace Recognized.Values
{
    using System;
    using System.Diagnostics;


    [DebuggerDisplay("{DebugText}")]
    public class PresentValue<TValue> :
        Value<TValue>
    {
        readonly StringCursor _text;
        readonly ITypeConverter<TValue> _typeConverter;

        Func<bool> _hasValue;
        Func<TValue> _value;

        public PresentValue(StringCursor text, ITypeConverter<TValue> typeConverter)
        {
            _text = text;
            _typeConverter = typeConverter;

            _hasValue = GetHasValue;
            _value = GetValue;
        }

        string DebugText
        {
            get
            {
                return string.Format("PresentValue<{0}>, HasValue: {1}, Value: {2}",
                    typeof(TValue).Name, HasValue,
                    HasValue
                        ? Value.ToString()
                        : "(n/a)");
            }
        }

        public bool IsPresent
        {
            get { return true; }
        }

        public bool TryGetValue<T>(ITypeConverter<T> typeConverter, out T value)
        {
            return typeConverter.TryGetValue(_text, out value);
        }

        public bool HasValue
        {
            get { return _hasValue(); }
        }

        public TValue Value
        {
            get { return _value(); }
        }


        bool GetHasValue()
        {
            TValue value;
            if (_typeConverter.TryGetValue(_text, out value))
            {
                _value = () => value;
                _hasValue = True;
                return true;
            }

            _value = ValueConversionFailed;
            _hasValue = False;
            return false;
        }

        TValue GetValue()
        {
            _hasValue();

            return _value();
        }

        TValue ValueConversionFailed()
        {
            string message = string.Format("Value conversion to {0} failed: {1}",
                typeof(TValue).Name, _text);

            _value = () =>
                {
                    throw new ValueException(message);
                };

            return _value();
        }

        static bool True()
        {
            return true;
        }

        static bool False()
        {
            return false;
        }
    }
}