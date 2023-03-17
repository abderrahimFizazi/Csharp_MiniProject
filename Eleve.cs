using System;
namespace Prepa
{
	public class Eleve : Model
	{
		string nom;
		string prenom;
		int appoge;
		string filiere;
		int niveau;

		public string Nom { get { return nom ;  } set { nom = value; } }
        public string Prenom { get { return prenom; } set { prenom = value; } }
        public int Appoge { get { return appoge; } set { appoge = value; } }
        public string Filiere { get { return filiere; } set { filiere = value; } }
        public int Niveau { get { return niveau; } set { niveau = value; } }


        public Eleve() { }

        public Eleve(int i, string n, string p, int ap,string fil, int niv ) {
			id = i;
			nom = n;
			prenom = p;
			appoge = ap;
			filiere = fil;
			niveau = niv;
		}
        public Eleve(string n, string p, int ap, string fil, int niv)
        {
			id = 0;
            nom = n;
            prenom = p;
            appoge = ap;
            filiere = fil;
            niveau = niv;
        }
        public override string ToString()
        {
			return "Id : " + id + "  -- Nom: " + nom + " -- Prenom: " + prenom + " -- Appoge: " + appoge + " -- Filiere: " + filiere + " -- Niveau: " + niveau;
		}


	}
}

