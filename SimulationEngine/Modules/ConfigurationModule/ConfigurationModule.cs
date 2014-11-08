using System;
using System.Collections.Generic;
using System.Linq;
using SimulationEngine.Components;
using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public class ConfigurationModule : IAttachedModule
    {
        public ISimulationControl Control { get; set; }

        public IList<ControlAgent> Agents { get; private set; }

        public ConfigurationModule()
        {
            Agents = new List<ControlAgent>();
        }

        public void RegistrationControlAgent(ControlAgent agent)
        {
            if (!Agents.Contains(agent))
            {
                Agents.Add(agent);
            }
            else
            {
                //VYHOZENI VYJIMKY, ZE DANY AGENT UZ TAM JE 
                throw new Exception("Agent " + agent.Manager.Name + "is already registred.");
            }
            //DODELAT ABY VLOZIL I MODEL POKUD NENI REGISTROVAN
        }

        public ControlAgent CancellationControlAgent(ControlAgent agent)
        {
            return Agents.Remove(agent) ? agent : null;
        }

        public bool HaveComponentsMessages()
        {
            //zjisteni jestli u agentu jsou nejake zpravy
            return Agents.Any(agent => agent.MessageCount > 0);
        }

        public IComponent FindAddressee(string componentName)
        {
            //zjisteni jestli se nejedna o komponentu agenta
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
