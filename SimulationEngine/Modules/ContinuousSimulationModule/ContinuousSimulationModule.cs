using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules.ContinuousSimulationModule
{
    public class ContinuousSimulationModule : IAttachedModule
    {
        public ISimulationControl Control { get; set; }
    }
}
