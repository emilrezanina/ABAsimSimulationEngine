using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;

namespace SimulationEngine.Verification
{
    public static class ErrorStringManager
    {
        public static string AddresseeWasntFound(Message outgoingPrototypeMsg)
        {
            return "Addressee for " + outgoingPrototypeMsg + " wasn't found.";
        }

        public static string AddreseeDoesntHavePrototype(IAgent addressee, Message outgoingPrototypeMsg)
        {
            return "Addressee " + addressee.Manager.Name + " doesn't have prototype message for message {Type: "
                   + outgoingPrototypeMsg.Type + " Code: " + outgoingPrototypeMsg.Code + "}.";
        }

        public static string PrototypesDontHaveSameDataParameters(Message outgoingPrototypeMsg)
        {
            return "Prototype message {Type: " + outgoingPrototypeMsg.Type
                   + " Code: " + outgoingPrototypeMsg.Code + "} doesn't have same data parameters.";
        }
    }
}
