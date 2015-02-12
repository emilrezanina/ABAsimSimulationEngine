using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulationKernel;
using SimulationEngineTests.Structures.AgentFactories;
using SimulationEngineTests.Structures.IdentficatorSets;

namespace SimulationEngineTests
{
    internal class TestSetting
    {
        public static ControlAgent CreateAgentRecievedHandoverMessage()
        {
            var simulationContext = new SimulationContext();
            var simulationModel = new SimulationModel();
            ControlAgentFactory controlAgentFactory = new ControlAgentReceivedHandoverMessageFactory(simulationContext.DiscreteSimController);
            var agent = controlAgentFactory.CreateAgent(ComponentNames.AgentA);
            simulationModel.RegistrationControlAgent(agent);
            simulationContext.SimModel = simulationModel;
            return agent;
        }

        public static Message GetSimpleMessageWithDataParameters(string code, int numberOfParam)
        {
            var msg = new Message(TypeMessage.Notice, ComponentNames.AgentA, ComponentNames.AgentB,
                code, null, 0);
            for (var i = 0; i < numberOfParam; i++)
            {
                msg.AddDataParameter("Param" + i, i);
            }
            
            return msg;
        }
    }

    
}