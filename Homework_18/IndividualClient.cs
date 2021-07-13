using System;

namespace Homework_18
{
    public class Individual : Client
    {
        public override int LoanRate { get; set; } = 15;
        public override int DepositRate { get; set; } = 5;
        public override string Status { get; set; } = "Individual";

        public Individual() : base($"Individual Client-{Guid.NewGuid().ToString().Substring(0, 5)}") { }
    }
}