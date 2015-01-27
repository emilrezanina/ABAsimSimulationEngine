using SimulationEngine.Communication;

namespace SimulationEngine.Modules.SimulationModelModule.Components
{
    public abstract class ControlManager : AgentManager
    {
        protected ControlManager(string name) : base(name)
        {
        }

        public void SendHandoverMessage(Message message)
        {
            ControlAgent.AgentsComunnicationExecution(message);
        }

        public void SendEntrustMessage(Message message)
        {
            ControlAgent.AgentsComunnicationExecution(message);
        }

        //DODELAT
        public void SendReturnMessage(Message message)
        {

        }

        //DODELAT
        public void SendCancelMessage(Message message)
        {

        }

        //DODELAT
        public void SendGoalMessage(Message message)
        {

        }
    }
}
