using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngineTests.Structures
{
    class DynamicAgentManager : DynamicManager
    {
        public DynamicAgentManager(string name) : base(name)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            
        }
    }
}
