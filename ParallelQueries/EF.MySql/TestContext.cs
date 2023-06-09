using Microsoft.EntityFrameworkCore;
using Models;

namespace EF.MySql
{
    public class TestContext : DbContext
    {
        public DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;port=3307;database=Test;uid=root;pwd=Abc.123456;");
        }
    }
}
