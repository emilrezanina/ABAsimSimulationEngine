using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace SimulationEngine.Modules.SimulationModelModule
{
    public interface IAgent
    {
        AgentManager Manager { get; set; }
        IComponent CancellingComponent(IComponent component);
        string CancellingCodeMessage(string codeMessage);
        IComponent GetComponent(string name);
        bool HasCodeMessage(string codeMessage);
        void ReciveMessage(Message message);
        void RegistrationComponent(IComponent component);
        void RegistrationCodeMessage(string codeMessage, params string[] attributes);
        AgentModel Model { get; }
        void AgentsComunnicationExecution(Message message);
        bool Equals(IAgent other);
    }
}
