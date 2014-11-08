using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace CustomerService.AgentComponents
{
    class ProcessMoveResource : ContinuousAssistant
    {
        private readonly ServiceSystemModel _model;

        public ProcessMoveResource(string componentName, IReciveSendMessage holdTarget, ServiceSystemModel model) : base(componentName, holdTarget)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msg;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    msg = new Message(TypeMessage.Hold, Name, Name, null, message.DataParameters,
                        message.Timestamp + 1);
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    msg = new Message(TypeMessage.Finish, Name, ControlAgent.Manager.Name,
                            MessageCodeManager.CompleteMoveResource, message.DataParameters, message.Timestamp)
                    {
                        Answer = message.Answer
                    };
                    SendFinishMessage(msg);
                    break;
            }
        }
    }
}
