﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using CustomerService.AgentComponents;
using CustomerService.Bear;
using CustomerService.Structures;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule;
using SimulationEngine.Modules.SimulationModelModule.Components;
using SimulationEngine.SimulationKernel;

namespace CustomerService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly SimulationContext _simulation;

        #region ObservableCollection
        //Zakaznici cekajici na obsluhu A
        private readonly ObservableCollection<Customer> _incomingCustomers = new ObservableCollection<Customer>();
        private readonly object _incomingCustomersLock = new object();
        public ObservableCollection<Customer> IncomingCustomers
        {
            get { return _incomingCustomers; }
        }

        private readonly ObservableCollection<Customer> _customersWaitingOnServiceA = new ObservableCollection<Customer>();
        private readonly object _customersWaitingOnServiceALock = new object();
        public ObservableCollection<Customer> CustomersWaitingOnServiceA
        {
            get { return _customersWaitingOnServiceA; }
        }

        private readonly ObservableCollection<Customer> _customersInServiceA = new ObservableCollection<Customer>();
        private readonly object _customersInServiceALock = new object();
        public ObservableCollection<Customer> CustomersInServiceA
        {
            get { return _customersInServiceA; }
        }

        private readonly ObservableCollection<Customer> _movingCustomersToServiceB = new ObservableCollection<Customer>();
        private readonly object _movingCustomersToServiceBLock = new object();
        public ObservableCollection<Customer> MovingCustomersToServiceB
        {
            get { return _movingCustomersToServiceB; }
        }

        private readonly ObservableCollection<Customer> _customersWaitingOnServiceB = new ObservableCollection<Customer>();
        private readonly object _customersWaitingOnServiceBLock = new object();
        public ObservableCollection<Customer> CustomersWaitingOnServiceB
        {
            get { return _customersWaitingOnServiceB; }
        }

        private readonly ObservableCollection<Customer> _customersInServiceB= new ObservableCollection<Customer>();
        private readonly object _customersInServiceBLock = new object();
        public ObservableCollection<Customer> CustomersInServiceB
        {
            get { return _customersInServiceB; }
        }

        private readonly ObservableCollection<Customer> _finishedCustomers = new ObservableCollection<Customer>();
        private readonly object _finishedCustomersLock = new object();
        public ObservableCollection<Customer> FinishedCustomers
        {
            get { return _finishedCustomers; }
        }

        private readonly ObservableCollection<Customer> _outgoingCustomers = new ObservableCollection<Customer>();
        private readonly object _outgoingCustomersLock = new object();
        public ObservableCollection<Customer> OutgoingCustomers
        {
            get { return _outgoingCustomers; }
        }

        private readonly ObservableCollection<ServiceResourse> _resourcesA = new ObservableCollection<ServiceResourse>();
        private readonly object _resourcesALock = new object();
        public ObservableCollection<ServiceResourse> ResourcesA
        {
            get { return _resourcesA; }
        }

        private readonly ObservableCollection<ServiceResourse> _resourcesB = new ObservableCollection<ServiceResourse>();
        private readonly object _resourcesBLock = new object();
        public ObservableCollection<ServiceResourse> ResourcesB
        {
            get { return _resourcesB; }
        }

        #endregion
        public bool IsModelInicialized { get; private set; }

        private class CommunicationOutputReciever : IObserver<Message>
        {
            private readonly TextBlock _communicationOutput;
            private readonly Dispatcher _dispatcher;
            public CommunicationOutputReciever(TextBlock communicationOutput, Dispatcher dispatcher)
            {
                _communicationOutput = communicationOutput;
                _dispatcher = dispatcher;
            }

            public void OnNext(Message value)
            {
                _dispatcher.Invoke(
                    () =>
                    {
                        var message = value.ToString();
                        _communicationOutput.Text += message;
                        _communicationOutput.Text += "\n";
                    } );

            }
             
            public void OnError(Exception error)
            {
                throw error;
            }

            public void OnCompleted()
            {
                
            }
        };
        private class ActualTimeOutputReciever : IObserver<long>
        {
            private readonly TextBlock _actuaTimeOutput;
            private readonly Dispatcher _dispatcher;
            public ActualTimeOutputReciever(TextBlock actuaTimeOutput, Dispatcher dispatcher)
            {
                _actuaTimeOutput = actuaTimeOutput;
                _dispatcher = dispatcher;
            }

            public void OnNext(long value)
            {
                _dispatcher.Invoke(
                    () =>
                    {
                        _actuaTimeOutput.Text = value.ToString(CultureInfo.InvariantCulture);
                    });

            }

            public void OnError(Exception error)
            {
                throw error;
            }
            public void OnCompleted()
            {

            }
        };

        public MainWindow()
        {
            DataContext = this;
            BindingOperations.EnableCollectionSynchronization(IncomingCustomers, _incomingCustomersLock);
            BindingOperations.EnableCollectionSynchronization(CustomersWaitingOnServiceA, _customersWaitingOnServiceALock);
            BindingOperations.EnableCollectionSynchronization(CustomersInServiceA, _customersInServiceALock);
            BindingOperations.EnableCollectionSynchronization(MovingCustomersToServiceB, _movingCustomersToServiceBLock);
            BindingOperations.EnableCollectionSynchronization(CustomersWaitingOnServiceB, _customersWaitingOnServiceBLock);
            BindingOperations.EnableCollectionSynchronization(CustomersInServiceB, _customersInServiceBLock);
            BindingOperations.EnableCollectionSynchronization(FinishedCustomers, _finishedCustomersLock);
            BindingOperations.EnableCollectionSynchronization(OutgoingCustomers, _outgoingCustomersLock);
            BindingOperations.EnableCollectionSynchronization(ResourcesA, _resourcesALock);
            BindingOperations.EnableCollectionSynchronization(ResourcesB, _resourcesBLock);


            _simulation = new SimulationContext();

            InitializeComponent();
            DataContext = this;
            new BearInjection(ThisWindow).Activate();
        }

        private void InicializeSimulationModel()
        {   
            var model = new ServiceSystemModel(this, 2, 1); //Pocet zdroju A: 1, Pocet zdroju B: 3
            var simModel = new SimulationModel();

            AgentManager managerSurroundings = new ManagerSurroundings(ComponentNameManager.AgentSurroundings); //nesel by ComponentName odstranit
            IComponent processEnteringCustomer = new ProcessEnteringCustomer(ComponentNameManager.ProcessEnteringCustomer,
                _simulation.DiscreteSimController, model);
            var agentSurroundings = new ControlAgent(_simulation.DiscreteSimController, managerSurroundings);
            agentSurroundings.RegistrationComponent(processEnteringCustomer);
            simModel.RegistrationControlAgent(agentSurroundings);
            agentSurroundings.RegistrationCodeMessage(MessageCodeManager.OutgoingCustomer, new[] { ParameterNameManager.Applicant });

            AgentManager managerService = new ManagerService(ComponentNameManager.AgentService);
            var processMoveCustomer = new ProcessMoveCustomer(ComponentNameManager.ProcessMoveCustomer,
                _simulation.DiscreteSimController, model);
            var queryChoosingServiceType = new QueryChoosingServiceType(ComponentNameManager.QueryChoosingServiceType);
            var processServiceA = new ProcessServiceA(ComponentNameManager.ProcessServiceA, _simulation.DiscreteSimController, model);
            var processServiceB = new ProcessServiceB(ComponentNameManager.ProcessServiceB, _simulation.DiscreteSimController, model);
            var processCustomerOutgoing = new ProcessCustomerOutgoing(ComponentNameManager.ProcessCustomerOutgoing , 
                _simulation.DiscreteSimController, model);
            var agentService = new ControlAgent(_simulation.DiscreteSimController, managerService);
            agentService.RegistrationComponent(processMoveCustomer);
            agentService.RegistrationComponent(queryChoosingServiceType);
            agentService.RegistrationComponent(processServiceA);
            agentService.RegistrationComponent(processServiceB);
            agentService.RegistrationComponent(processCustomerOutgoing);
            simModel.RegistrationControlAgent(agentService);
            agentService.RegistrationCodeMessage(MessageCodeManager.IncomingCustomer, new [] { ParameterNameManager.Customer });
            agentService.RegistrationCodeMessage(MessageCodeManager.DeliverResource, new[] { ParameterNameManager.Applicant });

            var managerResourceAdministrator = new ManagerResourceAdministrator(ComponentNameManager.AgentResourceAdministrator);
            var processMoveResource = new ProcessMoveResource(ComponentNameManager.ProcessMoveResource, _simulation.DiscreteSimController);
            var advisorSelectionOfFreeResources = new AdvisorSelectionOfFreeResources(ComponentNameManager.AdvisorSelectionOfFreeResources, model);
            var actionAssignResource = new ActionAssignResource(ComponentNameManager.ActionAssignResource, model);
            var queryNeedMoveResource = new QueryNeedMoveResource(ComponentNameManager.QueryNeedMoveResource);
            var actionPutApplicantToQueueOnResource = new ActionPutApplicantToQueueOnResource(ComponentNameManager.ActionPutApplicantToQueueOnResource, model);
            var actionReturnResource = new ActionReturnResource(ComponentNameManager.ActionReturnResource, model);
            var queryIsQueueOfApplicantEmpty = new QueryIsQueueOfApplicantEmpty(ComponentNameManager.QueryIsQueueOfApplicantEmpty, model);
            var actionRemoveApplicantFromQueue = new ActionRemoveApplicantFromQueue(ComponentNameManager.ActionRemoveApplicantFromQueue, model);
            var agentResourceAdministrator = new ControlAgent(_simulation.DiscreteSimController, managerResourceAdministrator);
            agentResourceAdministrator.RegistrationComponent(advisorSelectionOfFreeResources);
            agentResourceAdministrator.RegistrationComponent(actionAssignResource);
            agentResourceAdministrator.RegistrationComponent(queryNeedMoveResource);
            agentResourceAdministrator.RegistrationComponent(processMoveResource);
            agentResourceAdministrator.RegistrationComponent(actionPutApplicantToQueueOnResource);
            agentResourceAdministrator.RegistrationComponent(actionReturnResource);
            agentResourceAdministrator.RegistrationComponent(queryIsQueueOfApplicantEmpty);
            agentResourceAdministrator.RegistrationComponent(actionRemoveApplicantFromQueue);
            simModel.RegistrationControlAgent(agentResourceAdministrator);
            agentResourceAdministrator.RegistrationCodeMessage(MessageCodeManager.DeliverResource, new[] { ParameterNameManager.Applicant });
            agentResourceAdministrator.RegistrationCodeMessage(MessageCodeManager.CompleteMoveResource, new[] { ParameterNameManager.Resource });

            var startMessage = MessageProvider.CreateMessage(TypeMessage.Notice, null, 
                ComponentNameManager.AgentSurroundings,
                MessageCodeManager.StartSimulation, null, 0);
            _simulation.DiscreteSimController.ReciveMessage(startMessage);

            _simulation.SimModel = simModel;
            IsModelInicialized = true;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!IsModelInicialized)
            {
                InicializeSimulationModel();    
            }
            _simulation.Run();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _simulation.Stop();
        }
    }
}
