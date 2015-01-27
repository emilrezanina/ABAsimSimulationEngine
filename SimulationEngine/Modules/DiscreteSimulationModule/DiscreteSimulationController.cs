﻿using System;
using System.Threading;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;
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
            //0. Inicializace simulacniho casu diksretni simulace
            Control.ActualTime = 0;
            //2. Ukonceni simulace pokud:
            //      a) je vycerpany cas pro beh simulacniho programu
            //      b) v simulacnim programu nejsou zadne zpravy
            while ((!_centralMailbox.IsEmpty()
                    || Control.SimModel.HaveComponentsMessages()))
            {
                //3. Postupne vydavani pokynu vsem komponentam na odebirani zprav
                //ze svojich schranek a jejich okamzitemu zpracovani
                while (Control.SimModel.HaveComponentsMessages())
                {
                    //DODELAT OPTIMALIZACI, ABYCH NASEL HNED PRISLUSNY ZPRAVY KOMPONENTU
                    foreach (IAgent agent in Control.SimModel.Agents)
                    {
                        agent.ProcessAllMessages();
                    }
                }
                //10. Aktualizace simulacniho casu diskretni simulace           
                Message msg;
                //Kontrola jestli v centralni poste jsou zpravy
                if ((msg = _centralMailbox.GetEarliestMessage()) != null)
                {
                    Control.ActualTime = msg.Timestamp;
                    //11. Odebrani vsech zprav z kalendare s casovym razitkem
                    // stejnym jako actualTime
                    while (msg.Timestamp == Control.ActualTime)
                    {
                        //odeslani zpravy
                        msg = _centralMailbox.RemoveMessage();
                        MessageProcessing(msg, true);
                        //Posun na dalsi zpravu
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

        //ODESLANI DO INFRASTRUKTURY
        public void SendMessage(Message message)
        {
            MessageProcessing(message, false);
        }

        private void MessageProcessing(Message message, bool immediately)
        {
            //nalezeni adresata
            var addressee = Control.SimModel.FindAddressee(message.Addressee);
            if (addressee != null)
            {
                if (immediately)
                {
                    Control.MessageOutputProvider.TraceReceivedMessage(message);
                    addressee.ProcessTheMessage(message);
                }
                else
                {
                    addressee.ControlAgent.ReciveMessage(message);
                }
            }
            else
            {
                //VYVOLANI VYJIMKY, KDYZ ADRESAT NENI NALEZEN
                throw new Exception("Adressee " + message.Addressee + "not found.");
            }
        }
    }
}
