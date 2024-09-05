
namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    public class Session
    {
        public string Id { get; private set; }

        public Session(string sessionId)
        {
            Id = sessionId;
        }
    }
}
