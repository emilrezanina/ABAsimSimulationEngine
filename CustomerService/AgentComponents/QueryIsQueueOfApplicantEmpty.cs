using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class QueryIsQueueOfApplicantEmpty : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public QueryIsQueueOfApplicantEmpty(string name, ServiceSystemModel model) : base(name)
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
