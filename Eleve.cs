using System;
namespace Prepa
{
	public class Eleve : Model
	{
		public string nom;
        public string prenom;
        public int appoge;
        public string filiere;
        public int niveau;
        

		

        public Eleve() { }

  
        public override string ToString()
        {
			return " Nom: " + nom + "\t Prenom: " + prenom + "\t Appoge: " + appoge + "\t Filiere: " + filiere + "\t Niveau: " + niveau;
		}


	}
}

