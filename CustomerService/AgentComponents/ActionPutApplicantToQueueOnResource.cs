using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class ActionPutApplicantToQueueOnResource : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public ActionPutApplicantToQueueOnResource(string componentName, ServiceSystemModel model) : base(componentName)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            _model.PremistitZakaznikaDoFrontyNaObsluhu((Customer)message.DataParameters[ParameterNameManager.Applicant]);
            message.Result = ResultNameManager.ApplicantInQueue;
        }
    }
}
