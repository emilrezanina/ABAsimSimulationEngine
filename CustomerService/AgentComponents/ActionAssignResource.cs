using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class ActionAssignResource : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public ActionAssignResource(string componentName, ServiceSystemModel model)
            : base(componentName)
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
