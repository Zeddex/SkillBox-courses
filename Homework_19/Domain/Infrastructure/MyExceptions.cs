using System;

namespace Domain.Infrastructure
{
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException(string message) : base(message) {}
    }

    public class WrongAmountException : ApplicationException
    {
        public WrongAmountException(string message) : base(message) {}
    }

    public class DbErrorConnection : ApplicationException
    {
        public DbErrorConnection(string message) : base(message) { }
    }
}
