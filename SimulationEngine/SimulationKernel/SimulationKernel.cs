using System;
using System.Threading;
using SimulationEngine.Modules.AnimationModule;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;

namespace SimulationEngine.SimulationKernel
{
    public class SimulationKernel : ISimulationKernel, ISimulationControl
    {
        public DiscreteSimulationModule DiscreteSimulation { get; private set; }
        public ContinuousSimulationModule ContinuousSimulation { get; private set; }
        public AnimationModule Animation { get; private set; }
        public ConfigurationModule Configuration { get; private set; }

        private Thread _performanceThread;
        public long ActualTime { get; set; }
        public short Speed { get; set; }
        public bool Waiting { get; set; }

        public SimulationKernel()
        {
            _performanceThread = null;
            DiscreteSimulation = new DiscreteSimulationModule(this);
            ContinuousSimulation = null;
            Animation = null;
            Configuration = new ConfigurationModule(this);
            Waiting = false;
            ActualTime = 0;
            Speed = 1000;
        }

        public void ChangeSpeedOfSimulation(short speed)
        {
            Speed = speed;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            if (_performanceThread == null)
            {
                _performanceThread = new Thread(DiscreteSimulation.Performance);
                _performanceThread.Start();
            }

            if (Waiting)
                Waiting = false;   
        }

        public void Stop()
        {
            if (_performanceThread != null)
                Waiting = true;
        }

    }
}
