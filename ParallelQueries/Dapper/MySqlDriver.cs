using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Dapper.Mysql
{
    public abstract class MySqlDriver
    {
        private MySqlConnection _conn;
        private readonly string _connStr;

        protected MySqlDriver()
        {
            _connStr = "server=127.0.0.1;port=3307;database=Test;uid=root;pwd=Abc.123456;";
        }

        protected async Task<T> GetFirstOfDefaultAsync<T>(string query)
        {
            using (_conn = new MySqlConnection(_connStr))
            {
                // If you handle by yourself the open action, you cannot be able to perform parallel queries
                // 'There is already an open DataReader associated with this Connection which must be closed first.'
                await _conn.OpenAsync();
                T obj = await _conn.QueryFirstOrDefaultAsync<T>(query);
                return obj;
            }
        }
    }
}
