using SimulationEngine.Communication;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public class ControlAgent : AbstractAgent 
    {
        public ControlAgent(IReciveSendMessage agentCommunication, AgentManager manager) 
            : base(agentCommunication, manager)
        {
            Model = new AgentModel(this);
        }

        public override string ToString()
        {
            return "ControlAgent: " + Manager;
        }

        public override void ReciveMessage(Message message)
        {
            if (message.Type == TypeMessage.Handover)
            {
                var transferedDynamicAgent = message.DynamicAgent;
                Model.AddDynamicAgent(transferedDynamicAgent);
                transferedDynamicAgent.FullSetAgentModel(Model);
            }

            if (message.Type == TypeMessage.Entrust)
            {
                var transferedDynamicAgent = message.DynamicAgent;
                Model.AddDynamicAgent(transferedDynamicAgent);
                transferedDynamicAgent.TemporarySetAgentModel(Model);
            }
            base.ReciveMessage(message);
        }
        
    }
}
