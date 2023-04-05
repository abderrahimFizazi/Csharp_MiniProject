using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace MinProjetLibrary
{
    public class BackUp
    {
        public static MySqlConnection conn = new MySqlConnection("Server=127.0.0.1 ;Database=miniprojet;Uid=root;Pwd=;");
        public static MySqlCommand cmd = new MySqlCommand();
        public static string chemin = @"D:\ensat.xml";
        public static void Connect()
        {
            conn.Open();
            cmd.Connection = con;
        }
        // Garder la trace des eleves supprimées
        public static void ToXML(string cod)
        {
            XDocument doc;
            Connect();
            try
            {
                doc = XDocument.Load(chemin);
            }
            catch
            {
                doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("eleves"));
            }
            cmd.CommandText = $"select * from eleve where code='" + cod + "'";
            cmd.Connection = conn;
            MySqlDataReader reader1 = cmd.ExecuteReader();
            XElement enregistrement = new XElement("eleve");
            XElement notes = new XElement("notes");
            XElement moyenne = new XElement("moyenne");
            // Recuperer les données de la table eleve
            while (reader1.Read())
            {
                for (int i = 0; i < reader1.FieldCount; i++)
                {
                    XElement champ = new XElement(reader1.GetName(i), reader1.GetValue(i));
                    enregistrement.Add(champ);

                }

            }
            reader1.Close();
            cmd.CommandText = $"select * from notes where code_eleve='" + cod + "'";
            MySqlDataReader reader2 = cmd.ExecuteReader();
            // Recuperer les notes de l'eleve supprimé
            while (reader2.Read())
            {
                XElement note = new XElement("note");
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    if (reader2.GetName(i) == "code_mat")
                    {
                        XElement colonn = new XElement("code_matiere", reader2.GetValue(i));
                        note.Add(colonn);
                    }
                    else if (reader2.GetName(i) == "note")
                    {
                        XElement colonn = new XElement("note_matiere", reader2.GetValue(i));
                        note.Add(colonn);
                    }

                }
                notes.Add(note);
                note = null;
            }
            reader2.Close();
            cmd.CommandText = $"select * from moyennes where code_eleve='" + cod + "'";
            MySqlDataReader reader3 = cmd.ExecuteReader();
            // Recuperer la moyenne de l'eleve supprimé
            while (reader3.Read())
            {
                for (int i = 0; i < reader3.FieldCount; i++)
                {
                    if (reader3.GetName(i) == "moyenne")
                    {
                        if (reader3.GetValue(i) == null)
                        {
                            XElement champ = new XElement(reader3.GetName(i), "0");
                            moyenne.Add(champ);
                        }
                        else
                        {
                            XElement champ = new XElement(reader3.GetName(i), reader3.GetValue(i));
                            moyenne.Add(champ);
                        }
                    }
                }

            }
            // Enregistrer la date de suppression
            XElement date = new XElement("date_suppression", DateTime.Now.ToString());
            enregistrement.Add(notes);
            enregistrement.Add(moyenne);
            enregistrement.Add(date);
            doc.Root.Add(enregistrement);
            reader3.Close();
            doc.Save(chemin);

        }

        public static void Restaurer(DateTime date)
        {
            XDocument doc = XDocument.Load(@"D:\ensat.xml");
            Connect();
            foreach (var eleve in doc.Descendants("eleve"))
            {
                DateTime datesupr = DateTime.Parse(eleve.Element("date_suppression").Value);
                if (datesupr >= new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second))
                {
                    string code_ele = eleve.Element("code").Value;
                    string nom = eleve.Element("nom").Value;
                    string prenom = eleve.Element("prenom").Value;
                    string niveau = eleve.Element("niveau").Value;
                    string code_fil = eleve.Element("code_fil").Value;
                    // Restaurer l'eleve
                    cmd.CommandText = "insert into eleve(code,nom,prenom,niveau,code_fil) values" +
                        "(" + code_ele + "," + nom + "," + prenom + "," + niveau + "," + code_fil + ")";
                    cmd.ExecuteNonQuery();

                    var notes = from note in eleve.Descendants("note")
                                select note;
                    // Restaurer les notes de l'eleve
                    foreach (var note in notes)
                    {
                        string code_mat = note.Element("code_matiere").Value;
                        string note_mat = note.Element("note_matiere").Value;
                        cmd.Connection = conn;
                        cmd.CommandText = "insert into notes(code_eleve,code_mat,note) values" +
                            "("+code_ele +","+code_mat+","+note_mat+")";
                        cmd.ExecuteNonQuery();
                            
                    }
                    /*
                         Pour la moyenne elle sera calculé automatiquement par un trigger
                         une fois les notes sont saisies
                         Un autre trigger va incrementer le niveau de l'eleve
                         si la moyenne >= 12

                     */

                }
            }
        }


        // Rapport des eleves supprimes en HTML
        public static void ToHTML(string xml, string xslt)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xml);

            XmlDocument xsltDoc = new XmlDocument();
            xsltDoc.Load(xslt);

            XslCompiledTransform xsltTransform = new XslCompiledTransform();
            xsltTransform.Load(xsltDoc);

            XmlTextWriter writer = new XmlTextWriter(@"D:\ensat.html", null);

            xsltTransform.Transform(xmlDoc, null, writer);

            writer.Close();

        }

    }
}
