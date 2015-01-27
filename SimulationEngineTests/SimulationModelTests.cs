using SimulationEngine.Modules.SimulationModelModule;
using Xunit;

namespace SimulationEngineTests
{
    public class SimulationModelTests
    {
        [Fact]
        public void ReturnMethodIsEmptyTrueIfSimulationModelIsWithoutUnprocessedMessages()
        {
            var simModel = new SimulationModel();
            Assert.True(simModel.IsEmpty());
        }
        //[Fact]
        //void CheckCountOfAgent()
        //{
        //    //kontrola pri vlozeni agentu do simulacniho modelu, ze jich tam je ...
        //    //pouziti spise Theory
        //}
    }
}
