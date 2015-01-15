using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace SimulationEngine.Modules.ConfigurationModule
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
    }
}
