using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    internal class ManagerSurroundings : ControlManager
    {
        public ManagerSurroundings(string componentName)
            : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            switch (message.Type)
            {
                case TypeMessage.Notice:
                    ProcessNoticeMessage(message);
                    break;
                case TypeMessage.Finish:

                    break;
            }
        }

        private void ProcessNoticeMessage(Message message)
        {
            Message msg;
            switch (message.Code)
            {
                case MessageCodeManager.StartSimulation:
                    msg = new Message(TypeMessage.Start, Name, ComponentNameManager.ProcessEnteringCustomer, MessageCodeManager.OpenGate, null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.EndTime, 5);
                    SendStartMessage(msg);
                    break;
                case MessageCodeManager.IncomingCustomer:
                    msg = new Message(TypeMessage.Notice, Name, ComponentNameManager.AgentService, MessageCodeManager.WaitingNewCustomer, null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Customer, message.DataParameters[ParameterNameManager.Customer]);
                    SendNoticeMessage(msg);
                    break;
                case MessageCodeManager.OutgoingCustomer:
                    break;
            }
        }
    }
}