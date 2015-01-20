using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules.ContinuousSimulationModule
{
    public class ContinuousSimulationController : IAttachedModule
    {
        public ISimulationContext Control { get; private set; }

        public ContinuousSimulationController(ISimulationContext control)
        {
            Control = control;
        }
    }
}
