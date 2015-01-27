using System;

namespace SimulationEngine.Exceptions
{
    class AgentIsAlreadyRegistredException : Exception
    {
        public AgentIsAlreadyRegistredException(string agentName)
            : base("Agent " + agentName + "is already registred.")
        {
            
        }
    }
}
