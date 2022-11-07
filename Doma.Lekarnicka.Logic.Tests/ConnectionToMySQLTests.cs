using MySql.Data.MySqlClient;

namespace Doma.Lekarnicka.Logic.Tests;

public class ConnectionToMySQLTests
{
    [Fact]
    public void ConnectToDbIsSuccessfull()
    {
        ConnectionToMySQL connection = new();
        Assert.True(connection.Connect());
    }

    [Fact]
    public void ExecuteReaderWithSelect_ReturnsData()
    {
        ConnectionToMySQL connection = new();
        connection.Connect();
        string sql = "SELECT 1";
        MySqlDataReader reader = connection.ExecuteReader(sql);
        reader.Read();
        int result = reader.GetInt32(0);   
        Assert.Equal(1, result);
    }

    [Fact]
 
    public void ExecuteNonReader_ReturnsCountOfRemovedItem()
    {
        ConnectionToMySQL connection = new();
        connection.Connect();
        string sql = "DELETE FROM Drugs WHERE 1 = 0";
        int result = connection.ExecuteNonQuery(sql);
        Assert.Equal(0, result);
    }
}
