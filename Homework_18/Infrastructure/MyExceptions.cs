using System;

namespace Homework_18.Infrastructure
{
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException(string message) : base(message) {}
    }

    public class WrongAmountException : ApplicationException
    {
        public WrongAmountException(string message) : base(message) {}
    }
}
