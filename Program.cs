using MySql.Data.MySqlClient;
namespace Prepa
{
    class Program
    {
        static void Main(String[] args)
        {
            try { Connexion.Connect("Server=localhost;username='postgres';Password='1234' ; Database=mini ", "postgres");
                // Connexion.IUD("INSERT INTO Eleve(id,nom, prenom, appoge, niveau, filiere) VALUES (1,'abdo', 'fizazi', 1000, 2,'GSTR');");
                Eleve e1 = new Eleve();

                e1.id = 1;
                e1.niveau = 2;
                e1.nom = "Abderrahim";
                Console.Write(e1.save("nom", "niveau"));

                //List static
                //List<dynamic> l = new List<dynamic>();
                //l = Eleve.all<Eleve>();
                //foreach (Eleve e in l)
                //{
                //    Console.WriteLine(e);
                //}
                // List 
                //l = e1.All();
                //foreach(Eleve e in l){
                //    Console.WriteLine(e);   
                //}
                //Delete
                //Console.WriteLine(e1.delete());

                //Save
                // the save method will create an new record if the object passed with no id, if there is an id it will look for it and update it if it exists
                //Console.WriteLine(e1.save());

                //Find static
                //Dictionary<string, object> resT = Model.find<Eleve>(1);
                //foreach (KeyValuePair<string, object> e in resT)
                //{
                //    Console.WriteLine($"{e.Key} : {e.Value}");

                //}

                // Find
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