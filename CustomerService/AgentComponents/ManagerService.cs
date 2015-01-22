using System;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ManagerService : ControlManager
    {
        public ManagerService(string name)
            : base(name)
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
            var msg = MessageProvider.CreateMessage(TypeMessage.Execute, Name, ComponentNameManager.QueryChoosingServiceType, null, null, message.Timestamp);
            msg.AddDataParameter(ParameterNameManager.Resource, message.DataParameters[ParameterNameManager.Resource]);
            SendExecuteMessage(msg);
            msg = msg.Result.Equals(ResultNameManager.ServiceA)
                ? MessageProvider.CreateMessage(TypeMessage.Start, Name, ComponentNameManager.ProcessServiceA, MessageCodeManager.StartServiceA, message.DataParameters, message.Timestamp)
                : MessageProvider.CreateMessage(TypeMessage.Start, Name, ComponentNameManager.ProcessServiceB, MessageCodeManager.StartServiceB, message.DataParameters, message.Timestamp);
            SendStartMessage(msg);
        }

        private void ProcessFinishMessage(Message message)
        {
            Message msg;

            
            switch (message.Code)
            {
                case MessageCodeManager.CompleteMoveCustomerToServiceA:
                    
                    msg = MessageProvider.CreateMessage(TypeMessage.Request, Name,
                        ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.DeliverResource, message.DataParameters, message.Timestamp);
                    SendRequestMessage(msg);
                    break;
                    //vraceni zdroje z obsluhy A
                case MessageCodeManager.CompleteServiceA:
                    msg = MessageProvider.CreateMessage(TypeMessage.Notice, Name, ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.ReturnResource,
                        null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Resource, message.DataParameters[ParameterNameManager.Resource]);
                    SendNoticeMessage(msg);
                    //dodelani zadost o obsluhu B 
                    msg = MessageProvider.CreateMessage(TypeMessage.Request, Name, ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.DeliverResource,
                        null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Applicant, message.DataParameters[ParameterNameManager.Applicant]);
                    SendRequestMessage(msg);
                    break;
                case MessageCodeManager.CompleteServiceB:
                    //vraceni zdroje z obsluhy B
                    msg = MessageProvider.CreateMessage(TypeMessage.Notice, Name, ComponentNameManager.AgentResourceAdministrator, MessageCodeManager.ReturnResource,
                        null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Resource, message.DataParameters[ParameterNameManager.Resource]);
                    SendNoticeMessage(msg);
                    //vyrazeni zakaznika z obsluhy
                    msg = MessageProvider.CreateMessage(TypeMessage.Start, Name, ComponentNameManager.ProcessCustomerOutgoing, MessageCodeManager.MovingOutgoingCustomer, null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Applicant, message.DataParameters[ParameterNameManager.Applicant]);
                    SendExecuteMessage(msg);
                    break;
                case MessageCodeManager.OutgoingCustomer:
                    msg = MessageProvider.CreateMessage(TypeMessage.Notice, Name, ComponentNameManager.AgentSurroundings, MessageCodeManager.OutgoingCustomer,
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
                    var msg = MessageProvider.CreateMessage(TypeMessage.Start, Name, ComponentNameManager.ProcessMoveCustomer,
                        MessageCodeManager.MovingCustomer, null, message.Timestamp);
                    msg.AddDataParameter(ParameterNameManager.Applicant, message.DataParameters[ParameterNameManager.Customer]);
                    SendStartMessage(msg);
                    break;
            }
        }
    }
}
