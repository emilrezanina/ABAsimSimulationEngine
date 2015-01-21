using System;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace DynamicAgentsTestingProject.Structures
{
    class ManagerRegion : ControlManager
    {
        public ManagerRegion(string componentName)
            : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            switch (message.Type)
            {
                case TypeMessage.Entrust:
                    break;
                default:
                    throw new Exception("Message type " + message.Type + " dont have handler.");
            }
        }
    }
}
