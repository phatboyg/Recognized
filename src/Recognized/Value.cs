namespace Recognized
{
    /// <summary>
    ///     A value in a model interface
    /// </summary>
    public interface Value<out T> :
        IValue
    {
        /// <summary>
        ///     True if the value is present and can be converted to an instance of T
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        ///     The value of T
        /// </summary>
        T Value { get; }
    }


    public static class Value
    {
        /// <summary>
        ///     An empty value is neither present nor has a value
        /// </summary>
        /// <typeparam name="T">The Value type</typeparam>
        /// <returns>An empty value of the value type</returns>
        public static Value<T> Empty<T>()
        {
            return ValueCache<T>.EmptyValue;
        }

        /// <summary>
        ///     A null has is present but has no value. Null values are typically meant to redact information
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <returns></returns>
        public static Value<T> Null<T>()
        {
            return ValueCache<T>.NullValue;
        }

        public static Value<T> Constant<T>(ITextConverter<T> textConverter, T value)
            where T : class
        {
            return new ConstantValue<T>(textConverter, value != default(T), value);
        }

        public static Value<T> Constant<T>(ITextConverter<T> textConverter, T? value)
            where T : struct
        {
            return new ConstantValue<T>(textConverter, value.HasValue, value.HasValue ? value.Value : default(T));
        }


        static class ValueCache<TValue>
        {
            public static readonly Value<TValue> EmptyValue = new Empty();
            public static readonly Value<TValue> NullValue = new Null();


            class Empty :
                Value<TValue>
            {
                bool IValue.IsPresent
                {
                    get { return false; }
                }

                bool IValue.TryGetValue<T>(ITypeConverter<T> typeConverter, out T value)
                {
                    value = default(T);
                    return false;
                }

                bool Value<TValue>.HasValue
                {
                    get { return false; }
                }

                TValue Value<TValue>.Value
                {
                    get { throw new EmptyValueException(typeof(TValue)); }
                }
            }


            class Null :
                Value<TValue>
            {
                bool IValue.IsPresent
                {
                    get { return true; }
                }

                bool IValue.TryGetValue<T>(ITypeConverter<T> typeConverter, out T value)
                {
                    value = default(T);
                    return false;
                }

                bool Value<TValue>.HasValue
                {
                    get { return false; }
                }

                TValue Value<TValue>.Value
                {
                    get { throw new NullValueException(typeof(TValue)); }
                }
            }
        }
    }
}