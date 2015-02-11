using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.Modules.SimulationModelModule.Components;
using SimulationEngineTests.Structures.AgentFactories;
using SimulationEngineTests.Structures.IdentficatorSets;
using Xunit;
using Xunit.Extensions;

namespace SimulationEngineTests
{
    public class SimulationModelTests
    {
        [Fact]
        public void ReturnMethodIsEmptyTrueIfSimulationModelIsWithoutUnprocessedMessages()
        {
            var simModel = new SimulationModel();
            Assert.True(simModel.IsEmpty());
        }

        [Fact]
        public void RegistredIncomingMessageIsInIncomingMessageRegister()
        {
            ControlAgentFactory controlAgentFactory = new SimpleControlAgentFactory();
            var agent = controlAgentFactory.CreateAgent();
            var msg = new Message(TypeMessage.Notice, null,agent.Manager.Name, CodeMessages.BeginTest, null, 0);
            agent.IncomingMessageRegister.RegistrationPrototypeMessage(msg);
            Assert.False(agent.IncomingMessageRegister.IsEmpty());
        }

        [Fact]
        public void AgentWithoutRegistrationMessageHasEmptyMessageRegisters()
        {
            ControlAgentFactory controlAgentFactory = new SimpleControlAgentFactory();
            var agent = controlAgentFactory.CreateAgent();
            Assert.True(agent.IncomingMessageRegister.IsEmpty());
            Assert.True(agent.OutgoingMessageRegister.IsEmpty());
        }


        [Fact]
        public void FindMessageWithSameTypeAndCodeInIncomingMessageRegister()
        {
            ControlAgentFactory controlAgentFactory = new SimpleControlAgentFactory();
            var agent = controlAgentFactory.CreateAgent();
            var msg = new Message(TypeMessage.Notice, null, agent.Manager.Name, CodeMessages.BeginTest, null, 0);
            msg.AddDataParameter(MessageParameterNames.Person, null);
            agent.IncomingMessageRegister.RegistrationPrototypeMessage(msg);
            Assert.Equal(msg, agent.IncomingMessageRegister.GetPrototypeMessage(msg.Type, msg.Code));
        }

        [Fact]
        public void FindedMessageWithSameTypeAndCodeInIncomingMessageRegisterHasAllDataParameters()
        {
            ControlAgentFactory controlAgentFactory = new SimpleControlAgentFactory();
            var agent = controlAgentFactory.CreateAgent();
            var msg = new Message(TypeMessage.Notice, null, agent.Manager.Name, CodeMessages.BeginTest, null, 0);
            msg.AddDataParameter(MessageParameterNames.Person, null);
            msg.AddDataParameter(MessageParameterNames.Bike, null);
            agent.IncomingMessageRegister.RegistrationPrototypeMessage(msg);
            var searchedMsg = agent.IncomingMessageRegister.GetPrototypeMessage(msg.Type, msg.Code);
            Assert.Equal(2, searchedMsg.DataParameters.Count);
            Assert.True(searchedMsg.DataParameters.ContainsKey(MessageParameterNames.Person));
            Assert.True(searchedMsg.DataParameters.ContainsKey(MessageParameterNames.Bike));
        }


        [Fact]
        public void RemoveRegistredMessageFromIncomingMessageRegisterReturnRightMessage()
        {
            ControlAgentFactory controlAgentFactory = new SimpleControlAgentFactory();
            var agent = controlAgentFactory.CreateAgent();
            var msg = new Message(TypeMessage.Notice, null, agent.Manager.Name, CodeMessages.BeginTest, null, 0);
            msg.AddDataParameter(MessageParameterNames.Person, null);
            agent.IncomingMessageRegister.RegistrationPrototypeMessage(msg);
            var deletedMsg = agent.IncomingMessageRegister.CancellingPrototypeMessage(msg);
            Assert.True(agent.IncomingMessageRegister.IsEmpty());
            Assert.Equal(msg, deletedMsg);
        }
    }
}
