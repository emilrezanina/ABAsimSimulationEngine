using System.Drawing;
using DynamicAgentsTestingProject.NamesManagers;
using DynamicAgentsTestingProject.Structures;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulationKernel;

namespace DynamicAgentsTestingProject
{

    class Program
    {
        static void Main(string[] args)
        {
            var simKernel = new SimulationContext();
            var simModel = new SimulationModel();

            var agentSurroundings = GetAgentSurroundings(simKernel);
            simModel.RegistrationControlAgent(agentSurroundings);

            var agentModel = GetAgentModel((simKernel));
            simModel.RegistrationControlAgent(agentModel);

            var regionA = new Rectangle(0, 0, 50, 50);
            var agentRegionA = GetAgentRegion(simKernel, ComponentNameManager.AgentRegionA, regionA);
            simModel.RegistrationControlAgent(agentRegionA);


            var regionB = new Rectangle(0, 50, 50, 50);
            var agentRegionB = GetAgentRegion(simKernel, ComponentNameManager.AgentRegionB, regionB);
            simModel.RegistrationControlAgent(agentRegionB);

            simKernel.Run();
        }

        private static ControlAgent GetAgentRegion(ISimulationContext simKernel, string name, Rectangle region)
        {
            var managerRegion = new ManagerRegion(name);
            var agentRegion = new ControlAgent(simKernel.DiscreteSimController, managerRegion);
            return agentRegion;
        }

        private static ControlAgent GetAgentSurroundings(ISimulationContext simKernel)
        {
            var managerSurroundings = new ManagerSurroundings(ComponentNameManager.AgentSurroundings);
            var agentSurroundings = new ControlAgent(simKernel.DiscreteSimController, managerSurroundings);
            return agentSurroundings;
        }

        private static ControlAgent GetAgentModel(ISimulationContext simKernel)
        {
            var managerModel = new ManagerSurroundings(ComponentNameManager.AgentSurroundings);
            var agentModel = new ControlAgent(simKernel.DiscreteSimController, managerModel);
            return agentModel;
        }
    }
}
