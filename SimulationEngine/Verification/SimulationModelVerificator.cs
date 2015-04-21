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

        private bool ControlAgentInterfaceCheck(IAgent agent)
        {
            bool withoutProblem = true;
            foreach (var outgoingPrototypeMsg in agent.OutgoingMessageRegister.GetMessages())
            {
                withoutProblem = withoutProblem && HasOutputMessageHasReceiveHandler(outgoingPrototypeMsg);
            }
            return withoutProblem;
            
        }

        private bool HasOutputMessageHasReceiveHandler(Message outgoingPrototypeMsg)
        {
            var addressee = Model.FindAddressee(outgoingPrototypeMsg.Addressee).ControlAgent;
            if (addressee == null)
            {
                _errorMessages.Add(ErrorStringManager.AddresseeWasntFound(outgoingPrototypeMsg));
                return false;
            }
            var register = addressee.IncomingMessageRegister;
            var incomingPrototypeMsg = register.GetPrototypeMessage(outgoingPrototypeMsg.Type, outgoingPrototypeMsg.Code);
            if (incomingPrototypeMsg == null)
            {
                _errorMessages.Add(ErrorStringManager.AddreseeDoesntHavePrototype(addressee, outgoingPrototypeMsg));
                return false;
            }
            if (incomingPrototypeMsg.HasSameDataParameters(outgoingPrototypeMsg)) return true;
            
            _errorMessages.Add(ErrorStringManager.PrototypesDontHaveSameDataParameters(outgoingPrototypeMsg));
            return false;
        }

        public bool IsEveryAgentAttachedToSimulationModel()
        {
            return Model.Agents.Aggregate(true, (current, controlAgent) => current && HasAgentAnyCommunication(controlAgent));
        }

        private bool HasAgentAnyCommunication(IAgent agent)
        {
            return agent.IncomingMessageRegister.IsEmpty() || agent.OutgoingMessageRegister.IsEmpty();
        }

        public IEnumerable<string> GetErrorMessages()
        {
            return _errorMessages;
        }
    }
}
