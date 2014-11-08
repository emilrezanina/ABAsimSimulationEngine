using SimulationEngine.Communication;

namespace SimulationEngine.Modules.DiscreteSimulationModule
{
    public interface IReciveSendMessage
    {
        void ReciveMessage(Message message);
        void SendMessage(Message message);
    }
}
