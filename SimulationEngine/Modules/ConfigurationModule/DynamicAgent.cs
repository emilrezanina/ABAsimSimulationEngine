using System.Collections.Generic;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public class DynamicAgent : AbstractAgent
    {
        private readonly Stack<string> _owners; 
        public DynamicAgent(IReciveSendMessage agentCommunication, AgentManager manager) 
            : base(agentCommunication, manager)
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
            Model = ownerModel;
        }

        public override string ToString()
        {
            return "DynamicAgent: " + Manager;
        }
    }
}
