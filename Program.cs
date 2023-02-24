using MySql.Data.MySqlClient;
namespace Prepa
{
    class Program
    {
        static void Main(String[] args)
        {
            GestionEleve C1 = new GestionEleve();
            Eleve e = new Eleve("abdo","Fizazi", "1000", "3","GINF");
            Eleve e2 = new Eleve("aya ", "Fizazi", "1001", "3", "GINF");
            Eleve e3 = new Eleve("safae", "Fizazi", "1002", "3", "GINF");
            Eleve e4 = new Eleve("simo", "Fizazi", "1004", "3", "GINF");
            Eleve e5 = new Eleve("yassine", "Fizazi", "1005", "3", "GINF");
            Eleve leveNexistePas = new Eleve("prof", "ghailani", "1007", "3", "GINF");
            Eleve updatedUserAbdo = new Eleve("abderrahim", "new Fizazi", "1000", "4", "GSTR");


            C1.Update(updatedUserAbdo);
        }
    }
}