using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class QueryNeedMoveResource : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public QueryNeedMoveResource(string componentName, ServiceSystemModel model) : base(componentName)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            message.Result = ((ServiceResourse)message.DataParameters[ParameterNameManager.Resource]).Type 
                == ServiceResourse.EServiceResourseType.A ? MessageCodeManager.MoveResource 
                : MessageCodeManager.MoveNotResource;
        }
    }
}
