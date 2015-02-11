using System;
using System.Collections.Generic;
using System.Linq;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;

namespace SimulationEngine.Verification
{
    class SimulationModelVerificator
    {
        private readonly List<string> _errorMessages;
        public SimulationModel Model { get; private set; }

        public SimulationModelVerificator(SimulationModel model)
        {
            _errorMessages = new List<string>();
            Model = model;
        }

        bool InterfaceVerification()
        {
            return Model.Agents.Aggregate(true, (current, controlAgent) 
                => current && ControlAgentInterfaceCheck(controlAgent));
        }

        bool ControlAgentInterfaceCheck(ControlAgent agent)
        {
            var withoutProblem = true;
            foreach (var outgoingPrototypeMsg in agent.OutgoingMessageRegister.GetMessages())
            {
                var findReceiveHandlerForMessagePrototype = HasOutputMessageHasReceiveHandler(outgoingPrototypeMsg);
                if (findReceiveHandlerForMessagePrototype)
                {
                    _errorMessages.Add("");
                }
                withoutProblem = withoutProblem && findReceiveHandlerForMessagePrototype;
            }
            return withoutProblem;
            
        }

        bool HasOutputMessageHasReceiveHandler(Message outgoingPrototypeMsg)
        {
            var addressee = (AbstractAgent) Model.FindAddressee(outgoingPrototypeMsg.Addressee);
            if (addressee == null)
                return false;

            var register = addressee.IncomingMessageRegister;
            var incomingPrototypeMsg = register.GetPrototypeMessage(outgoingPrototypeMsg.Type, outgoingPrototypeMsg.Code);
            return incomingPrototypeMsg != null && 
                incomingPrototypeMsg.HasSameDataParameters(outgoingPrototypeMsg);
        }
    }
}
