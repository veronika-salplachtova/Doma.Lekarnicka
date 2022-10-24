namespace Doma.Lekarnicka.Logic;

public abstract class HomeFirstAidKitItem
{
    public string Name { get; }
    public int Quantity { get; }


    public HomeFirstAidKitItem(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}
