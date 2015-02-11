using System;
using System.Collections.Generic;
using System.Linq;
using SimulationEngine.Communication;
using SimulationEngine.Exceptions;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public abstract class AbstractAgent : IAgent
    {
        public AgentModel Model { get; protected set; }
        private readonly IList<IComponent> _components;
        public MessageRegister IncomingMessageRegister { get; private set; }
        public MessageRegister OutgoingMessageRegister { get; private set; }
        private readonly Mailbox _mailbox;
        public IReciveSendMessage MessageSenderAndReciever { get; set; }

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

        protected AbstractAgent(IReciveSendMessage messageSenderAndReciever, AgentManager manager)
        {
            _components = new List<IComponent>();
            IncomingMessageRegister = new MessageRegister();
            OutgoingMessageRegister = new MessageRegister();
            Manager = manager;
            _mailbox = new Mailbox();
            MessageSenderAndReciever = messageSenderAndReciever;
            _waitingOnResponseMessages = new List<Message>();
        }

        public IComponent GetComponent(string name)
        {
            return _manager.Name.Equals(name) ? _manager : 
                _components.FirstOrDefault(component => component.Name.Equals(name));
        }

        public void RegistrationComponent(IComponent component)
        {
            if (_components.Contains(component))
                throw new ComponentIsAlreadyRegistredException(component.Name, Manager.Name);

            _components.Add(component);
            component.ControlAgent = this;
        }

        public IComponent CancellingComponent(IComponent component)
        {
            return _components.Remove(component) ? component : null;
        }

        public virtual void ReciveMessage(Message message)
        {
            if (message.Type == TypeMessage.Request)
            {
                var ansferMessage = Message.CreateAnsferMessage(message);
                message.Answer = ansferMessage;
                _waitingOnResponseMessages.Add(message);
            }
            Manager.ProcessTheMessage(message);
        }

        public void AgentsComunnicationExecution(Message message)
        {
            if (message.Type == TypeMessage.Response)
                message = GetRequestMessageForResponseMessage(message);
            switch (message.AddressType)
            {
                case AddressType.Addressed: 
                    SendAdressMessage(message); 
                    break;
                case AddressType.PartiallyAddressed: 
                    SendPartialyAdressMessage(message); 
                    break;
                case AddressType.Unaddressed: 
                    SendNoAdressMessage(message); 
                    break;
                default:
                    throw new Exception();
            }
        }

        private Message GetRequestMessageForResponseMessage(Message message)
        {
            foreach (var waitingMessage in _waitingOnResponseMessages.Where(waitingMessage => 
                HasMessagesSameDataParameters(message, waitingMessage)))
            {
                _waitingOnResponseMessages.Remove(waitingMessage);
                var msg = waitingMessage.Answer;
                msg.DataParameters = message.DataParameters;
                return message;
            }
            return null;
        }

        private bool HasMessagesSameDataParameters(Message message, Message waitingMessage)
        {
            var foundMsg = IncomingMessageRegister.GetMessages().FirstOrDefault(messageFromRegister => 
                messageFromRegister.Code == message.Code);
            if (foundMsg == null)
                return false;
            return foundMsg.DataParameters.All(dataParameter => 
                message.DataParameters.ContainsKey(dataParameter.Key) 
                && waitingMessage.DataParameters.ContainsKey(dataParameter.Key));
        }

        private void SendAdressMessage(Message message)
        {
            MessageSenderAndReciever.SendMessage(message);
        }

        private void SendPartialyAdressMessage(Message message)
        {
            throw new NotImplementedException();
        }

        private void SendNoAdressMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IAgent other)
        {
            return Equals(_manager, other.Manager);
        }

        public override int GetHashCode()
        {
            return (Manager != null ? Manager.GetHashCode() : 0);
        }
    }
}
