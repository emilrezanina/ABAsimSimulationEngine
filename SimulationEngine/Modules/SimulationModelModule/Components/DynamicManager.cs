using SimulationEngine.Communication;

namespace SimulationEngine.Modules.SimulationModelModule.Components
{
    public abstract class DynamicManager : AgentManager
    {
        protected DynamicManager(string name) : base(name)
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
