using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public interface IAgent
    {
        AgentManager Manager { get; set; }
        IComponent CancellingComponent(IComponent component);
        
        IComponent GetComponent(string name);
        void ReciveMessage(Message message);
        void RegistrationComponent(IComponent component);
        MessageRegister IncomingMessageRegister { get; }
        MessageRegister OutgoingMessageRegister { get; }
        AgentModel Model { get; }
        void AgentsComunnicationExecution(Message message);
        bool Equals(IAgent other);
    }
}
