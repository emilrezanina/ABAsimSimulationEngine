using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;

namespace SimulationEngine.Components
{
    public interface IComponent
    {
        void ProcessTheMessage(Message message);
        IAgent ControlAgent { get; set; }
        string Name { get; set; }
    }
}
