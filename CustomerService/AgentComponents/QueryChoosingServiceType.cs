using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class QueryChoosingServiceType : AbstractComponent
    {

        public QueryChoosingServiceType(string name) : base(name)
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
