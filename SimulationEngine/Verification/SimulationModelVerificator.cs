using System.Collections.Generic;
using System.Linq;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;

namespace SimulationEngine.Verification
{
    public class SimulationModelVerificator
    {
        private readonly List<string> _errorMessages;
        public SimulationModel Model { get; private set; }

        public SimulationModelVerificator(SimulationModel model)
        {
            _errorMessages = new List<string>();
            Model = model;
        }

        public bool InterfaceVerification()
        {
            return Model.Agents.Aggregate(true, (current, controlAgent) 
                => current && ControlAgentInterfaceCheck(controlAgent));
        }

        private bool ControlAgentInterfaceCheck(ControlAgent agent)
        {
            var withoutProblem = true;
            foreach (var outgoingPrototypeMsg in agent.OutgoingMessageRegister.GetMessages())
            {
                var tryFindReceiveHandlerForMessagePrototype = HasOutputMessageHasReceiveHandler(outgoingPrototypeMsg);
                if (!tryFindReceiveHandlerForMessagePrototype)
                {
                    _errorMessages.Add("");
                }
                withoutProblem = withoutProblem && tryFindReceiveHandlerForMessagePrototype;
            }
            return withoutProblem;
            
        }

        private bool HasOutputMessageHasReceiveHandler(Message outgoingPrototypeMsg)
        {
            var addressee = Model.FindAddressee(outgoingPrototypeMsg.Addressee).ControlAgent;
            if (addressee == null)
                return false;

            var register = addressee.IncomingMessageRegister;
            var incomingPrototypeMsg = register.GetPrototypeMessage(outgoingPrototypeMsg.Type, outgoingPrototypeMsg.Code);
            return incomingPrototypeMsg != null && 
                incomingPrototypeMsg.HasSameDataParameters(outgoingPrototypeMsg);
        }

        public IEnumerable<string> GetErrorMessages()
        {
            return _errorMessages;
        }
    }
}
