
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
                if (itemType == 1)
                {
                    list.Add(new Drug(reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5))));
                }
                else if (itemType == 2)
                {
                    list.Add(new MedicalSupply(reader.GetString(1), reader.GetInt32(4)));
                }
            }
            reader.Close();
        }
        catch
        {

        }
        return list;
    }

    public void Add(HomeFirstAidKitItem itemToAdd)
    {
        if (itemToAdd is Drug drugToAdd)
        {
            connectionToMySQL.ExecuteNonQuery($"INSERT INTO Inventory (ItemType, ItemName, PackageSize, Units, Quantity, Expiration) " +
                $"VALUES (1,'{drugToAdd.Name}',{drugToAdd.PackageSize},'{drugToAdd.Units}',{drugToAdd.Quantity},{drugToAdd.Expiration.ToString("yyyy-MM-dd")})");
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
