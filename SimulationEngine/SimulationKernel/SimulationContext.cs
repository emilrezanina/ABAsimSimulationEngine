using System;
using System.Threading.Tasks;
using SimulationEngine.Modules.ContinuousSimulationModule;
using SimulationEngine.Modules.DiscreteSimulationModule;
using SimulationEngine.Modules.SimulationModelModule;

namespace SimulationEngine.SimulationKernel
{
    public class SimulationContext : ISimulationKernel, ISimulationContext
    {
        private Task _performanceThread;

        public DiscreteSimulationController DiscreteSimController { get; private set; }
        public SimulationModel SimModel { get; set; }


        public ContinuousSimulationController ContinuousSimController { get; private set; }


        public long ActualTime { get; set; }

        public short Speed { get; set; }
        public bool Waiting { get; set; }

        public SimulationContext()
        {
            DiscreteSimController = new DiscreteSimulationController(this);
            ContinuousSimController = new ContinuousSimulationController(this);

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
                _performanceThread = new Task(DiscreteSimController.Performance);
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
