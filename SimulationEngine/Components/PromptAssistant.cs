using SimulationEngine.Communication;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace SimulationEngine.Components
{
    public abstract class PromptAssistant : AbstractComponent
    {
        public IReciveSendMessage HoldTarget { get; private set; }
        protected PromptAssistant(string componentName, IReciveSendMessage holdTarget) : base(componentName)
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
