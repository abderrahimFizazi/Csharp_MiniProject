using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prepa
{
    abstract public class Model
    {
        public static int id { get; set; }
        private string sql = "";

        public static string Capitalize(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

private Dictionary<string, T> ObjectToDictionary<T>(Object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dico = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
            return dico;
        }

        private dynamic DictionaryToObject(Dictionary<string, object> dico)
        {
            dynamic obj = Activator.CreateInstance(GetType());
            foreach (var kvp in dico)
            {
                PropertyInfo propertyInfo = (GetType()).GetProperty(kvp.Key);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    object value = kvp.Value;
                    if (value != null && value.GetType() != propertyInfo.PropertyType)
                    {
                        value = Convert.ChangeType(value, propertyInfo.PropertyType);
                    }
                    propertyInfo.SetValue(obj, value, null);
                }
            }
            return obj;
        }

        private static dynamic DictionaryToObject<T>(Dictionary<string, object> dico)
        {

            Type type = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (var kvp in dico)
            {
                PropertyInfo propertyInfo = type.GetProperty(kvp.Key);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    object value = kvp.Value;
                    if (value != null && value.GetType() != propertyInfo.PropertyType)
                    {
                        value = Convert.ChangeType(value, propertyInfo.PropertyType);
                    }
                    propertyInfo.SetValue(obj, value, null);
                }
            }

            return obj;
        }



        public dynamic find()
        {
            if (id == 0)
                //No record in database
                return -1;
            Dictionary<string, object> dico = new Dictionary<string, object>();
            sql = "select * from " + this.GetType().Name + " where id=" + id;
            Console.WriteLine(sql);
            using (IDataReader dr = Connexion.Select(sql))
            {
                while (dr.Read())
                {
                    //loop over the reader to get column names and values by index
                    for (var i = 0; i < dr.FieldCount; i++)
                    {
                        dico.Add(Capitalize(dr.GetName(i)), dr.GetValue(i));
                    }
                }
            }
            // return an object by converting the dictionary
            return dico;
        }
        public static dynamic find<T>(int id)
        {
            if (id == 0)
                //No record in database
                return -1;
            Dictionary<string, object> dico = new Dictionary<string, object>();
            string query = "select * from " + typeof(T).Name + " where id=" + id;
            using (IDataReader dr = Connexion.Select(query))
            {
                while (dr.Read())
                {
                    //loop over the reader to get column names and values by index
                    for (var i = 0; i < dr.FieldCount; i++)
                    {
                        dico.Add(Capitalize(dr.GetName(i)), dr.GetValue(i));
                    }
                }
            }
            return dico;
        }
        public int save()
        { 
                Dictionary<string, string> dico = new Dictionary<string, string>();
                dico = ObjectToDictionary<string>(this);
                if (id == 0)
                {
                    // Get the column names and values from the dictionary
                    string columnNames = string.Join(",", dico.Keys.Where(key => key != "Id"));
                    string values = string.Join(",", dico.Where(pair => pair.Key != "Id").Select(pair => $"'{pair.Value}'"));
                    // Create the INSERT statement using the class name of the object being saved
                    sql = $"INSERT INTO {this.GetType().Name} ({columnNames}) VALUES ({values})";
                }
                else
                {
                    //set column name = value
                    string set = string.Join(",", dico.Where(pair => pair.Key != "Id").Select(pair => $"{pair.Key} = '{pair.Value}'"));
                    sql = $"UPDATE {this.GetType().Name} SET {set} WHERE id= {id}";
                }
                // Execute the query
                Console.WriteLine(sql);
                return Connexion.IUD(sql);
              
        }
        public int delete() {
            if (id == 0)
                return -1;
            string req = $"Delete from {this.GetType().Name} where id = {id}";
            return Connexion.IUD(req);
        }
        public  List<dynamic> All()
        {
            List<dynamic> Collection = new List<dynamic>();
            sql = "select * from " + this.GetType().Name;
            using (IDataReader dr = Connexion.Select(sql))
            {
                while (dr.Read())
                {
                    Dictionary<string, object> dico = new Dictionary<string, object>();
                    //loop over the reader to get column names and values by index
                    for (var i = 0; i < dr.FieldCount; i++)
                        dico.Add(Capitalize(dr.GetName(i)), dr.GetValue(i));
                    Collection.Add(DictionaryToObject(dico));

                }
            }
            return Collection;
        }
        public static List<dynamic> all<T>()
        {

            List<dynamic> Collection = new List<dynamic>();
            string query = "select * from " + typeof(T).Name;
            using (IDataReader dr = Connexion.Select(query))
            {
                while (dr.Read())
                {
                    Dictionary<string, object> dico = new Dictionary<string, object>();
                    //loop over the reader to get column names and values by index
                    for (var i = 0; i < dr.FieldCount; i++)
                        dico.Add(Capitalize(dr.GetName(i)), dr.GetValue(i));
                    Collection.Add(DictionaryToObject<T>(dico));

                }
            }
            return Collection;
        }

    }

}


