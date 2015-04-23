using System.Collections.Generic;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulationKernel;
using SimulationEngine.Verification;

namespace SimulationEngine
{
    public class SimulationFramework
    {
        public List<SimulationContext> SimulationContexts { get; private set; }

        public SimulationFramework()
        {
            SimulationContexts = new List<SimulationContext>();
        }

        public void RunAllSimulationContexts()
        {
            foreach (var simulationContext in SimulationContexts)
            {
                simulationContext.Run();
            }
        }

        public bool Verification(SimulationModel model)
        {
            var verificator = new SimulationModelVerificator(model);
            return verificator.InterfaceVerification() && verificator.IsEveryAgentAttachedToSimulationModel();
        }
    }
}
