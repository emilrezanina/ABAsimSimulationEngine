﻿using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ActionAssignResource : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public ActionAssignResource(string name, ServiceSystemModel model)
            : base(name)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            var resource = _model.PridelZdrojZakaznikovi((Customer)message.DataParameters[ParameterNameManager.Applicant]);
            message.AddDataParameter(ParameterNameManager.Resource, resource);
            message.Result = ResultNameManager.ResourceAssigned;
        }
    }
}
