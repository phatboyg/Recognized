namespace Recognized.Tests.ValueTests
{
    using Recognized;


    public interface Address
    {
        Value<string> Street { get; }

        string HouseNumber { get; }

        int? PostalCode { get; }

        Country Country { get; }
    }


    public interface Country
    {
        
    }
}