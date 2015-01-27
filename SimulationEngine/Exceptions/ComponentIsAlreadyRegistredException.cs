using System;

namespace SimulationEngine.Exceptions
{
    class ComponentIsAlreadyRegistredException : Exception
    {
        public ComponentIsAlreadyRegistredException(string componentName, string agentName)
            : base("Component " + componentName + "is already registred in agent " + agentName + ".")
        {
            
        }
    }
}
