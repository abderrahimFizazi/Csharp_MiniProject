using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepa
{
    internal class Eleve 
    {
        string nom;
        string prenom;
        string appoge;
        string niveau;
        string filiere;
        public Eleve(string nom, string prenom, string appoge, string niveau, string filiere)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.appoge = appoge;
            this.niveau = niveau;
            this.filiere = filiere;
        }
        public string Nom {
            get { return nom; }
            set { nom = value; }
            }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        public string Appoge
        {
            get { return appoge; }
            set { appoge = value; }
        }
        public string Niveau
        {
            get { return niveau; }
            set { niveau = value; }
        }
        public string Filiere
        {
            get { return filiere; }
            set { filiere = value; }
        }
        public override string ToString()
        {
            return "Nom: "+nom+"\t Prenom: "+ prenom+ "\t Appoge: " + appoge + "\t Filiere: " + filiere+ " \tNiveau: "+ niveau;
        }
    }
}
