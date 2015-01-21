namespace CustomerService.Structures
{
    public class CustomersGenerator
    {
        private uint _idCounter;
        public Customer Generate()
        {
            return new Customer { Id = _idCounter++ };
        }
    }
}
