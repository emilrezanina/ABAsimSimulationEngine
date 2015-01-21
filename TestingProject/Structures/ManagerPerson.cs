using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace TestingProject.Structures
{
    class ManagerPerson : DynamicManager
    {
        public ManagerPerson(string componentName) : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}
