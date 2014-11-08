using System;
using System.Collections.Generic;
using SimulationEngine.Components;
using SimulationEngine.Modules.ConfigurationModule;

namespace SimulationEngine.Communication
{
    

    public class Message
    {
        public TypeMessage Type { get; set; }           //typ zpravy
        public string Sender { get; set; }      //odesilatel
        public string Addressee { get; set; }   //adresat
        public string Code { get; set; }               //kod zpravy
        public int Timestamp { get; set; }             //cas doruceni, casove razitko
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

        public Message(TypeMessage typeMessage, string sender, string addressee, 
            string codeMessage, IDictionary<string, object> dataParameters, int timestamp)
        {
            Type = typeMessage;
            Addressee = addressee;
            Code = codeMessage;
            Sender = sender;
            DataParameters = dataParameters ?? new Dictionary<string, object>();
            Timestamp = timestamp;
            AddressType = AddressType.Address;
            //dynamicAgent = null;
        }

        public Message(TypeMessage typeMessage, string sender, string addressee,
            string codeMessage)
        {
            Type = typeMessage;
            Addressee = addressee;
            Code = codeMessage;
            Sender = sender;
            DataParameters = new Dictionary<string, object>();
            AddressType = AddressType.Address;
            //dynamicAgent = null;
        }

        public static Message CreateAnsferMessage(Message message)
        {
            var ansferMessage = new Message(TypeMessage.Response,
                    message.Addressee,
                    message.Sender,
                    message.Code,
                    message.DataParameters,
                    message.Timestamp);
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

        public override String ToString()
        {
            return "Message{"
                    + "typeMessage=" + Type
                    + ", sender=" + Sender
                    + ", addressee=" + Addressee
                    + ", codeMessage=" + Code
                    + ", data=" + DataParameters
                    + ", timestamp=" + Timestamp
                    + ", addressType=" + AddressType
                    + ", answer=" + Answer
                    //+ ", dynamicAgent=" + dynamicAgent.getManager().getComponentName()
                    + '}';
        }
    }
}
