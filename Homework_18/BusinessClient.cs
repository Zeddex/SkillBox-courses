﻿using System;

namespace Homework_18
{
    public class Business : Client
    {
        public override int LoanRate { get; set; } = 10;
        public override int DepositRate { get; set; } = 10;
        public override string Status { get; set; } = "Business";

        public Business() : base($"Business Client-{Guid.NewGuid().ToString().Substring(0, 5)}") { }
    }
}