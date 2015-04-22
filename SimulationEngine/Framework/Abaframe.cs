using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulationKernel;
using SimulationEngine.Verification;

namespace SimulationEngine.Framework
{
    class Abaframe
    {
        public bool Verification(SimulationModel model)
        {
            var verificator = new SimulationModelVerificator(model);
            return verificator.InterfaceVerification() && verificator.IsEveryAgentAttachedToSimulationModel();
        }
        public void Run(SimulationModel model)
        {
            var simContext = new SimulationContext {SimModel = model};
            simContext.Run();
        }

    }
}
