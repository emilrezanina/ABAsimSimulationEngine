using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class QueryNeedMoveResource : AbstractComponent
    {
        public QueryNeedMoveResource(string componentName) : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            message.Result = ((ServiceResourse)message.DataParameters[ParameterNameManager.Resource]).Type 
                == ServiceResourse.EServiceResourseType.A ? MessageCodeManager.MoveResource 
                : MessageCodeManager.MoveNotResource;
        }
    }
}
