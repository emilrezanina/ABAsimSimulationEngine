using System;

namespace SimulationEngine.Exceptions
{
    public class AddresseeNotFoundException : Exception
    {
        public AddresseeNotFoundException(string addressee)
            : base("Adressee " + addressee + "not found.")
        {
            
        }
    }
}
