using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngineTests.Structures
{
    internal class AgentBManager : ControlManager
    {
        public AgentBManager(string name) : base(name)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}