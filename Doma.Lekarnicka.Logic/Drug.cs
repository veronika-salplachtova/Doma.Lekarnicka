namespace Doma.Lekarnicka.Logic
{
        public class Drug : HomeFirstAidKitItem
    {
        public Drug(string name, DateOnly expiration) : base(name)
        {
            this.Expiration = expiration;  
        }

        public DateOnly Expiration { get; }

        public override string ToString()
        {
            return $"{Name.PadRight(15,' ')}{Expiration}";
        }
    }
}