using MySql.Data.MySqlClient;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ADO.MySql
{
    public abstract class MySqlDriver
    {
        MySqlConnection _conn;
        private readonly string _connStr;

        protected MySqlDriver()
        {
            _connStr = "server=127.0.0.1;port=3307;database=Test;uid=root;pwd=Abc.123456;";
        }

        protected async Task<T> GetFirstOfDefaultAsync<T>(string query) 
        {
            T obj = default;
            using (_conn = new MySqlConnection(_connStr))
            {
                using MySqlCommand cmd = new MySqlCommand(query, _conn);
                await _conn.OpenAsync();
                using MySqlDataReader dr = cmd.ExecuteReader();
                while (await dr.ReadAsync())
                {
                    obj = SetItem<T>(dr);
                }

                return obj;
            }
        }

        private T SetItem<T>(MySqlDataReader dr)
        {
            Type objType = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (PropertyInfo property in objType.GetProperties())
            {
                property.SetValue(obj, dr[property.Name], null);
            }

            return obj;
        }
    }
}
