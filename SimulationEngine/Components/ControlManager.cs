using SimulationEngine.Communication;

namespace SimulationEngine.Components
{
    public abstract class ControlManager : AgentManager
    {
        protected ControlManager(string componentName) : base(componentName)
        {
        }

        //DODELAT
        public void SendEntrustMessage(Message message)
        {
            var dynamicAgent = message.DynamicAgent;
            //zbytecne navic
            dynamicAgent.GetLastOwner();
        }
        //DODELAT
        public void SendCancelMessage(Message message)
        {

        }
        //DODELAT
        public void SendGoalMessage(Message message)
        {

        }
        //Tato zprava je vyuzivana na presunuti dynamickeho agenta
        //do pusobnosti jineho ridiciho agenta.
        //Prijimajici agent se stava novym ridicim agentem daneho dynamickeho prostredi
        public void SendHandoverMessage(Message message)
        {
            //resit jestli tam nahodou uz dany agent neni?
            //pridani agenta do modelu
            //presunuti zpravy do meziagentove komunikace
            var dynamicAgent = message.DynamicAgent;
            dynamicAgent. RemoveLastOwner();
            ControlAgent.AgentsComunnication(message);
        }
        //DODELAT
        public void SendReturnMessage(Message message)
        {

        }
    }
}
