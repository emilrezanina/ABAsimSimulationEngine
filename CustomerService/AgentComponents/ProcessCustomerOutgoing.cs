using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

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
                    msg = new Message(TypeMessage.Hold, Name, Name,
                            null, message.DataParameters, message.Timestamp + 1);
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    var zakaznik = (Customer)message.DataParameters[ParameterNameManager.Applicant];
                    _model.PremisteniZakaznikaZObsluhy(zakaznik);
                    msg = new Message(TypeMessage.Finish, Name, ControlAgent.Manager.Name,
                            MessageCodeManager.OutgoingCustomer, message.DataParameters, message.Timestamp);
                    SendFinishMessage(msg);
                    break;
            }
        }
    }
}
