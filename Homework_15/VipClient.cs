using System;

namespace Homework_15
{
    internal class Vip : Client
    {
        public override int LoanRate { get; set; } = 5;
        public override int DepositRate { get; set; } = 15;
        public override string Status { get; set; } = "VIP";

        public Vip() : base($"VIP Client-{Guid.NewGuid().ToString().Substring(0, 5)}") { }
    }
}