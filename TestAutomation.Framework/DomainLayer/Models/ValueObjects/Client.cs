
namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    public class Client
    {
        public string IP { get; private set; }

        public Client(string clientIP)
        {
            IP = clientIP;
        }
    }
}