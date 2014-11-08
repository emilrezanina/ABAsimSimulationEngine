namespace CustomerService.Structures
{
    public class ServiceResourse
    {
        public enum EServiceResourseType
        {
            A,
            B
        }

        public EServiceResourseType Type { get; private set; }

        public ServiceResourse(EServiceResourseType type)
        {
            Type = type;
        }
    }
}
