using System;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace TestingProject.Structures
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
                    Message msg;
                    switch (message.Code)
                    {
                        case "Begin generation":
                            msg = new Message(TypeMessage.Start,
                                    Name,
                                    "pGeneratorPersons",
                                    message.Code,
                                    null,
                                    message.Timestamp);
                            SendStartMessage(msg);
                            break;
                        case "End generation":
                            msg = new Message(TypeMessage.Break,
                                    Name,
                                    "pGeneratorPersons",
                                    message.Code,
                                    null,
                                    message.Timestamp);
                            SendBreakMessage(msg);
                            break;
                        case "New arrival person":
                            //1. Pridat noveho dynamickeho agenta do modelu agenta modelu
                            msg = new Message(TypeMessage.Handover,
                                    Name,
                                    "mModel",
                                    "New person",
                                    null,
                                    message.Timestamp) {DynamicAgent = message.DynamicAgent};
                            SendHandoverMessage(msg);
                            break;
                        default:
                            throw new Exception("Notice message with " + message.Code + "has not handler.");
                    }
                    break;
                default:
                    throw new Exception(message.Type + " message has not handler.");
            }
        }
    }
}
