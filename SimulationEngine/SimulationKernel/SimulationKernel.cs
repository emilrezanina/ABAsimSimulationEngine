using System;
using System.Threading;
using SimulationEngine.Modules.AnimationModule;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.SimulationKernel
{
    public class SimulationKernel : ISimulationKernel, ISimulationControl
    {
        private DiscreteSimulationModule _discreteSimulation;
        public DiscreteSimulationModule DiscreteSimulation
        {
            get { return _discreteSimulation; }
            set
            {
                _discreteSimulation = value;
                _discreteSimulation.Control = this;
            }
        }

        private ContinuousSimulationModule _continuousSimulation;
        public ContinuousSimulationModule ContinuousSimulation
        {
            get { return _continuousSimulation; }
            set
            {
                _continuousSimulation = value;
                _continuousSimulation.Control = this;
            }
        }

        public AnimationModule Animation { get; set; }

        private ConfigurationModule _configuration;
        public ConfigurationModule Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
                _configuration.Control = this;
            }
        }

        public CommunicationOutputProvider MessageOutputProvider { get; set; }
        public ActualTimeOutputProvider ActualTimeOutputProvider { get; set; }

        private Thread _performanceThread;
        private long _actaulTime;
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
