namespace MicrogrooveChallenge.BLL.Exceptions
{
    public class CustomerModelValidationException : Exception
    {
        public CustomerModelValidationException()
        {
        }

        public CustomerModelValidationException(string message)
            : base(message)
        {
        }

        public CustomerModelValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
