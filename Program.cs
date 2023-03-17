using MySql.Data.MySqlClient;
namespace Prepa
{
    class abdo { }
    class Program
    {
        static void Main(String[] args)
        {
            try { Connexion.Connect("Server=localhost;username='postgres';Password='1234' ; Database=mini ", "postgres");
                // Connexion.IUD("INSERT INTO Eleve(id,nom, prenom, appoge, niveau, filiere) VALUES (1,'abdo', 'fizazi', 1000, 2,'GSTR');");
                Eleve e1 = new Eleve("ahmed", "mouaad", 21111, "GSEA", 7);

                //List<dynamic> l = new List<dynamic>();
                //l = e1.All();
                //foreach(Eleve e in l){
                //    Console.WriteLine(e);   
                //}
                //Delete
                //Console.WriteLine(e1.delete());

                //Save
                // the save method will create an new record if the object passed with no id, if there is an id it will look for it and update it if it exists
                Console.WriteLine(e1.save());

                //Find T
                //Dictionary<string, object> resT = Model.find<Eleve>(1);
                //foreach (KeyValuePair<string, object> e in resT)
                //{
                //  Console.WriteLine($"{e.Key} : {e.Value}");

                //    }

                //Find
                //Dictionary<string, object> res = e1.find();
                //foreach (KeyValuePair<string, object> e in res)
                //{
                //    Console.WriteLine($"{e.Key} : {e.Value}");

                //}


            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}