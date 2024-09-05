using System;
using TestAutomation.Framework.DomainLayer.Events.Interfaces;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Events.Args {
    public class StaleElementArgs : IArgs {
        /// <summary>
        /// StaleElement argüman sınıfı
        /// </summary>
        /// <param name="message">Message objesi alır.</param>
        public StaleElementArgs(Message message) {
            Message = message;
        }

        private Message message;

        /// <summary>
        /// Message property'si. Null set edilmesi durumunda InvalidOperationException döndürür.
        /// </summary>
        public Message Message
        {
            get
            {
                if (message != null)
                    return message;

                throw new InvalidOperationException("Message objesi boş olamaz.");
            }
            set
            {
                message = value;
            }
        }
    }
}
