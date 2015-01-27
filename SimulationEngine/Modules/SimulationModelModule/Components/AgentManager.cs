using SimulationEngine.Communication;
using SimulationEngine.Exceptions;

namespace SimulationEngine.Modules.SimulationModelModule.Components
{
    public abstract class AgentManager : AddressableComponent
    {
        protected AgentManager(string name) : base(name)
        {
        }

        public void SendStartMessage(Message message)
        {
            CommunicationToAssistent(message);
        }

        public void SendBreakMessage(Message message)
        {
            CommunicationToAssistent(message);
        }

        public void SendExecuteMessage(Message message)
        {
            CommunicationToAssistent(message);
        }

        public new void SendNoticeMessage(Message message)
        {
            ControlAgent.AgentsComunnicationExecution(message);
        }

        public void SendRequestMessage(Message message)
        {
            ControlAgent.AgentsComunnicationExecution(message);
        }

        public void SendResponseMessage(Message message)
        {
            ControlAgent.AgentsComunnicationExecution(message);
        }

        public void SendCallMessage(Message message)
        {
            ControlAgent.AgentsComunnicationExecution(message);
        }

        protected void CommunicationToAssistent(Message message)
        {
            var component = ControlAgent.GetComponent(message.Addressee);
            if (component != null)
            {
                component.ProcessTheMessage(message);
            }
            else
            {
                throw new AddresseeNotFoundException(message.Addressee);
            }
        }
    }
}
