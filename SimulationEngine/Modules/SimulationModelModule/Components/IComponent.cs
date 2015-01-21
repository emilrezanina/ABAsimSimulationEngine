using SimulationEngine.Communication;

namespace SimulationEngine.Modules.SimulationModelModule.Components
{
    public interface IComponent
    {
        void ProcessTheMessage(Message message);
        IAgent ControlAgent { get; set; }
        string Name { get; set; }
    }
}
