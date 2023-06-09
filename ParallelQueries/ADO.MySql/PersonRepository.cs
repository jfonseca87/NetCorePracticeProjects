using Models;
using System.Threading.Tasks;

namespace ADO.MySql
{
    public class PersonRepository : BaseRepository
    {
        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await GetFirstOfDefaultAsync<Person>($"select * from person where id = {id}");
        }
    }
}
