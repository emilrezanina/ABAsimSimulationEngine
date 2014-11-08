using SimulationEngine.Modules.AnimationModule;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace SimulationEngine.SimulationKernel
{
    public interface ISimulationControl
    {
        DiscreteSimulationModule DiscreteSimulation { get; }
        ContinuousSimulationModule ContinuousSimulation { get; }
        AnimationModule Animation { get; }
        ConfigurationModule Configuration { get; }

        long ActualTime { get; set; }
        short Speed { get; set; }
        bool Waiting { get; set; }
    }
}
