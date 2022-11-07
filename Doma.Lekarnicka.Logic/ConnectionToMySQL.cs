using MySql.Data.MySqlClient;

namespace Doma.Lekarnicka.Logic;

public class ConnectionToMySQL
{
    MySqlConnection conn;
    string myConnectionString = "server=lekarnicka.cizzqrz9ailg.us-east-1.rds.amazonaws.com;uid=LekarnickaUser;pwd=1234;database=Lekarnicka";

    public bool Connect()
    {
        try
        {
            conn = new();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public MySqlDataReader ExecuteReader(string sql)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        catch
        {
            return null;
        }
    }

    public int ExecuteNonQuery(string sql)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            return cmd.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
    }
}
