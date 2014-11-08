using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriter;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public class ControlAgent : AbstractAgent 
    {
        public ControlAgent(IReciveSendMessage agentCommunication) 
            : base(agentCommunication)
        {
            ControlModel = new AgentModel(this);
        }

        protected override AgentManager CreateManager()
        {
            return null;
        }

        public override string ToString()
        {
            return "ControlAgent: " + Manager;
        }
    }
}
