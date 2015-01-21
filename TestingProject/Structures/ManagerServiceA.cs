using System;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace TestingProject.Structures
{
    class ManagerServiceA : ControlManager
    {
        public ManagerServiceA(string componentName) : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            switch (message.Type)
            {
                case TypeMessage.Entrust:

                    break;
                case TypeMessage.Return:

                    break;
                default:
                    throw new Exception(message.Type + " message has not handler.");
            }
        }
    }
}
