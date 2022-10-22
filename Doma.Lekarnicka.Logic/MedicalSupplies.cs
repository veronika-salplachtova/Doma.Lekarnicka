namespace Doma.Lekarnicka.Logic;

public class MedicalSupplies : HomeFirstAidKitItem
{
    public MedicalSupplies(string name) : base(name)
    { 
    }

    public override string ToString()
    {
        return Name;
    }
}
