using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;
using SimulationEngineTests.Structures.IdentficatorSets;

namespace SimulationEngineTests.Structures
{
    internal class AgentAManager : ControlManager
    {
        public AgentAManager(string name)
            : base(name)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            switch (message.Type)
            {
                case TypeMessage.Notice:
                    switch (message.Code)
                    {
                        case CodeMessages.BeginTest:
                            var msg = new Message(
                                TypeMessage.Notice, 
                                Name, 
                                ComponentNames.AgentB, 
                                CodeMessages.NewMessage, 
                                null, 
                                message.Timestamp);
                            SendNoticeMessage(msg);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }


    }
}