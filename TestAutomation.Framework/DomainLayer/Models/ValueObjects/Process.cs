
namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    /// <summary>
    /// Process sınıfıdır.
    /// </summary>
    public class Process
    {
        /// <summary>
        /// Process Name'idir.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Message constructor methodudur.
        /// </summary>
        /// <param process="name">Process olarak belirtilicek string'i alır.</param>
        public Process(string name)
        {
            Name = name;
        }
    }
}
