using System;
using System.Collections.Generic;
using System.Linq;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public class SimulationModel
    {
        public string Name { get; set; }
        public IList<ControlAgent> Agents { get; private set; }
        public ModelStateSpace StateSpace { get; set; }


        public SimulationModel()
        {
            Agents = new List<ControlAgent>();
        }

        public SimulationModel(string name) : this()
        {
            Name = name;
        }

        public void RegistrationControlAgent(ControlAgent agent)
        {
            if(Agents.Contains(agent))
                throw new Exception("Agent " + agent.Manager.Name + "is already registred.");
            
            Agents.Add(agent);
        }

        public ControlAgent CancellationControlAgent(ControlAgent agent)
        {
            return Agents.Remove(agent) ? agent : null;
        }

        public bool HaveComponentsMessages()
        {
            return Agents.Any(agent => agent.MessageCount > 0);
        }

        public IComponent FindAddressee(string componentName)
        {
            foreach (var agent in Agents)
            {
                IComponent component;
                if ((component = agent.GetComponent(componentName)) != null)
                {
                    return component;
                }
            }
            return null;
        }
    }
}
