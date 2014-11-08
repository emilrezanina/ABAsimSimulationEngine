using System;
using System.Threading;
using SimulationEngine.Modules.AnimationModule;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.SimulatorWriter;

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

        private Thread _performanceThread;
        public long ActualTime { get; set; }
        public short Speed { get; set; }
        public bool Waiting { get; set; }

        public SimulationKernel(CommunicationOutputProvider messageOutputProvider)
        {
            MessageOutputProvider = messageOutputProvider;
            _performanceThread = null;
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
