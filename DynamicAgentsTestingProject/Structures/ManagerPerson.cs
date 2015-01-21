using System;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace DynamicAgentsTestingProject.Structures
{
    class ManagerPerson : DynamicManager
    {
        public ManagerPerson(string componentName) : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
