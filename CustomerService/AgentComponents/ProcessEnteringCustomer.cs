using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ProcessEnteringCustomer : ContinuousAssistant
    {
        private int _endTime;
        private readonly ServiceSystemModel _model;

        public ProcessEnteringCustomer(string name, IReciveSendMessage holdTarget,
            ServiceSystemModel model)
            : base(name, holdTarget)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msgHold;
            object customer;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    _endTime = (int)message.DataParameters[ParameterNameManager.EndTime];
                    customer = _model.VygenerujZakaznika();
                    msgHold = MessageProvider.CreateMessage(TypeMessage.Hold, Name, Name, null, null, message.Timestamp + 1);
                    msgHold.AddDataParameter(ParameterNameManager.Customer, customer);
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
                            msgManager = MessageProvider.CreateMessage(TypeMessage.Finish, Name, ControlAgent.Manager.Name, null, null,
                                message.Timestamp);
                            SendFinishMessage(msgManager);
                        }
                        customer = _model.VygenerujZakaznika();
                        msgHold = MessageProvider.CreateMessage(TypeMessage.Hold, Name, Name, null, null, nextArrivalTime);
                        msgHold.AddDataParameter(ParameterNameManager.Customer, customer);
                        SendHoldMessage(msgHold);

                        msgManager = MessageProvider.CreateMessage(TypeMessage.Notice, Name, ControlAgent.Manager.Name, 
                            MessageCodeManager.IncomingCustomer, message.DataParameters, message.Timestamp);
                        SendNoticeMessage(msgManager);
                    }
                    break;
            }
        }
    }
}
