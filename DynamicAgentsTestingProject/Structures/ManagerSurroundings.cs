using System;
using DynamicAgentsTestingProject.NamesManagers;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace DynamicAgentsTestingProject.Structures
{
    class ManagerSurroundings : ControlManager
    {
        public ManagerSurroundings(string componentName) : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            switch (message.Type)
            {
                case TypeMessage.Notice:
                    switch (message.Code)
                    {
                        case MessageCodeNameManager.OpenGate:
                            var msg = new Message(TypeMessage.Start, Name, this.Name, null, null, message.Timestamp);
                            break;
                        default:
                            throw new Exception("Message (Type: " + message.Type + ", Code: " + message.Code + ") dont have handler.");
                    }
                    break;
                default:
                    throw new Exception("Message type " + message.Type + " dont have handler.");
            }    
        }
    }
}
