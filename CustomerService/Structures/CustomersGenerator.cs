namespace CustomerService.Structures
{
    public class CustomersGenerator
    {
        private uint _idCounter = 0;
        public Customer Generate()
        {
            return new Customer() { Id = _idCounter++ };
        }
    }
}
