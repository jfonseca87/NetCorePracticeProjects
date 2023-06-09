
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelQueries
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tasks = new List<Task<Person>>();
            var listId = new List<int> { 1, 3, 4, 5, 6, 7, 8 };

            // ADO Mysql Object
            //ADO.MySql.PersonRepository repository = new ADO.MySql.PersonRepository();
            //listId.ForEach(num => tasks.Add(repository.GetPersonByIdAsync(num)));
            //var result = await Task.WhenAll(tasks);

            // Dapper Mysql Object - Option 1
            // Dapper.Mysql.MySqlDriver driver = new Dapper.Mysql.MySqlDriver();
            // listId.ForEach(num => tasks.Add(driver.GetFirstOfDefaultAsync<Person>($"select * from person where id = { num }")));
            // var result = await Task.WhenAll(tasks);

            // Dapper Mysql Object - Option 2
            //listId.ForEach(num => tasks.Add(Task.Run(() =>
            //{
            //    Dapper.MySql.PersonRepository repository = new Dapper.MySql.PersonRepository();
            //    return repository.GetPersonByIdAsync(num);
            //})));
            //var result = await Task.WhenAll(tasks);

            // Entity Framework Object
            listId.ForEach(num => tasks.Add(Task.Run(() =>
            {
                EF.MySql.PersonRepository repository = new EF.MySql.PersonRepository();
                return repository.GetPersonByIdAsync(num);
            })));
            var result = await Task.WhenAll(tasks);

            Console.Read();
        }
    }
}
