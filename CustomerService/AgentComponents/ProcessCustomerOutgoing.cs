using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ProcessCustomerOutgoing : ContinuousAssistant
    {
        private readonly ServiceSystemModel _model;
        public ProcessCustomerOutgoing(string componentName, IReciveSendMessage holdTarget, ServiceSystemModel model) : base(componentName, holdTarget)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msg;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    msg = MessageProvider.CreateMessage(TypeMessage.Hold, Name, Name,
                            null, message.DataParameters, message.Timestamp + 1);
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    var zakaznik = (Customer)message.DataParameters[ParameterNameManager.Applicant];
                    _model.PremisteniZakaznikaZObsluhy(zakaznik);
                    msg = MessageProvider.CreateMessage(TypeMessage.Finish, Name, ControlAgent.Manager.Name,
                            MessageCodeManager.OutgoingCustomer, message.DataParameters, message.Timestamp);
                    SendFinishMessage(msg);
                    break;
            }
        }
    }
}
