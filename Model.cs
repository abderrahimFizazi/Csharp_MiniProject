using System;
using System.Reflection;
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

        //public dynamic find()
        //{
        //    Dictionary<string, object> dico = new Dictionary<string, object>();
        //    sql = "select * from " + this.GetType().Name + " where id=" + id;
        //    Connexion Cnx = new Connexion(sql);

        //    return DictionaryToObject(dico);
        //}

    }
}

