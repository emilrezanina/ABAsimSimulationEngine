using SimulationEngine.Communication;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace SimulationEngine.Modules.SimulationModelModule.Components
{
    public abstract class ContinuousAssistant : AddressableComponent
    {
        public IReciveSendMessage HoldTarget { get; private set; }

        protected ContinuousAssistant(string name, IReciveSendMessage holdTarget)
            : base(name)
        {
            HoldTarget = holdTarget;
        }

        public void SendHoldMessage(Message message)
        {
            HoldTarget.ReciveMessage(message);
        }

        public void SendFinishMessage(Message message)
        {
            message.Addressee = ControlAgent.Manager.Name;
            ControlAgent.Manager.ProcessTheMessage(message);
        }
    }
}
