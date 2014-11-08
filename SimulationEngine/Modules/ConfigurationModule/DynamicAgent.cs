using System.Collections.Generic;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public class DynamicAgent : AbstractAgent
    {
        private readonly Stack<ComponentName> _owners; 
        public DynamicAgent(IReciveSendMessage agentCommunication) : base(agentCommunication)
        {
            _owners = new Stack<ComponentName>();
        }

        public void AddOwner(ComponentName controlAgentName)
        {
            _owners.Push(controlAgentName);
        }

        public ComponentName RemoveLastOwner()
        {
            return _owners.Pop();
        }

        public ComponentName GetLastOwner()
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
