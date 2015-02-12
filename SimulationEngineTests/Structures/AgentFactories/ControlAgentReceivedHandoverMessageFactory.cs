using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.Modules.SimulationModelModule.Components;
using SimulationEngineTests.Structures.Components;
using SimulationEngineTests.Structures.IdentficatorSets;

namespace SimulationEngineTests.Structures.AgentFactories
{
    public class ControlAgentReceivedHandoverMessageFactory : ControlAgentFactory
    {
        private readonly IReciveSendMessage _control;
        public ControlAgentReceivedHandoverMessageFactory(IReciveSendMessage control)
        {
            _control = control;
        }

        public override ControlAgent CreateAgent(string name)
        {
            ControlManager manager = new ManagerProccessingHandoverMessage(name);
            var agent = new ControlAgent(_control, manager);
            return agent;
        }
    }
}
