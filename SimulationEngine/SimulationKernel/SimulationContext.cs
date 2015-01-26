﻿using System;
using System.Threading;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulatorWriters;

namespace SimulationEngine.SimulationKernel
{
    public class SimulationContext : ISimulationKernel, ISimulationContext
    {
        private Thread _performanceThread;
        private long _actaulTime;

        public DiscreteSimulationController DiscreteSimController { get; private set; }
        public SimulationModel SimModel { get; set; }


        public ContinuousSimulationController ContinuousSimController { get; private set; }

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

        public SimulationContext()
        {
            DiscreteSimController = new DiscreteSimulationController(this);
            ContinuousSimController = new ContinuousSimulationController(this);
            ActualTimeOutputProvider = new ActualTimeOutputProvider();
            MessageOutputProvider = new CommunicationOutputProvider();

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
                _performanceThread = new Thread(DiscreteSimController.Performance);
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
