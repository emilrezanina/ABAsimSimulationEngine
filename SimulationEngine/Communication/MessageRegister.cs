using System.Collections.Generic;
using System.Linq;

namespace SimulationEngine.Communication
{
    public class MessageRegister
    {
        private readonly IList<Message> _messages;

        public MessageRegister()
        {
            _messages = new List<Message>();
        }

        public void RegistrationPrototypeMessage(Message msg)
        {
            _messages.Add(msg);
        }

        public Message CancellingPrototypeMessage(Message msg)
        {
            return _messages.Remove(msg) ? msg : null;
        }

        public Message GetPrototypeMessage(TypeMessage type, string code)
        {
            return _messages.FirstOrDefault(message => message.Type == type && message.Code == code);
        }

        public IEnumerable<Message> GetMessages()
        {
            return _messages;
        }

        public bool IsEmpty()
        {
            return _messages.Count == 0;
        }
    }
}
