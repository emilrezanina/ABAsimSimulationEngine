using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace CustomerService.AgentComponents
{
    class ProcessMoveCustomer : ContinuousAssistant
    {
        private readonly ServiceSystemModel _model;

        public ProcessMoveCustomer(string componentName, IReciveSendMessage holdTarget, ServiceSystemModel model)
            : base(componentName, holdTarget)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msg;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    msg = MessageProvider.CreateMessage(TypeMessage.Hold, Name,
                        Name, null, message.DataParameters, message.Timestamp + 1);
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    var applicant = (Customer)message.DataParameters[ParameterNameManager.Applicant];
                    _model.PremisteniZakaznikaNaObsluhu(applicant);
                    msg = MessageProvider.CreateMessage(TypeMessage.Finish, Name,
                            ControlAgent.Manager.Name,
                            MessageCodeManager.CompleteMoveCustomerToServiceA, message.DataParameters, message.Timestamp);
                    SendFinishMessage(msg);
                    break;
            }
        }
    }
}
