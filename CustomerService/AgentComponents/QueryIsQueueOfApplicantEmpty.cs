using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class QueryIsQueueOfApplicantEmpty : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public QueryIsQueueOfApplicantEmpty(string componentName, ServiceSystemModel model) : base(componentName)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            var resource = (ServiceResourse) message.DataParameters[ParameterNameManager.Resource];
            message.Result = _model.JeFrontaNaObsluhuPrazdna(resource) ? ResultNameManager.QueueIsEmpty : ResultNameManager.QueueIsntEmpty;
        }
    }
}
