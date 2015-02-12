using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.Modules.SimulationModelModule.Components;
using SimulationEngineTests.Structures.Components;

namespace SimulationEngineTests.Structures.AgentFactories
{
    public class SimpleControlAgentFactory : ControlAgentFactory
    {
        public override ControlAgent CreateAgent(string name)
        {
            ControlManager manager = new SimpleControlManager(name);
            var agent = new ControlAgent(null, manager);
            return agent;
        }
    }
}
