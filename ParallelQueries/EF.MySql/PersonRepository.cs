using Microsoft.EntityFrameworkCore;
using Models;
using System.Threading.Tasks;

namespace EF.MySql
{
    public class PersonRepository
    {
        private readonly TestContext _db;

        public PersonRepository()
        {
            _db = new TestContext();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _db.Person.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
