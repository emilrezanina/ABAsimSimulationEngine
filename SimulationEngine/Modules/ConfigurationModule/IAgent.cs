using SimulationEngine.Communication;
using SimulationEngine.Components;

namespace SimulationEngine.Modules.ConfigurationModule
{
    public interface IAgent
    {
        AgentManager Manager { get; set; }
        IComponent CancellingComponent(IComponent component);
        string CancellingCodeMessage(string codeMessage);
        IComponent GetComponent(string name);
        int MessageCount { get; }
        bool HasCodeMessage(string codeMessage);
        void ProcessAllMessages();
        void ReciveMessage(Message message);
        void RegistrationComponent(IComponent component);
        void RegistrationCodeMessage(string codeMessage, params string[] attributes);
        AgentModel ControlModel { get; }
        void AgentsComunnication(Message message);
        bool Equals(IAgent other);
    }
}
