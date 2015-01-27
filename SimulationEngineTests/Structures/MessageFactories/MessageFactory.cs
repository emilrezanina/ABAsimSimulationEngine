using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngineTests.Structures.IdentficatorSets;

namespace SimulationEngineTests.Structures.MessageFactories
{
    abstract class MessageFactory
    {
        public static Message CreateHandoverMessage(string addressee, DynamicAgent agent)
        {
            var msg = new Message(TypeMessage.Handover, null, addressee, CodeMessages.TransferDynamicAgent, null, 0)
            {
                DynamicAgent = agent
            };
            return msg;
        }
    }
}
