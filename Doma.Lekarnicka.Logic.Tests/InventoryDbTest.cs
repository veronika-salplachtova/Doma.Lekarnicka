
namespace Doma.Lekarnicka.Logic.Tests;

public class InventoryDbTest
{
   [Fact]
    public void AddItemToDB()
    {
        InventoryDb db = new InventoryDb();
        Drug test = new("test", 1, "tbl", 1,new (2024,1,1));
        ConnectionToMySQL connectionToMySQL = new();
        connectionToMySQL.Connect();
        int count1 = connectionToMySQL.ExecuteScalar($"SELECT COUNT(*) FROM Inventory WHERE ItemName = '{test.Name}'");

        db.Add(test);
        int count2 = connectionToMySQL.ExecuteScalar($"SELECT COUNT(*) FROM Inventory WHERE ItemName = '{test.Name}'");
        Assert.Equal(count1 + 1,count2);
        db.Remove(test.Name);
    }

    [Fact]
    public void RemoveItemFromDb()
    {
        InventoryDb db = new();
        Drug testRemove = new("testRemove", 1, "tbl", 1, new(2024, 1, 1));
        db.Add(testRemove);

        ConnectionToMySQL connectionToMySQL = new();
        connectionToMySQL.Connect();
        int count1 = connectionToMySQL.ExecuteScalar($"SELECT COUNT(*) FROM Inventory WHERE ItemName = '{testRemove.Name}'");

        db.Remove(testRemove.Name);
        int count2 = connectionToMySQL.ExecuteScalar($"SELECT COUNT(*) FROM Inventory WHERE ItemName = '{testRemove.Name}'");

        Assert.Equal(count1 - 1, count2);
    }
}
