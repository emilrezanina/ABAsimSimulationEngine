using System.Collections.Generic;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriter;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public class DynamicAgent : AbstractAgent
    {
        private readonly Stack<string> _owners; 
        public DynamicAgent(IReciveSendMessage agentCommunication) 
            : base(agentCommunication)
        {
            _owners = new Stack<string>();
        }

        public void AddOwner(string controlAgentName)
        {
            _owners.Push(controlAgentName);
        }

        public string RemoveLastOwner()
        {
            return _owners.Pop();
        }

        public string GetLastOwner()
        {
            return _owners.Peek();
        }

        public void SetAgentModel(AgentModel ownerModel)
        {
            ControlModel = ownerModel;
        }

        protected override AgentManager CreateManager()
        {
            return null;
        }

        public override string ToString()
        {
            return "DynamicAgent: " + Manager;
        }
    }
}
