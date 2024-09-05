namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    /// <summary>
    /// Message sınıfıdır.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Message text'idir.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Message constructor methodudur.
        /// </summary>
        /// <param name="text">Message olarak belirtilicek string'i alır.</param>
        public Message(string text)
        {
            Text = text;
        }
    }
}
