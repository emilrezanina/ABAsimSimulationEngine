using System.Collections.Generic;
using System.Linq;
using SimulationEngine.Exceptions;

namespace SimulationEngine.Communication
{
    public class MessageRegister
    {
        private readonly IList<Message> _messages;

        public MessageRegister()
        {
            _messages = new List<Message>();
        }

        public void RegistrationMessagePrototype(Message message)
        {
            if (_messages.Any(msgPrototype => msgPrototype.Type == message.Type 
                                              && msgPrototype.Code == message.Code))
                throw new MessagePrototypeIsRegistredException(message);

            _messages.Add(message);
        }

        public Message CancellingMessagePrototype(Message message)
        {
            return _messages.Remove(message) ? message : null;
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
