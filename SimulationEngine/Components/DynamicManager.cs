using SimulationEngine.Communication;

namespace SimulationEngine.Components
{
    public abstract class DynamicManager : AgentManager
    {
        protected DynamicManager(string componentName) : base(componentName)
        {
        }

        //DODELAT
        public void SendDoneMessage(Message message)
        {

        }
        //DODELAT
        public void SendTransferMessage(Message message)
        {

        }
    }
}
