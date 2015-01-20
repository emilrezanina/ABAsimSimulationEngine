using System.Collections.Generic;
using SimulationEngine.SimulationKernel;

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
    }
}
