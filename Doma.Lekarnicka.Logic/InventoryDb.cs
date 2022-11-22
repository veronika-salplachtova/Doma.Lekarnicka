
using MySql.Data.MySqlClient;

namespace Doma.Lekarnicka.Logic;

public class InventoryDb
{
    ConnectionToMySQL connectionToMySQL;

    public InventoryDb()
    {
        this.connectionToMySQL = new ConnectionToMySQL();
        this.connectionToMySQL.Connect();
    }

    public List<HomeFirstAidKitItem> Read()
    {
        List<HomeFirstAidKitItem> list = new();
        MySqlDataReader reader = connectionToMySQL.ExecuteReader("SELECT ItemType, ItemName, PackageSize, Units, Quantity, Expiration FROM Inventory");

        try
        {
            while (reader.Read())
            {
                int itemType = reader.GetInt32(0);
                HomeFirstAidKitItem item = null;
                if (itemType == 1)
                {
                    item = new Drug(reader.GetString(1), reader.IsDBNull(2) ? null : reader.GetInt32(2), reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5)));
                }
                else if (itemType == 2)
                {
                    item = new MedicalSupply(reader.GetString(1), reader.GetInt32(4));
                }
                list.Add(item);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            
        }
        return list;
    }

    public void Add(HomeFirstAidKitItem itemToAdd)
    {

        if (itemToAdd is Drug drugToAdd)
        {
            connectionToMySQL.ExecuteNonQuery($"INSERT INTO Inventory (ItemType, ItemName, PackageSize, Units, Quantity, Expiration) " +
                $"VALUES (1,'{drugToAdd.Name}',{(drugToAdd.PackageSize ==null ? "null" : drugToAdd.PackageSize)},{(drugToAdd.Units == null ? "null" : "'" + drugToAdd.Units + "'")},{drugToAdd.Quantity},'{drugToAdd.Expiration.ToString("yyyy-MM-dd")}')");
        }
        else if (itemToAdd is MedicalSupply medicalSupplyToAdd)
        {
            connectionToMySQL.ExecuteNonQuery($"INSERT INTO Inventory (ItemType, ItemName, Quantity) " +
                $"VALUES (2, '{medicalSupplyToAdd.Name}', {medicalSupplyToAdd.Quantity})");

        }
    }

    public void Remove(string itemNameToRemove)
    {
        connectionToMySQL.ExecuteNonQuery($"DELETE FROM Inventory WHERE ItemName = '{itemNameToRemove}' LIMIT 1");
    }
}
