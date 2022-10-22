namespace Doma.Lekarnicka.Logic;

public abstract class HomeFirstAidKitItem
{
    public string Name { get; }
    
    public HomeFirstAidKitItem(string name)
    {
        Name = name;
    }
}
