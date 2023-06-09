using WebApplication2.repository.Interfaces;

namespace WebApplication2.repository.Implementations
{
    public class SummaryRepository : ISummaryRepository
    {
        public string[] GetSummaries()
        {
            return new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
        }
    }
}
