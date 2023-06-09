using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using DynamoDbAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamoDbAPI.Controllers
{
    [ApiController]
    [Route("dynamodbcrud")]
    public class DynamoDbCrudController : ControllerBase
    {
        private readonly DynamoDBContext _client;

        public DynamoDbCrudController(IAmazonDynamoDB context)
        {
            _client = new DynamoDBContext(context);
        }

        [HttpGet]
        public async Task<IActionResult> GetTestTableRecords()
        {
            try
            {
                List<TestTable> testRecords = new List<TestTable>();
                var search = _client.FromScanAsync<TestTable>(new ScanOperationConfig(), new DynamoDBOperationConfig());

                while (!search.IsDone)
                {
                    var data = await search.GetNextSetAsync();
                    testRecords.AddRange(data);
                }

                return Ok(testRecords);
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddTestTableRecord(TestTable testTable)
        {
            try
            {
                await _client.SaveAsync(testTable);
                return Ok(testTable);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
