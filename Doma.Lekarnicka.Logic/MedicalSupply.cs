namespace Doma.Lekarnicka.Logic;

public class MedicalSupply : HomeFirstAidKitItem
{
    public MedicalSupply(string name) : base(name)
    { 
    }

    public override string ToString()
    {
        return Name;
    }
}
