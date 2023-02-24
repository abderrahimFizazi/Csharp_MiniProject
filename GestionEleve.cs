using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepa
{
    internal class GestionEleve : IDAO<Eleve>
    {
        static MySqlConnection MyCon = DatabaseCnx.getConexion();

        public void Create(Eleve elv)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = MyCon;
            string nom = elv.Nom;
            string prenom = elv.Prenom;
            string appoge = elv.Appoge;
            string filiere = elv.Filiere;
            string niveau = elv.Niveau;

            cmd.CommandText = "insert into Eleve values('"+ nom+"','"+prenom +"','"+ appoge+"','" +niveau+"','"+filiere+"')";
            cmd.ExecuteNonQuery();
            Console.WriteLine("User created succesfully!");
        }

        public int Delete(Eleve elv)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = MyCon;
            string appoge = elv.Appoge;

            cmd.CommandText = "delete from Eleve where appoge = " + appoge + ";";
            cmd.ExecuteNonQuery();
            Console.WriteLine("User Deleted succesfully!");
            return 0;
        }

        public Eleve Find(Eleve elv)
        {
            MySqlCommand cmd= new MySqlCommand();
            cmd.Connection = MyCon;
            string appoge = elv.Appoge;
            Eleve res = null;
            cmd.CommandText = "Select * from Eleve where appoge = "+ appoge + ";";
 
            MySqlDataReader MyReader = cmd.ExecuteReader();
            while (MyReader.Read())
            {
               res = new Eleve(MyReader.GetString(0), 
                MyReader.GetString(1),
                MyReader.GetString(2),
                MyReader.GetString(3),
                MyReader.GetString(4)
                );
            }
            return res;
        }

        public List<Eleve> Select()
        {
            string req = "Select * from Eleve";
            MySqlCommand cmd = new MySqlCommand(req,MyCon);
            List<Eleve> res = new List<Eleve>();
            MySqlDataReader MyReader = cmd.ExecuteReader();

            while (MyReader.Read())
            {
                Eleve elv = new Eleve(MyReader.GetString(0),
                MyReader.GetString(1),
                MyReader.GetString(2),
                MyReader.GetString(3),
                MyReader.GetString(4)
                );
                res.Add(elv);
            }
            return res;
        }

        public int Update(Eleve elv)
        {
            string req = "Update ELeve set nom = @nom, prenom= @prenom, niveau=@niveau, filiere= @filiere where appoge = @appoge";
            MySqlCommand cmd = new MySqlCommand(req, MyCon);
            cmd.Parameters.AddWithValue("@nom", elv.Nom );
            cmd.Parameters.AddWithValue("@prenom", elv.Prenom);
            cmd.Parameters.AddWithValue("@niveau", elv.Niveau);
            cmd.Parameters.AddWithValue("@filiere", elv.Filiere);
            cmd.Parameters.AddWithValue("@appoge", elv.Appoge);

            return cmd.ExecuteNonQuery();
        }
        public string getNbrEleve()
        {
            string res;
            string req = "select count(*) from Eleve;";
            MySqlCommand cmd = new MySqlCommand(req, MyCon);
            res = cmd.ExecuteScalar().ToString();
            if(res != null)
            return res;
            return null;
        }
    }
}
