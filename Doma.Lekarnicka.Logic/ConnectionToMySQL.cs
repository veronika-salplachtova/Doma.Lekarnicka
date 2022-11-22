using MySql.Data.MySqlClient;

namespace Doma.Lekarnicka.Logic;

public class ConnectionToMySQL
{
    MySqlConnection conn;
    string myConnectionString = "server=lekarnicka.cizzqrz9ailg.us-east-1.rds.amazonaws.com;uid=LekarnickaUser;pwd=1234;database=Lekarnicka";

    public void Connect()
    {
       conn = new();
       conn.ConnectionString = myConnectionString;
       conn.Open();
    }

    public MySqlDataReader ExecuteReader(string sql)
    {
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public int ExecuteNonQuery(string sql)
    {
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        return cmd.ExecuteNonQuery();
    }

    public int ExecuteScalar(string sql)
    {        
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandText = sql;
            Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            return count;
     }
}
