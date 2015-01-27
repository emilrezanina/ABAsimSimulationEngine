using SimulationEngine.Modules.SimulationModelModule;

namespace SimulationEngineTests.Structures.AgentFactories
{
    abstract class ControlAgentFactory
    {
        public abstract ControlAgent CreateAgent();
    }
}
