using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Threading.Tasks;

namespace DynamoDbTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var asd = new AmazonDynamoDBConfig();
            asd.UseHttp = true;
            asd.ServiceURL = "http://localhost:4566";
            IAmazonDynamoDB serviceContext = new AmazonDynamoDBClient(asd);
            DynamoDBContext dynamoDBContext = new DynamoDBContext(serviceContext);

            try
            {
                await dynamoDBContext.SaveAsync(new TestTable { first = "Something 1", second = 1 });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    [DynamoDBTable("test_table")]
    public class TestTable
    {
        [DynamoDBHashKey]
        public string first { get; set; }

        [DynamoDBRangeKey]
        public int second { get; set; }
    }
}
