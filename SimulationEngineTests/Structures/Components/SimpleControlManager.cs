using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngineTests.Structures.Components
{
    class SimpleControlManager : ControlManager
    {
        public SimpleControlManager(string name) : base(name)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            
        }
    }
}
