using System;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.SimulationKernel;
using SimulationEngine.SimulatorWriters;
using TestingProject.Structures;

namespace TestingProject
{
    class Program
    {
        static void Main()
        {
            var simulatorOutput = new CommunicationOutputProvider();
            var simModel = new SimulationModel();
            var simKernel = new SimulationContext()
            {
                MessageOutputProvider = simulatorOutput
            };

            var surroundingsManager = new ManagerSurroundings("mSurroundings");
            var generatorCustomersPersons = new ProcessGeneratorPersons("pGeneratorPersons", simKernel.DiscreteSimController);
            var surroundingsAgent = new ControlAgent(simKernel.DiscreteSimController, surroundingsManager);
            surroundingsAgent.RegistrationComponent(generatorCustomersPersons);
            surroundingsAgent.RegistrationCodeMessage("Begin generation", new String[] { });
            surroundingsAgent.RegistrationCodeMessage("End generation", new String[] { });
            surroundingsAgent.RegistrationCodeMessage("Person left", new[] { "Customer" });

            var modelManager = new ManagerModel("mModel");
            var modelAgent = new ControlAgent(simKernel.DiscreteSimController, modelManager);
            modelAgent.RegistrationCodeMessage("New person", new String[] { });
 
            var serviceAManager = new ManagerServiceA("mServiceA");
            var serviceAAgent = new ControlAgent(simKernel.DiscreteSimController, serviceAManager);
            serviceAAgent.RegistrationCodeMessage("Start service A", new String[] { });

            simModel.RegistrationControlAgent(surroundingsAgent);
            simModel.RegistrationControlAgent(modelAgent);

            var msg = MessageProvider.CreateMessage(TypeMessage.Notice,
                    null,
                    "mSurroundings",
                    "Begin generation",
                    null,
                    0);

            simKernel.SimModel = simModel;
            simKernel.DiscreteSimController.ReciveMessage(msg);
            simKernel.Run();

        }
    }
}
