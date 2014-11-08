using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules.AnimationModule
{
    public class AnimationModule : IAttachedModule
    {
        public ISimulationControl Control { get; private set; }

        public AnimationModule(ISimulationControl control)
        {
            Control = control;
        }
    }
}
