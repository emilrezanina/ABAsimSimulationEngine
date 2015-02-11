using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.Modules.SimulationModelModule.Components;
using SimulationEngineTests.Structures.Components;
using SimulationEngineTests.Structures.IdentficatorSets;

namespace SimulationEngineTests.Structures.AgentFactories
{
    public class SimpleControlAgentFactory : ControlAgentFactory
    {
        public override ControlAgent CreateAgent()
        {
            ControlManager manager = new SimpleControlManager(ComponentNames.AgentA);
            var agent = new ControlAgent(null, manager);
            return agent;
        }
    }
}
