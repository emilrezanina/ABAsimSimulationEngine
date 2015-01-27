using System.Threading;
using SimulationEngine.Communication;
using SimulationEngine.SimulationKernel;

namespace SimulationEngine.Modules.DiscreteSimulationModule
{
    public class DiscreteSimulationController : IAttachedModule, IReciveSendMessage
    {
        private readonly Mailbox _centralMailbox;
        public ISimulationContext Control { get; private set; }
        public DiscreteSimulationController(ISimulationContext control)
        {
            Control = control;
            _centralMailbox = new Mailbox();
        }

        public void Performance()
        {
            Control.ActualTime = 0;
            while (!(_centralMailbox.IsEmpty()
                    && Control.SimModel.IsEmpty()))
            {
                if(!Control.SimModel.IsEmpty())
                    Control.SimModel.ProcessAllInterAgentMessages();
                    
        
                Message msg;
                if ((msg = _centralMailbox.GetEarliestMessage()) != null)
                {
                    Control.ActualTime = msg.Timestamp;
                    while (msg.Timestamp == Control.ActualTime)
                    {
                        msg = _centralMailbox.RemoveMessage();
                        Control.SimModel.ReceiveMessage(msg, true);
                        msg = _centralMailbox.GetEarliestMessage();
                        if (msg == null)
                        {
                            break;
                        }
                    }
                }
                Thread.Sleep(Control.Speed);
                while (Control.Waiting)
                {
                    Thread.SpinWait(1000);
                }
            }
        }

        public void ReciveMessage(Message message)
        {
            _centralMailbox.AddMessage(message);
        }

        public void SendMessage(Message message)
        {
            Control.SimModel.ReceiveMessage(message, false);
        }

       
    }
}
