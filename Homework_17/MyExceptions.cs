using System;

namespace Homework_17
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
