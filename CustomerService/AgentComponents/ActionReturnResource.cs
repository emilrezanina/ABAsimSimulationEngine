using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ActionReturnResource : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public ActionReturnResource(string componentName, ServiceSystemModel model) : base(componentName)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            var resource = (ServiceResourse) message.DataParameters[ParameterNameManager.Resource];
            _model.UvolneniZdroje(resource);
            message.Result = ResultNameManager.ResourceReturned;
        }
    }
}
