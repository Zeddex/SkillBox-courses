using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13
{
    public abstract class Client
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public abstract byte Rate { get; set; }

        public Client(string name = "RandomClient")
        {
            Name = name;
        }
    }

    public class Individual : Client
    {
        public override byte Rate { get; set; } = 5;
        public Individual() : base($"Individual Client-{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

    internal class Business : Client
    {
        public override byte Rate { get; set; } = 10;
        public Business() : base($"Business Client-{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

    internal class Vip : Client
    {
        public override byte Rate { get; set; } = 15;
        public Vip() : base($"VIP Client-{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

}

