using SimulationEngine.Communication;
using SimulationEngine.SimulatorWriter;

namespace SimulationEngine.Modules.DiscreteSimulationModule
{
    public interface IReciveSendMessage
    {
        void ReciveMessage(Message message);
        void SendMessage(Message message);
        CommunicationOutputProvider MessageOutputProvider { get; }
    }
}
