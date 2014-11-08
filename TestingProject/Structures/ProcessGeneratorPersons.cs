using System;
using CustomerService.AgentComponents;
using SimulationEngine.Communication;
using SimulationEngine.Components;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace TestingProject.Structures
{
    class ProcessGeneratorPersons : ContinuousAssistant
    {
        private bool _activate;
        private readonly Random _random;
        private int _time;
        private int _order;

        public ProcessGeneratorPersons(string componentName, IReciveSendMessage holdTarget)
            : base(componentName, holdTarget)
        {
            _random = new Random();
            _time = 0;
            _order = 0;
        }

        public override void ProcessTheMessage(Message message)
        {
            Message msg;
            switch (message.Type)
            {
                case TypeMessage.Start:
                    msg = CreateHoldMessage();
                    SendHoldMessage(msg);
                    break;
                case TypeMessage.Hold:
                    if (!_activate)
                        return;
                    msg = CreateHoldMessage();
                    SendHoldMessage(msg);
                    msg = new Message(TypeMessage.Notice,
                            Name,
                            ComponentNameManager.AgentSurroundings,
                            "New arrival person",
                            null,
                            message.Timestamp) {DynamicAgent = message.DynamicAgent};
                    SendNoticeMessage(msg);
                    break;
                case TypeMessage.Break:
                    _activate = false;
                    break;
                default:
                    throw new Exception(message.Type + " message has not handler.");
            }
        }

        private DynamicAgent GetNextPerson()
        {
            var managerPerson = new ManagerPerson("Person" + _order++);
            var agentPerson = new DynamicAgent(HoldTarget) {Manager = managerPerson};
            return agentPerson;
        }
        
        private int GetNextArrivalTime()
        {
            _time = _time + _random.Next(5);
            return _time;
        }
        private Message CreateHoldMessage()
        {
            var agentPerson = GetNextPerson();
            var newTimestamp = GetNextArrivalTime();
            var msg = new Message(TypeMessage.Hold,
                    Name,
                    Name,
                    null,
                    null,
                    newTimestamp) {DynamicAgent = agentPerson};
            return msg;
        }
    }
}
