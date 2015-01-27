using SimulationEngine.Communication;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.Modules.DiscreteSimulationModule
{
    public interface IReciveSendMessage
    {
        void ReciveMessage(Message message);
        void SendMessage(Message message);
    }
}
