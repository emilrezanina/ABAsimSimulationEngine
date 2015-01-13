using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public class ConfigurationModule : IAttachedModule
    {
        public ISimulationControl Control { get; private set; }

        public SimulationModel Model { get; set; }

        public ConfigurationModule(ISimulationControl control)
        {
            Control = control;
        }
    }
}
