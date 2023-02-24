using MySql.Data.MySqlClient;

namespace Prepa
{
    internal class DatabaseCnx
    {
        static MySqlConnection cnn = null;
        static string server = "localhost";
        static string database = "last";
        static string uid = "root";
        static string pwd = "";
        static public MySqlConnection Conexion()
        {
            string connetionString = "server="+server+";database="+database+";uid="+uid+";pwd="+pwd+";";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");
                return cnn;
            }catch(MySqlException ex)
            {
                Console.WriteLine("Problem with database Connection");
            }
            return null;
        }
        static public MySqlConnection getConexion()
        {
            if(cnn == null)
            {
                cnn = Conexion();
            }
            return cnn;
        }

    }
}
