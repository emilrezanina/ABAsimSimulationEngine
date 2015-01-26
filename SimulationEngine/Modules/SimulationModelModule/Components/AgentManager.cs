using System;
using SimulationEngine.Communication;

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
            //presunuti zpravy do meziagentove komunikace
            ControlAgent.AgentsComunnication(message);
        }

        public void SendRequestMessage(Message message)
        {
            ControlAgent.AgentsComunnication(message);
        }

        public void SendResponseMessage(Message message)
        {
            ControlAgent.AgentsComunnication(message);
        }

        public void SendCallMessage(Message message)
        {
            ControlAgent.AgentsComunnication(message);
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
                throw new Exception("Assistant " + message.Addressee + " not found.");
            }
        }
    }
}
