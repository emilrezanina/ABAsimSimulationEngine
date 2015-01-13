using System;
using System.Threading;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.SimulationKernel
{
    public class SimulationKernel : ISimulationKernel, ISimulationControl
    {
        private Thread _performanceThread;
        private long _actaulTime;

        public DiscreteSimulationModule DiscreteSimulation { get; private set; }
        public ContinuousSimulationModule ContinuousSimulation { get; private set; }
        public ConfigurationModule Configuration { get; private set; }

        public CommunicationOutputProvider MessageOutputProvider { get; set; }
        public ActualTimeOutputProvider ActualTimeOutputProvider { get; set; }

        public long ActualTime
        {
            get { return _actaulTime; }
            set
            {
                _actaulTime = value;
                ActualTimeOutputProvider.ChangingActualTime(value);
            }
        }
        public short Speed { get; set; }
        public bool Waiting { get; set; }

        public SimulationKernel()
        {
            DiscreteSimulation = new DiscreteSimulationModule(this);
            ContinuousSimulation = new ContinuousSimulationModule(this);
            Configuration = new ConfigurationModule(this);

            _performanceThread = null;
            Waiting = false;
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
