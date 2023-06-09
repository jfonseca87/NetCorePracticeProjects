using Amazon.DynamoDBv2.DataModel;

namespace DynamoDbAPI.Models
{
    [DynamoDBTable("test_table")]
    public class TestTable
    {
        [DynamoDBHashKey]
        public string first { get; set; }

        [DynamoDBRangeKey]
        public int second { get; set; }
    }
}
