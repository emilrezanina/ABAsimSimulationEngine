using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var tasks = new List<Task>();
            foreach (var simulationContext in SimulationContexts)
            {
                var runTask = new Task(simulationContext.Run);
                tasks.Add(runTask);
                runTask.ContinueWith(t => { Console.WriteLine("Task " + t.Id + " is complete."); });
                runTask.Start();
            }
            Task.WaitAll(tasks.ToArray());
        }

        public bool Verification(SimulationModel model)
        {
            var verificator = new SimulationModelVerificator(model);
            return verificator.InterfaceVerification() && verificator.IsEveryAgentAttachedToSimulationModel();
        }
    }
}
