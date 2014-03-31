namespace Recognized.Values
{
    using System;
    using System.Diagnostics;
    using Parsing;


    [DebuggerDisplay("{DebugText}")]
    public class ConstantValue<TValue> :
        Value<TValue>
    {
        readonly bool _hasValue;
        readonly ITextConverter<TValue> _textConverter;
        readonly TValue _value;

        Func<bool> _isPresent;
        Func<TextRef> _text;

        public ConstantValue(ITextConverter<TValue> textConverter, bool hasValue, TValue value)
        {
            _value = value;
            _hasValue = hasValue;
            _textConverter = textConverter;

            _isPresent = GetIsPresent;
        }

        string DebugText
        {
            get
            {
                return string.Format("ConstantValue<{0}>, {1}, {2}",
                    typeof(TValue).Name,
                    IsPresent ? "Present" : "Not Present",
                    HasValue ? "Value: " + Value : "No Value");
            }
        }

        public bool IsPresent
        {
            get { return _isPresent(); }
        }

        public bool TryGetValue<T>(ITypeConverter<T> typeConverter, out T value)
        {
            if (!_isPresent())
            {
                value = default(T);
                return false;
            }

            return typeConverter.TryGetValue(_text(), out value);
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public TValue Value
        {
            get { return _value; }
        }


        bool GetIsPresent()
        {
            string text;
            if (_textConverter.TryConvert(_value, out text))
            {
                var textRef = new StringTextRef(text);
                _text = () => textRef;
                _isPresent = True;
                return true;
            }

            _text = ValueConversionFailed;
            _isPresent = False;
            return false;
        }

        TextRef ValueConversionFailed()
        {
            string message = string.Format("String conversion from {0} failed: {1}", typeof(TValue).Name, _value);

            _text = () =>
                {
                    throw new ValueException(message);
                };

            return _text();
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