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
    }
}
