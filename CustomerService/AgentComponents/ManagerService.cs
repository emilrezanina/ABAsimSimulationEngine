using System;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class ManagerService : ControlManager
    {
        public ManagerService(string componentName)
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
                    ProcessFinishMessage(message);
                    break;
                case TypeMessage.Response:
                    ProcessResponseMessage(message);
                    break;
                default:
                    throw new Exception(message.ToString());
            }
        }

        private void ProcessResponseMessage(Message message)
        {
            //zjisteni o jaky druh obsluhy se ted jedna
            var msg = new Message(TypeMessage.Execute, Name, ComponentNameManager.QueryChoosingServiceType, null, null, message.Timestamp);
            msg.AddDataParameter(ParameterNameManager.Resource, message.DataParameters[ParameterNameManager.Resource]);
            SendExecuteMessage(msg);
            msg = msg.Result.Equals(ResultNameManager.ServiceA)
                ? new Message(TypeMessage.Start, Name, ComponentNameManager.ProcessServiceA, null, message.DataParameters, message.Timestamp)
                : new Message(TypeMessage.Start, Name, ComponentNameManager.ProcessServiceB, null, message.DataParameters, message.Timestamp);
            SendStartMessage(msg);
        }

        private void ProcessFinishMessage(Message message)
        {
            Message msg;

            
            switch (message.Code)
            {
                case MessageCodeManager.CompleteMoveCustomerToServiceA:
                    //vraceni zdroje z obsluhy A
                    msg = new Message(TypeMessage.Request, Name,
                        ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.DeliverResource, message.DataParameters, message.Timestamp);
                    SendRequestMessage(msg);
                    break;
                case MessageCodeManager.CompleteServiceA:
                    msg = new Message(TypeMessage.Notice, Name, ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.ReturnResource,
                        null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Resource, message.DataParameters[ParameterNameManager.Resource]);
                    SendNoticeMessage(msg);
                    //dodelani zadost o obsluhu B 
                    msg = new Message(TypeMessage.Request, Name, ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.DeliverResource,
                        null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Applicant, message.DataParameters[ParameterNameManager.Applicant]);
                    SendRequestMessage(msg);
                    break;
                case MessageCodeManager.CompleteServiceB:
                    //vraceni zdroje z obsluhy B
                    msg = new Message(TypeMessage.Notice, Name, ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.ReturnResource,
                        null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Resource, message.DataParameters[ParameterNameManager.Resource]);
                    SendNoticeMessage(msg);
                    //vyrazeni zakaznika z obsluhy
                    msg = new Message(TypeMessage.Start, Name, ComponentNameManager.ProcessCustomerOutgoing , null, null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Applicant, message.DataParameters[ParameterNameManager.Applicant]);
                    SendExecuteMessage(msg);
                    break;
                case MessageCodeManager.OutgoingCustomer:
                    msg = new Message(TypeMessage.Notice, Name, ComponentNameManager.AgentSurroundings, MessageCodeManager.OutgoingCustomer,
                        message.DataParameters, message.Timestamp);
                    SendNoticeMessage(msg);
                    break;
            }
        }

        private void ProcessNoticeMessage(Message message)
        {
            switch (message.Code)
            {
                case MessageCodeManager.WaitingNewCustomer:
                    var msg = new Message(TypeMessage.Start, Name, ComponentNameManager.ProcessMoveCustomer,
                        null, null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Applicant, message.DataParameters[ParameterNameManager.Customer]);
                    SendStartMessage(msg);
                    break;
            }
        }
    }
}
