using SimulationEngine.Modules.AnimationModule;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.SimulationKernel
{
    public interface ISimulationControl
    {
        DiscreteSimulationModule DiscreteSimulation { get; }
        ContinuousSimulationModule ContinuousSimulation { get; }
        ConfigurationModule Configuration { get; }
        CommunicationOutputProvider MessageOutputProvider { get; }
        ActualTimeOutputProvider ActualTimeOutputProvider { get; }

        long ActualTime { get; set; }
        short Speed { get; set; }
        bool Waiting { get; set; }
    }
}
