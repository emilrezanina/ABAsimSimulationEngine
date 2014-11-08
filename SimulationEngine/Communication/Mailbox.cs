using System.Collections.Generic;

namespace SimulationEngine.Communication
{
    public class Mailbox
    {
        private readonly IList<Message> _mailbox;

        public Mailbox()
        {
            _mailbox = new List<Message>();
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
            var removedMessage = _mailbox[0];
            _mailbox.RemoveAt(0);
            return removedMessage;
        }

        public bool IsEmpty()
        {
            return MessageCount == 0;
        }

        public Message GetEarliestMessage()
        {
            return IsEmpty() ? null : _mailbox[0];
        }
    }
}
