﻿using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.DiscreteSimulationModule;

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
                    msg = new Message(TypeMessage.Hold, Name, Name, null, 
                        message.DataParameters, message.Timestamp + 2);
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    _model.DokonciObsluhu((Customer)message.DataParameters[ParameterNameManager.Applicant],
                            (ServiceResourse)message.DataParameters[ParameterNameManager.Resource]);
                    msg = new Message(TypeMessage.Finish, Name,
                            ControlAgent.Manager.Name,
                            MessageCodeManager.CompleteServiceA, message.DataParameters, message.Timestamp);
                    SendFinishMessage(msg);
                    break;
            }
        }
    }
}