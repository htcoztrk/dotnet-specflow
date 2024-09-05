using TestAutomation.Framework.DomainLayer.Events.Interfaces;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;

namespace TestAutomation.Framework.DomainLayer.Events.Args {
    public class SystemNotifierArgs : IArgs {
        public SystemNotifierArgs(Message message) {
            Message = message;
        }

        public Message Message { get; set; }
    }
}
