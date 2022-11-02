namespace Doma.Lekarnicka.Logic
{
        public class Drug : HomeFirstAidKitItem
    {
        public Drug(string name, int? packageSize, string units, int quantity, DateOnly expiration) : base(name, quantity)
        {
            PackageSize = packageSize;
            Units = units;
            this.Expiration = expiration;
           
        }

        public DateOnly Expiration { get; }
        public int? PackageSize { get; }
        public string Units { get; }

        public override string ToString()
        {
            string quantityToString = Quantity.ToString();
            string expirationToString = Expiration.ToString();
            string units = "";
            if (Units != null)
                units = Units;
            return $"{Name.PadRight(15,' ')}{PackageSize} {units.PadRight(15,' ')}{quantityToString.PadRight(15, ' ')}{expirationToString.PadRight(15, ' ')}";
        }
    }
}