namespace Doma.Lekarnicka.Logic.Tests;

public class ConnectionToMySQLTests
{
    [Fact]
    public void ConnectToDbIsSuccessfull()
    {
        ConnectionToMySQL connection = new();
        Assert.True(connection.Connect());
    }
}
