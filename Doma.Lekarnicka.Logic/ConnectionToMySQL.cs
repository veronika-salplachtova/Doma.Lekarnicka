using MySql.Data.MySqlClient;

namespace Doma.Lekarnicka.Logic
{
    public class ConnectionToMySQL
    {
        MySqlConnection conn;

        public bool Connect()
        {
            string myConnectionString = "server=lekarnicka.cizzqrz9ailg.us-east-1.rds.amazonaws.com;uid=LekarnickaUser;pwd=1234;database=Lekarnicka";

            try
            {
                conn = new ();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return false;
        }   
    }
}
