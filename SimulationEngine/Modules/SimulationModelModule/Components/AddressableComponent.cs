using SimulationEngine.Communication;

namespace SimulationEngine.Modules.SimulationModelModule.Components
{
    public abstract class AddressableComponent : AbstractComponent
    {
        protected AddressableComponent(string componentName) : base(componentName)
        {
        }

        public void SendNoticeMessage(Message message)
        {
            message.Addressee = ControlAgent.Manager.Name;
            ControlAgent.Manager.ProcessTheMessage(message);
        }
    }
}
