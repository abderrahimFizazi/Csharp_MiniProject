using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Prepa
{
    class Connexion
    {
        static IDbCommand cmd = null;
        static IDbConnection con = null;
        public static string Server = null;
        public static string ConString = null; // uid, password, @ip, database name

        public static bool IsConnected
        {
            get
            {
                return con != null && con.State == ConnectionState.Open; //Check connection state
            }
        }

        public static void Connect(string cstr, string server)
        {
            Server = server.Trim().ToLower();  // Trim + ToLower  => avoid inserting probs
            ConString = cstr;
            if (con != null && IsConnected) return; // Check Connection exixstence and openness

            switch (Server)
            {
                case "mysql":
                    con = new MySqlConnection(cstr);
                    cmd = new MySqlCommand("", (MySqlConnection)con);  // Cast con from IDbConnection to Mysql connection
                    con.Open();
                    break;
                // case: "Oracle" ... Here we add other databases...
                default:
                    throw new Exception("database server not supported..");
            }

        }

        public static int IUD(string req)   // IUD => Insert, Update, Delete Querey
        {
            cmd.CommandText = req;
            return cmd.ExecuteNonQuery();
        }
        public static IDataReader Select(string req) // Select Querey
        {
            cmd.CommandText = req;
            return cmd.ExecuteReader();
        }
        public static Dictionary<string, string> getChamps_Table(string table)
        {

            switch (Server)
            {
                case "mysql":
                    cmd.CommandText = $"SELECT COLUMN_NAME,COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{con.Database}' AND TABLE_NAME = '{table}'";
                    break;
                // case: "Oracle" ... Here we add other databases...
                default:
                    throw new Exception("database server not supported...");
            }

            IDataReader reader = cmd.ExecuteReader();
            Dictionary<string, string> res = new Dictionary<string, string>();

            while (reader.Read())
            {
                res.Add(reader.GetString(0), reader.GetString(1));
            }

            reader.Close();
            return res;
        }

    }
}

