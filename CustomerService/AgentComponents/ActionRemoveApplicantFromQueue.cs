using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace CustomerService.AgentComponents
{
    class ActionRemoveApplicantFromQueue : AbstractComponent
    {
        private readonly ServiceSystemModel _model;

        public ActionRemoveApplicantFromQueue(string componentName, ServiceSystemModel model)
            : base(componentName)
        {
            _model = model;
        }

        public override void ProcessTheMessage(Message message)
        {
            //TADY MUSI BYT RECENO JESTLI SE TYKA KOUKNUTI DO FRONTY NA OBSLUHU A NEBO DO FRONTY NA OBSLUHU B
            var serviceResourse = message.DataParameters[ParameterNameManager.Resource] as ServiceResourse;
            if (serviceResourse != null)
            {
                var typeResource = serviceResourse.Type;
                var applicant = _model.VratZakaznikaCekajicihoVeFronte(typeResource);
                message.AddDataParameter(ParameterNameManager.Applicant, applicant);
            }
            message.Result = ResultNameManager.AssignResource;
        }
    }
}
