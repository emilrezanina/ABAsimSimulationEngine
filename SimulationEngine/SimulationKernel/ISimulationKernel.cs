namespace SimulationEngine.SimulationKernel
{
    public interface ISimulationKernel
    {
        void ChangeSpeedOfSimulation(short speed);
        void Reset();
        void Run();
        void Stop();
    }
}
