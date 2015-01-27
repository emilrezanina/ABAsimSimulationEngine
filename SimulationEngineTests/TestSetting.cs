using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulationKernel;
using SimulationEngineTests.Structures.AgentFactories;

namespace SimulationEngineTests
{
    internal class TestSetting
    {
        public static ControlAgent CreateAgentRecievedHandoverMessage()
        {
            var simulationContext = new SimulationContext();
            var simulationModel = new SimulationModel();
            ControlAgentFactory controlAgentFactory = new ControlAgentReceivedHandoverMessageFactory(simulationContext.DiscreteSimController);
            var agent = controlAgentFactory.CreateAgent();
            simulationModel.RegistrationControlAgent(agent);
            simulationContext.SimModel = simulationModel;
            return agent;
        }
    }
}