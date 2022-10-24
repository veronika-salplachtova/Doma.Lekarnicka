namespace Doma.Lekarnicka.Logic;

public class MedicalSupply : HomeFirstAidKitItem
{
    public MedicalSupply(string name, int quantity) : base(name, quantity)
    { 
    }

    public override string ToString()
    {
        string quantityToString = Quantity.ToString();
        return $"{Name.PadRight(15, ' ')}{quantityToString.PadRight(15, ' ')}";
    }
}
