using SimulationEngine.Communication;
using SimulationEngine.Modules.ConfigurationModule;

namespace SimulationEngine.Components
{
    public interface IComponent
    {
        void ProcessTheMessage(Message message);
        IAgent ControlAgent { get; set; }
        string Name { get; set; }
    }
}
