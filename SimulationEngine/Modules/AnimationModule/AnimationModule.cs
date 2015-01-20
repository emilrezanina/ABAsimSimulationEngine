using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules.AnimationModule
{
    public class AnimationModule : IAttachedModule
    {
        public ISimulationContext Control { get; private set; }

        public AnimationModule(ISimulationContext control)
        {
            Control = control;
        }
    }
}
