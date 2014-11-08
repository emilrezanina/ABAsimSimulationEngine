﻿using System;
using SimulationEngine.Communication;
using SimulationEngine.Modules.ConfigurationModule;
using SimulationEngine.SimulationKernel;
using TestingProject.Structures;

namespace TestingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var simKernel = new SimulationKernel();
            //Agent okoli
            var surroundingsManager = new ManagerSurroundings("mSurroundings");
            var generatorCustomersPersons = new ProcessGeneratorPersons("pGeneratorPersons", simKernel.DiscreteSimulation);
            var surroundingsAgent = new ControlAgent(simKernel.DiscreteSimulation) {Manager = surroundingsManager};
            surroundingsAgent.RegistrationComponent(generatorCustomersPersons);
            surroundingsAgent.RegistrationCodeMessage("Begin generation", new String[] { });
            surroundingsAgent.RegistrationCodeMessage("End generation", new String[] { });
            surroundingsAgent.RegistrationCodeMessage("Person left", new[] { "Customer" });

            //Agent modelu
            var modelManager = new ManagerModel("mModel");
            var modelAgent = new ControlAgent(simKernel.DiscreteSimulation) {Manager = modelManager};
            modelAgent.RegistrationCodeMessage("New person", new String[] { });

            //Agent obsluhy A
            var serviceAManager = new ManagerServiceA("mServiceA");
            var serviceAAgent = new ControlAgent(simKernel.DiscreteSimulation) {Manager = serviceAManager};
            serviceAAgent.RegistrationCodeMessage("Start service A", new String[] { });

            simKernel.Configuration.RegistrationAgent(surroundingsAgent);
            simKernel.Configuration.RegistrationAgent(modelAgent);

            var msg = new Message(TypeMessage.Notice,
                    null,
                    "mSurroundings",
                    "Begin generation",
                    null,
                    0);

            simKernel.DiscreteSimulation.ReciveMessage(msg);
            simKernel.Run();

        }
    }
}