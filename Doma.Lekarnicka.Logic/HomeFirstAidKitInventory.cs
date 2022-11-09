using MySql.Data.MySqlClient;

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
    }

    public void Remove(string nameItemToRemove)
    {
        HomeFirstAidKitItem itemToRemove = homeFirstAidKitList.Find(i => i.Name.Equals(nameItemToRemove, StringComparison.InvariantCultureIgnoreCase));
        homeFirstAidKitList.Remove(itemToRemove);
    }

    ConnectionToMySQL connectionToMySQL = new ConnectionToMySQL();

    public void ReadFromDb()
    {
        connectionToMySQL.Connect();
        MySqlDataReader reader = connectionToMySQL.ReaderExecute("SELECT ItemType, ItemName, PackageSize, Units, Quantity, Expiration FROM Inventory");

        try
        {
            while (reader.Read())
            {
                if (reader.GetInt32(0) == 1)
                {
                    homeFirstAidKitList.Add(new Drug(reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5))));
                }
                else if (reader.GetInt32(0) == 2)
                {
                    homeFirstAidKitList.Add(new MedicalSupply(reader.GetString(1), reader.GetInt32(4)));                    
                }

            }
            reader.Close();
        }
        catch
        {

        }
    }
    public string ValuesToSql;
    public void AddDrugToDb()
    {
        connectionToMySQL.Connect();
        connectionToMySQL.ExecuteNonQuery($"INSERT INTO Inventory (ItemType, ItemName, PackageSize, Units, Quantity, Expiration) VALUES ({ValuesToSql})");
    }

    public void AddMedicalSupplyToDb()
    {
        connectionToMySQL.Connect();
        connectionToMySQL.ExecuteNonQuery($"INSERT INTO Inventory (ItemType, ItemName, Quantity) VALUES ({ValuesToSql})");
    }

    public void RemoveItemFromDb(string nameItemToRemove)
    {
        HomeFirstAidKitItem itemToRemove = homeFirstAidKitList.Find(i => i.Name.Equals(nameItemToRemove, StringComparison.InvariantCultureIgnoreCase));
        connectionToMySQL.Connect();
        connectionToMySQL.ExecuteNonQuery($"DELETE FROM Inventory WHERE ItemName = {nameItemToRemove}");
    }
}
/*
 * 
*/