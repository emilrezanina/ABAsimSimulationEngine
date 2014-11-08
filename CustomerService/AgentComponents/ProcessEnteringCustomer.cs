using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace CustomerService.AgentComponents
{
    class ProcessEnteringCustomer : ContinuousAssistant
    {
        private int _endTime;
        private readonly ServiceSystemModel _model;

        public ProcessEnteringCustomer(string componentName, IReciveSendMessage holdTarget,
            ServiceSystemModel model)
            : base(componentName, holdTarget)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msgHold;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    _endTime = (int)message.DataParameters[ParameterNameManager.EndTime];
                    msgHold = new Message(TypeMessage.Hold, Name, Name, null, null, message.Timestamp + 1); //endtime tady nema byt
                    SendHoldMessage(msgHold);
                    break;
                case TypeMessage.Hold:
                    //pouze pokud proces porad jede a cas ukonceni nebyl dosazen
                    if (message.Timestamp <= _endTime)
                    {
                        var nextArrivalTime = message.Timestamp + 1;
                        Message msgManager;
                        if (nextArrivalTime == _endTime)
                        {
                            msgManager = new Message(TypeMessage.Finish, Name, ControlAgent.Manager.Name, null);
                            SendFinishMessage(msgManager);
                        }
                        msgHold = new Message(TypeMessage.Hold, Name, Name, null, null, nextArrivalTime);
                        SendHoldMessage(msgHold);
                        object customer = _model.VygenerujZakaznika();
                        msgManager = new Message(TypeMessage.Notice, Name, ControlAgent.Manager.Name, MessageCodeManager.IncomingCustomer);
                        msgManager.AddDataParameter(ParameterNameManager.Customer, customer);
                        SendNoticeMessage(msgManager);
                    }
                    break;
            }
        }
    }
}
