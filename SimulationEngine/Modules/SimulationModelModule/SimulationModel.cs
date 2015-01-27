using System.Collections.Generic;
using System.Linq;
using SimulationEngine.Communication;
using SimulationEngine.Exceptions;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public class SimulationModel
    {
        private readonly IList<Message> _unprocessedInterAgentMessages;
 
        public string Name { get; set; }
        public IList<ControlAgent> Agents { get; private set; }
        public ModelStateSpace StateSpace { get; set; }


        public SimulationModel()
        {
            Agents = new List<ControlAgent>();
            _unprocessedInterAgentMessages =new List<Message>();
        }

        public SimulationModel(string name) : this()
        {
            Name = name;
        }

        public void RegistrationControlAgent(ControlAgent agent)
        {
            if(Agents.Contains(agent))
                throw new AgentIsAlreadyRegistredException(agent.Manager.Name);
            
            Agents.Add(agent);
        }

        public ControlAgent CancellationControlAgent(ControlAgent agent)
        {
            return Agents.Remove(agent) ? agent : null;
        }

        public bool IsEmpty()
        {
            return !_unprocessedInterAgentMessages.Any();
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

         private IAgent FindAddresseeAgent(string addressee)
         {
             //TODO: Problem with missing searching in dynamic agents
             return Agents.FirstOrDefault(controlAgent => controlAgent.Manager.Name.Equals(addressee));
         }

        public void ReceiveMessage(Message message, bool immediatelyProcess)
        {
            if (immediatelyProcess)
            {
                var addressee = FindAddressee(message.Addressee);
                if (addressee == null)
                    throw new AddresseeNotFoundException(message.Addressee);

                addressee.ProcessTheMessage(message);
            }
            else
                _unprocessedInterAgentMessages.Add(message);
        }

        public void ProcessAllInterAgentMessages()
        {
            while (!IsEmpty())
            {
                var message = _unprocessedInterAgentMessages.First();
                _unprocessedInterAgentMessages.Remove(message);
                var addresseeAgent = FindAddresseeAgent(message.Addressee);
                if (addresseeAgent == null)
                    throw new AddresseeNotFoundException(message.Addressee);

                addresseeAgent.ReciveMessage(message);
            }
            
        }

       
    }
}
