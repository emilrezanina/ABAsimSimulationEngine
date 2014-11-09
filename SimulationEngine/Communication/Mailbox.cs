using System.Collections.Generic;
using System.Linq;

namespace SimulationEngine.Communication
{
    public class Mailbox
    {
        private readonly SortedSet<Message> _mailbox; 

        public Mailbox()
        {
            _mailbox = new SortedSet<Message>(new Message.MessageTimestampComparer());
        }

        public int MessageCount
        {
            get { return _mailbox.Count; }
        }

        public void AddMessage(Message message)
        {
            _mailbox.Add(message);
        }

        public Message RemoveMessage()
        {
            var removedMessage = _mailbox.First();
            _mailbox.Remove(removedMessage);
            return removedMessage;
        }

        public bool IsEmpty()
        {
            return MessageCount == 0;
        }

        public Message GetEarliestMessage()
        {
            return IsEmpty() ? null : _mailbox.First();
        }
    }
}
