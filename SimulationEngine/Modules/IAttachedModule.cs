using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules
{
    public interface IAttachedModule
    {
        ISimulationControl Control { get; }
    }
}
