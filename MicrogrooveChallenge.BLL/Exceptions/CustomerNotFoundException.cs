namespace MicrogrooveChallenge.BLL.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException()
        {
        }

        public CustomerNotFoundException(string message)
            : base(message)
        {
        }

        public CustomerNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
