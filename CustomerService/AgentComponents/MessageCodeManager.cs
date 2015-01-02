namespace CustomerService.AgentComponents
{
    public struct MessageCodeManager
    {
        public const string CompleteMoveCustomerToServiceA = "Complete move customer to service A";
        public const string CompleteServiceA = "Complete service A";
        public const string CompleteServiceB = "Complete service B";
        public const string OutgoingCustomer = "Outgoing customer";
        public const string MoveResource = "Move resource";
        public const string MoveNotResource = "Move not resource";
        public const string ReturnResource = "Return resource";
        public const string CompleteMoveResource = "Complete move resource";
        public const string DeliverResource = "Deliver resource";
        public const string StartSimulation = "Start simulation";
        public const string IncomingCustomer = "Incoming customer";
        public const string OpenGate = "Open gate";
        public const string WaitingNewCustomer = "Waiting new customer";
        public const string MovingCustomer = "Moving customer";
        public const string StartServiceA = "Start service A";
        public const string StartServiceB = "Start service B";
        public const string MovingOutgoingCustomer = "Moving outgoing customer";
        public const string IsQueueOfApplicantEmpty = "Is queue of applicant empty";
    }

    public struct ParameterNameManager
    {
        public const string Applicant = "Applicant";
        public const string Resource = "Resource";
        public const string ResourceType = "ResourceType";
        public const string Customer = "Customer";
        public const string EndTime = "End time";
    }

    public struct ResultNameManager
    {
        public const string ResourceAssigned = "Resource assigned";
        public const string ApplicantInQueue = "Applicant in queue";
        public const string AssignResource = "Assign resource";
        public const string ResourceReturned = "Resource returned";
        public const string AssignNotResource = "Assign not resource";
        public const string ServiceA = "Service A";
        public const string ServiceB = "Service B";
        public const string QueueIsEmpty = "Queue is empty";
        public const string QueueIsntEmpty = "Queue isnt empty";
    }

    public struct ComponentNameManager
    {
        public const string ActionReturnResource = "aReturnResource";
        public const string QueryIsQueueOfApplicantEmpty = "qIsQueueOfApplicantEmpty";
        public const string AgentSurroundings = "aSurroundings";
        public const string ProcessEnteringCustomer = "pEnteringCustomer";
        public const string AgentService = "aService";
        public const string ProcessMoveCustomer = "pMoveCustomer";
        public const string QueryChoosingServiceType = "qChoosingServiceType";
        public const string ProcessServiceA = "pServiceA";
        public const string ProcessServiceB = "pServiceB";
        public const string ProcessCustomerOutgoing = "pCustomerOutgoing";
        public const string AgentResourceAdministrator = "aResourceAdministrator";
        public const string ProcessMoveResource = "pMoveResource";
        public const string AdvisorSelectionOfFreeResources = "adSelectionOfFreeResources";
        public const string ActionAssignResource = "aAssignResource";
        public const string QueryNeedMoveResource = "qNeedMoveResource";
        public const string ActionPutApplicantToQueueOnResource = "aPutApplicantToQueueOnResource";
        public const string ActionRemoveApplicantFromQueue = "aRemoveApplicantFromQueue";
    }
}
