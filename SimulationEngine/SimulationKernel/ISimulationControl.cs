using SimulationEngine.Modules.AnimationModule;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.SimulationKernel
{
    public interface ISimulationControl
    {
        DiscreteSimulationModule DiscreteSimulation { get; set; }
        ContinuousSimulationModule ContinuousSimulation { get; set; }
        AnimationModule Animation { get; set; }
        ConfigurationModule Configuration { get; set; }
        CommunicationOutputProvider MessageOutputProvider { get; }
        ActualTimeOutputProvider ActualTimeOutputProvider { get; }

        long ActualTime { get; set; }
        short Speed { get; set; }
        bool Waiting { get; set; }
    }
}
