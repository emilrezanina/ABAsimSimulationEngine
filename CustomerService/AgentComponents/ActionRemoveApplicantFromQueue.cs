using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class ActionRemoveApplicantFromQueue : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public ActionRemoveApplicantFromQueue(string componentName, ServiceSystemModel model) : base(componentName)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            var applicant = _model.VratZakaznikaCekajicihoVeFronte();
            message.AddDataParameter(ParameterNameManager.Applicant, applicant);
            message.Result = ResultNameManager.AssignResource;
        }
    }
}
