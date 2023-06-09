
using Amazon.SQS;
using Amazon.SQS.Model;

var sqsConfig = new AmazonSQSConfig
{
    ServiceURL = "http://localhost:4566"
};
var sqsClient = new AmazonSQSClient(sqsConfig);

var requestConf = new ReceiveMessageRequest
{
    QueueUrl = "http://localhost:4566/000000000000/my-queue",
    MaxNumberOfMessages = 10
};

while (true)
{
    Console.WriteLine("Waiting for messages...");
    var messages = await sqsClient.ReceiveMessageAsync(requestConf);
    foreach (var message in messages.Messages)
    {
        Console.WriteLine($"Message received: {message.Body}");
        await sqsClient.DeleteMessageAsync(new DeleteMessageRequest
        {
            QueueUrl = "http://localhost:4566/000000000000/my-queue",
            ReceiptHandle = message.ReceiptHandle
        });
    }
}