using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimulationEngine.Modules.SimulationModelModule;

namespace SimulationEngine.Communication
{
    public class Message
    {
        public int SequenceNumberCreation { get; set; }
        public TypeMessage Type { get; set; }           //typ zpravy
        public string Sender { get; set; }      //odesilatel
        public string Addressee { get; set; }   //adresat
        public string Code { get; set; }               //kod zpravy
        public long Timestamp { get; set; }             //cas doruceni, casove razitko
        public AddressType AddressType { get; set; }   //typ adresovani
        public Message Answer { get; set; }            //odpoved na zpravu - pro request
        public string Result { get; set; }             //vysledek

        public IDictionary<string, object> DataParameters { get; set; }
        public DynamicAgent DynamicAgent { get; set; }

        public void AddDataParameter(string key, object data)
        {
            DataParameters.Add(key, data);
        }

        public void AddDataParameters(IDictionary<string, object> dataParameters)
        {
            foreach (var dataParameter in dataParameters)
            {
                DataParameters.Add(dataParameter.Key, dataParameter.Value);
            }
        }

        public object DeleteDataParameter(String key)
        {
            var deletedDataParameter = DataParameters[key];
            DataParameters.Remove(key);
            return deletedDataParameter;
        }

        public Message(TypeMessage type, string sender, string addressee,
            string code, IDictionary<string, object> dataParameters, long timestamp)
        {
            Type = type;
            Addressee = addressee;
            Code = code;
            Sender = sender;
            DataParameters = dataParameters ?? new Dictionary<string, object>();
            Timestamp = timestamp;
            AddressType = AddressType.Addressed;
            //dynamicAgent = null;
        }

        public static Message CreateAnsferMessage(Message message)
        {
            var ansferMessage = MessageProvider.CreateMessage(TypeMessage.Response,
                    message.Addressee, message.Sender, message.Code,
                    message.DataParameters, message.Timestamp);
            return ansferMessage;
        }

        public int CompareTo(Message second)
        {
            if (Timestamp > second.Timestamp)
            {
                return 1;
            }
            if (Timestamp > second.Timestamp)
            {
                return -1;
            }
            return 0;
        }

        public class MessageTimestampComparer : Comparer<Message>
        {
            public override int Compare(Message x, Message y)
            {
                if (x == null || y == null)
                    throw new ArgumentNullException();

                var compareValue = x.Timestamp.CompareTo(y.Timestamp);
                if (compareValue == 0)
                    compareValue = x.SequenceNumberCreation.CompareTo(y.SequenceNumberCreation);

                return compareValue;
            }
        };

        public override String ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(" Message{");
            stringBuilder.Append("Timestamp: " + Timestamp);
            stringBuilder.Append(", Type: " + Type);
            stringBuilder.Append(", From: " + Sender);
            stringBuilder.Append(", To: " + Addressee);
            stringBuilder.Append(", Code: " + Code);
            stringBuilder.Append(", Data: {");
            if (!DataParameters.Any())
                stringBuilder.Append("NULL");
            foreach (var dataParameter in DataParameters)
            {
                stringBuilder.Append(dataParameter.Key + ' ');
            }
            stringBuilder.Append("};");
            return stringBuilder.ToString();
        }
    }
}
