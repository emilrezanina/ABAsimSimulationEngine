using System;
using SimulationEngine.Communication;

namespace SimulationEngine.Exceptions
{
    public class MessagePrototypeIsRegistredException : Exception
    {
        public MessagePrototypeIsRegistredException(Message message) : base("Message { type: "
            + message.Type + "; code: " + message.Code + "} is already registred.")
        {
        }
    }
}
