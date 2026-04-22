namespace Domain.Utilities.Exceptions
{
    internal class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
