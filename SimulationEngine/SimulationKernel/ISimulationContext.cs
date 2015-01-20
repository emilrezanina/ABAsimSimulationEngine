using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.SimulationKernel
{
    public interface ISimulationContext
    {
        DiscreteSimulationController DiscreteSimController { get; }
        SimulationModel SimModel { get; set; }

        ContinuousSimulationController ContinuousSimController { get; }
        
        CommunicationOutputProvider MessageOutputProvider { get; }
        ActualTimeOutputProvider ActualTimeOutputProvider { get; }

        long ActualTime { get; set; }
        short Speed { get; set; }
        bool Waiting { get; set; }
    }
}
