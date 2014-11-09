using System;
using System.Collections.Generic;
using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public abstract class AbstractAgent : IAgent
    {
        public AgentModel ControlModel { get; protected set; }
        private readonly IList<IComponent> _components;
        private readonly IDictionary<string, string[]> _mapOfOwnCodesMessages;
        private readonly Mailbox _mailbox;
        private readonly IReciveSendMessage _agentCommunication;
        protected CommunicationOutputProvider MessageOutputProvider;
        //POUZE NA EVIDENCI
        private readonly IList<Message> _waitingOnResponseMessages;

        private AgentManager _manager;
        public AgentManager Manager 
        {
            get { return _manager; }
            set
            {
                _manager = value;
                _manager.ControlAgent = this;
            }
        }

        protected AbstractAgent(IReciveSendMessage agentCommunication)
        {
            //NENI DODELANY CONTROLMODEL
            ControlModel = null;
            _components = new List<IComponent>();
            _mapOfOwnCodesMessages = new Dictionary<string, string[]>();
            _manager = CreateManager();
            _mailbox = new Mailbox();
            _agentCommunication = agentCommunication;
            MessageOutputProvider = agentCommunication.MessageOutputProvider;
            _waitingOnResponseMessages = new List<Message>();
        }

        public IComponent GetComponent(string name)
        {
            if (_manager.Name.Equals(name))
            {
                return _manager;
            }
            foreach (var component in _components)
            {
                if (component.Name.Equals(name))
                    return component;
            }
            return null;
        }

        public int MessageCount
        {
            get { return _mailbox.MessageCount; }
        }

        public void RegistrationComponent(IComponent component)
        {
            if (!_components.Contains(component))
            {
                _components.Add(component);
                component.ControlAgent = this;
            }
            else
            {
                //OSETRIT VYJMKU, ZE UZ TAM DANY KOMPONENT JE
                throw new Exception("Component " + component.Name + "is already registred in Agent.");
            }
        }

        public IComponent CancellingComponent(IComponent component)
        {
            return _components.Remove(component) ? component : null;
        }

        public void RegistrationCodeMessage(string codeMessage, params string[] attributes)
        {
            _mapOfOwnCodesMessages.Add(codeMessage, attributes);
        }

        public string CancellingCodeMessage(string codeMessage)
        {
            return _mapOfOwnCodesMessages.Remove(codeMessage) ? codeMessage : null;
        }

        public bool HasCodeMessage(string codeMessage)
        {
            return _mapOfOwnCodesMessages.ContainsKey(codeMessage);
        }

        public void ReciveMessage(Message message)
        {
            if (message.Type == TypeMessage.Request)
            {
                var ansferMessage = Message.CreateAnsferMessage(message);
                message.Answer = ansferMessage;
                _waitingOnResponseMessages.Add(message);
            }
            _mailbox.AddMessage(message);
        }

        public void ProcessAllMessages()
        {
            while (_mailbox.MessageCount != 0)
            {
                var message = _mailbox.RemoveMessage();
                MessageOutputProvider.TraceReceivedMessage(message);
                var component = GetComponent(message.Addressee);
                if (component != null)
                {
                    component.ProcessTheMessage(message);
                }
                else
                {
                    //VYVOLANI VYJIMKY, ZE NEBYL NALEZ ADRESAT
                    throw new Exception("Adressee " + message.Addressee + "not found.");
                }
            }
        }

        public void AgentsComunnication(Message message)
        {
            if (message.Type == TypeMessage.Response)
                message = FindingRequestMessage(message);
            //2. VYRESIT JESTLI SE JEDNA O MEZIAGENTOVOU KOMUNIKACI
            switch (message.AddressType)
            {
                //a) jestli se jedna o adresni zpravu
                case AddressType.Address: SendAdressMessage(message); break;
                //b) jestli se jedna o castne adresni zpravu
                case AddressType.PartialyAddress: SendPartialyAdressMessage(message); break;
                //c) jestli se jedna o neadresni zpravu
                case AddressType.NoAddress: SendNoAdressMessage(message); break;
                default:
                    throw new Exception();
            }
        }

        private Message FindingRequestMessage(Message message)
        {
            {
                //ziskani potrebnych atributu pro nalezeni spravneho requestu
                var attributes = _mapOfOwnCodesMessages[message.Code];
                foreach (var waitingMessage in _waitingOnResponseMessages)
                {
                    //kdyz ma zprava podle vnitrni tabulky request
                    //urcity pocet potrebnych identifikacnich kodu tak to vezme
                    //vnitrni tabulka obsahuje: klic = kod zpravy, nazvy atributu
                    //odstraneni te zpravy
                    var same = true;
                    foreach (var atribut in attributes)
                    {
                        if (!message.DataParameters[atribut].Equals(waitingMessage.DataParameters[atribut]))
                        {
                            same = false;
                            break;
                        }
                    }
                    if (same)
                    {
                        _waitingOnResponseMessages.Remove(waitingMessage);
                        var msg = waitingMessage.Answer;
                        msg.DataParameters = message.DataParameters;
                        return message;
                    }
                }
                return null;
            }
        }

        private void SendAdressMessage(Message message)
        {
            _agentCommunication.SendMessage(message);
        }

        private void SendPartialyAdressMessage(Message message)
        {

        }

        private void SendNoAdressMessage(Message message)
        {

        }

        public bool Equals(IAgent other)
        {
            return Equals(_manager, other.Manager);
        }

        public override int GetHashCode()
        {
            return (Manager != null ? Manager.GetHashCode() : 0);
        }

        protected abstract AgentManager CreateManager();
    }
}
