namespace Recognized.Tests
{
    using ValueTests;


    public class AddressMap :
        ModelMap<Address>
    {
        public AddressMap()
        {
            Map(x => x.Street);

            Map(x => x.HouseNumber);

            Map(x => x.Country);

            Map(x => x.PostalCode);
        }
    }
}