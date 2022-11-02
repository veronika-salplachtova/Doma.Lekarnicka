namespace Doma.Lekarnicka.Logic;

public class HomeFirstAidKitInventory
{
    private List<HomeFirstAidKitItem> homeFirstAidKitList;

    public HomeFirstAidKitInventory()
    {
        homeFirstAidKitList = new();

    }

    public IReadOnlyList<HomeFirstAidKitItem> HomeFirstAidKitList { get { return homeFirstAidKitList; } }

    public bool DoesExistItemWithName(string nameItem)
    {
        HomeFirstAidKitItem foundName = homeFirstAidKitList.Find(i => i.Name.Equals(nameItem, StringComparison.InvariantCultureIgnoreCase));
        if (foundName == null)
        {
            return false;
        }
        return true;
    }

    public void AddItem(HomeFirstAidKitItem itemToAdd)
    {
        homeFirstAidKitList.Add(itemToAdd);
        Console.WriteLine($"The item {itemToAdd.Name} has been added.");
    }

    public void Remove(string nameItemToRemove)
    {
        HomeFirstAidKitItem itemToRemove = homeFirstAidKitList.Find(i => i.Name.Equals(nameItemToRemove, StringComparison.InvariantCultureIgnoreCase));
        homeFirstAidKitList.Remove(itemToRemove);
    }
}
