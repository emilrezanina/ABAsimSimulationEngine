using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace CustomerService.AgentComponents
{
    class ProcessMoveResource : ContinuousAssistant
    {
        public ProcessMoveResource(string componentName, IReciveSendMessage holdTarget) : base(componentName, holdTarget)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msg;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    msg = MessageProvider.CreateMessage(TypeMessage.Hold, Name, Name, null, message.DataParameters,
                        message.Timestamp + 1);
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    msg = MessageProvider.CreateMessage(TypeMessage.Finish, Name, ControlAgent.Manager.Name,
                        MessageCodeManager.CompleteMoveResource, message.DataParameters, message.Timestamp);
                    msg.Answer = message.Answer;
                    SendFinishMessage(msg);
                    break;
            }
        }
    }
}
