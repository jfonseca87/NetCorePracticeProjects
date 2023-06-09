
using Amazon.SQS;
using Amazon.SQS.Model;

var sqsConfig = new AmazonSQSConfig
{
    ServiceURL = "http://localhost:4566"
};

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = "http://localhost:4566/000000000000/my-queue",
    MessageBody = "Hello from SQS!"
};

try
{
    var sqsClient = new AmazonSQSClient(sqsConfig);
    var response = await sqsClient.SendMessageAsync(sendMessageRequest);
}
catch (Exception ex)
{
	throw;
}


var some = string.Empty;
