using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules
{
    public interface IAttachedModule
    {
        ISimulationContext Control { get; }
    }
}
