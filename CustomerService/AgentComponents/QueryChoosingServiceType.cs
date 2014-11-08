using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class QueryChoosingServiceType : AbstractComponent
    {

        public QueryChoosingServiceType(string componentName) : base(componentName)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            var resource = (ServiceResourse)message.DataParameters[ParameterNameManager.Resource];
            message.Result = resource.Type == ServiceResourse.EServiceResourseType.A 
                ? ResultNameManager.ServiceA : ResultNameManager.ServiceB;
        }
    }
}
