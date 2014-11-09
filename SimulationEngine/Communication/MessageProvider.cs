using System.Collections.Generic;

namespace SimulationEngine.Communication
{
    public static class MessageProvider
    {
        private static int _sequenceNumber;
 
        public static Message CreateMessage(TypeMessage type, string sender, string addressee,
            string code, IDictionary<string, object> dataParameters, long timestamp)
        {
            var msg = new Message(type, sender, addressee, code, dataParameters, timestamp)
            {
                SequenceNumberCreation = _sequenceNumber++
            };
            return msg;
        }
    }
}
