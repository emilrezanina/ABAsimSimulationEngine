using System;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace TestingProject.Structures
{
    class ManagerModel : ControlManager
    {
        public ManagerModel(string componentName) : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            switch (message.Type)
            {
                case TypeMessage.Handover:
                    switch (message.Code)
                    {
                        case "New person":
                            var msg = new Message(TypeMessage.Entrust,
                                Name,
                                "mServiceA",
                                "Start service",
                                null,
                                message.Timestamp) {DynamicAgent = message.DynamicAgent};
                            SendEntrustMessage(msg);
                            break;
                        default:
                            throw new Exception("Handover message with " + message.Code + "has not handler.");
                    }
                    break;
                case TypeMessage.Return:
                    break;
                default:
                    throw new Exception(message.Type + " message has not handler.");
            }
        }
    }
}
