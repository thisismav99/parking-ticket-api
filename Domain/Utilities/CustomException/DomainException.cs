namespace Domain.Utilities.CustomException
{
    internal class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
