namespace Recognized.TextConverters
{
    using System;
    using System.Globalization;


    public class GuidTextConverter :
        ITextConverter<Guid>
    {
        readonly string _format;

        public GuidTextConverter(string format)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            Exception exception;
            if (!IsValidFormat(format, out exception))
                throw new ArgumentException("Invalid format specified: " + format, exception);

            _format = format;
        }

        public bool TryConvert(Guid value, out string text)
        {
            text = value.ToString(_format, CultureInfo.InvariantCulture);
            return true;
        }

        bool IsValidFormat(string format, out Exception exception)
        {
            try
            {
                var s = Guid.Empty.ToString(format, CultureInfo.InvariantCulture);

                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }
    }
}