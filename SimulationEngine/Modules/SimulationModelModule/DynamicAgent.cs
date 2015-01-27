using System.Collections.Generic;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public class DynamicAgent : AbstractAgent
    {
        private readonly Stack<string> _owners; 
        public DynamicAgent(IReciveSendMessage agentCommunication, AgentManager manager) 
            : base(agentCommunication, manager)
        {
            _owners = new Stack<string>();

        }

        public string GetCurrentOwner()
        {
            return _owners.Peek();
        }

        public int GetOwnerCount()
        {
            return _owners.Count;
        }

        public void FullSetAgentModel(AgentModel model)
        {
            Model = model;
            _owners.Clear();
            _owners.Push(model.Agent.Manager.Name);
        }

        public void TemporarySetAgentModel(AgentModel model)
        {
            Model = model;
            _owners.Push(model.Agent.Manager.Name);
        }

        public override string ToString()
        {
            return "DynamicAgent: " + Manager;
        }
    }
}
