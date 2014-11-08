using System;
using SimulationEngine.Communication;
using SimulationEngine.Modules.ConfigurationModule;

namespace SimulationEngine.Components
{
    public abstract class AbstractComponent : IComponent
    {
        public IAgent ControlAgent { get; set; }
        public string Name { get; set; }

        protected AbstractComponent(string componentName)
        {
            ControlAgent = null;
            Name = componentName;
        }

        public abstract void ProcessTheMessage(Message message);

        protected bool Equals(AbstractComponent other)
        {
            return Equals(ControlAgent, other.ControlAgent) && Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ControlAgent != null ? ControlAgent.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public override String ToString()
        {
            return Name;
        }

        
    }
}
