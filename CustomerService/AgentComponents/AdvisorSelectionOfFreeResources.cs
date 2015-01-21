using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class AdvisorSelectionOfFreeResources : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public AdvisorSelectionOfFreeResources(string componentName, ServiceSystemModel model) : base(componentName)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            var applicant = (Customer)message.DataParameters[ParameterNameManager.Applicant];
            var zdroj = _model.VyberVolnehoZdroje(applicant);
            message.Result = zdroj != null ? ResultNameManager.AssignResource : ResultNameManager.AssignNotResource;
        }
    }
}
