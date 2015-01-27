using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.Modules.SimulationModelModule.Components;
using SimulationEngineTests.Structures.IdentficatorSets;

namespace SimulationEngineTests.Structures.AgentFactories
{
    class ControlAgentReceivedHandoverMessageFactory : ControlAgentFactory
    {
        private readonly IReciveSendMessage _control;
        public ControlAgentReceivedHandoverMessageFactory(IReciveSendMessage control)
        {
            _control = control;
        }

        public override ControlAgent CreateAgent()
        {
            ControlManager manager = new ManagerProccessingHandoverMessage(ComponentNames.AgentA);
            var agent = new ControlAgent(_control, manager);
            return agent;
        }
    }
}
