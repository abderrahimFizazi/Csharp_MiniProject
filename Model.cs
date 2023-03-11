using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using Newtonsoft.Json;

namespace Prepa
{
	abstract class Model
	{
        public int id { get; set; }
        private string sql = "";


        Dictionary<string, T> ObjectToDictionary<T>(object obj)  // This Method Convert Object to Dictionary
        {

            // Stuck overflow :)
            // https://stackoverflow.com/questions/11576886/how-to-convert-object-to-dictionarytkey-tvalue-in-c
            // Add Newtonsoft Package from Nuget...

            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
            return dictionary;
        }

        private dynamic DictionaryToObject(Dictionary<String, object> dico)
            // We use here a dynamic type return cuz we dont know the exact type of the object
            //  I copied this from an older project :(
        {
            if (dico.Count == 0) return null;
            Type type = GetType();
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dico)
            {
                PropertyInfo info = type.GetProperty(kv.Key);
                info.SetValue(obj, Convert.ChangeType(kv.Value, info.PropertyType));
            }
            return Convert.ChangeType(obj, this.GetType());
        }

        public int save()
        {
            Dictionary<string, string> dico = new Dictionary<string, string>();
            dico = ObjectToDictionary<string>(this);


            return 0;
        }

        public dynamic find()
        {
            sql = "select * from " + this.GetType().Name + " where id=" + id;

            Dictionary<string, string> champs = new Dictionary<string, string>();
            Dictionary<string, object> dico = new Dictionary<string, object>();


            champs = Connexion.getChamps_Table(GetType().Name);
            IDataReader reader = Connexion.Select(sql);

            string champsName = "";
            Type type = null;
            int index = 0;
            int nbr_Champs = reader.FieldCount;
            while (reader.Read())
            {
                for (int i = 0;i < nbr_Champs; i++) {
                    champsName = champs.Keys.ElementAt(i);
                    type = GetFieldType(champs.Values.ElementAt(i)); 
                }
            }

            return DictionaryToObject(dico);
        }

    }
}

