using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ActionPutApplicantToQueueOnResource : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public ActionPutApplicantToQueueOnResource(string name, ServiceSystemModel model) : base(name)
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
