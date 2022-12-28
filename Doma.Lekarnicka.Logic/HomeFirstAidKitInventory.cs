using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Doma.Lekarnicka.Logic;

public class HomeFirstAidKitInventory
{
    private List<HomeFirstAidKitItem> homeFirstAidKitList;
    private InventoryDb db;


	public void StartRead()
    {
        db = new InventoryDb();
        homeFirstAidKitList = db.Read();
    }


    public IReadOnlyList<HomeFirstAidKitItem> HomeFirstAidKitList { get { return homeFirstAidKitList; } }

    public bool DoesExistItemWithName(string nameItem)
    {
        HomeFirstAidKitItem foundName = homeFirstAidKitList.Find(i => i.Name.Contains(nameItem, StringComparison.InvariantCultureIgnoreCase));
		if (foundName == null)
        {
            return false;
        }
        return true;
    }

    public void AddItem(HomeFirstAidKitItem itemToAdd)
    {
        homeFirstAidKitList.Add(itemToAdd);
        db.Add(itemToAdd);
    }

    public void Remove(string nameItemToRemove)
    {
        HomeFirstAidKitItem itemToRemove = homeFirstAidKitList.Find(i => i.Name.Contains(nameItemToRemove, StringComparison.InvariantCultureIgnoreCase));
		homeFirstAidKitList.Remove(itemToRemove);
        db.Remove(nameItemToRemove);
    }

}