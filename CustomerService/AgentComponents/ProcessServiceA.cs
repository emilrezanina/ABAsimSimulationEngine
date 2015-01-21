using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ProcessServiceA : ContinuousAssistant
    {
        private readonly ServiceSystemModel _model;

        public ProcessServiceA(string componentName, IReciveSendMessage holdTarget, ServiceSystemModel model) : base(componentName, holdTarget)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msg;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    var applicant = (Customer)message.DataParameters[ParameterNameManager.Applicant];
                    var resource = (ServiceResourse)message.DataParameters[ParameterNameManager.Resource];
                    _model.ZacniObsluhu(applicant, resource);
                    msg = MessageProvider.CreateMessage(TypeMessage.Hold, Name, Name, null, 
                        message.DataParameters, message.Timestamp + 2);
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    _model.DokonciObsluhu((Customer)message.DataParameters[ParameterNameManager.Applicant],
                            (ServiceResourse)message.DataParameters[ParameterNameManager.Resource]);
                    msg = MessageProvider.CreateMessage(TypeMessage.Finish, Name,
                            ControlAgent.Manager.Name,
                            MessageCodeManager.CompleteServiceA, message.DataParameters, message.Timestamp);
                    SendFinishMessage(msg);
                    break;
            }
        }
    }
}
