using System.Collections.Generic;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public class AgentModel
    {
        public ControlAgent Agent { get; set; }
        public AgentModel ControlModel { get; set; }
        public IList<DynamicAgent> DynamicAgents { get; private set; }
        public IList<AgentModel> Submodels { get; private set; }

        public AgentModel(ControlAgent agent)
        {
            DynamicAgents = new List<DynamicAgent>();
            Submodels = new List<AgentModel>();
            Agent = agent;
        }

        public void AddDynamicAgent(DynamicAgent agent)
        {
            DynamicAgents.Add(agent);
            agent.FullSetAgentModel(this);
        }
        public DynamicAgent RemoveDynamicAgent(DynamicAgent agent)
        {
            return DynamicAgents.Remove(agent) ? agent : null;
        }

        public void AddSubmodel(AgentModel model)
        {
            if (Submodels.Contains(model))
                return;
            Submodels.Add(model);
        }

        public AgentModel RemoveSubmodel(AgentModel model)
        {
            return Submodels.Remove(model) ? model : null;
        }
    }
}
