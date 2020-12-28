using System;
using System.Collections.Generic;
using System.Text;

namespace Homework_13
{
    public abstract class Client
    {
        public string Name { get; set; }
        public uint Money { get; set; }
        public abstract byte Rate { get; set; }

        public Client(string name = "RandomClient")
        {
            Name = name;
        }
    }

    public class Individual : Client
    {
        public override byte Rate { get; set; } = 5;
        public Individual() : base($"individual_{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

    internal class Business : Client
    {
        public override byte Rate { get; set; } = 10;
        public Business() : base($"business_{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

    internal class Vip : Client
    {
        public override byte Rate { get; set; } = 15;
        public Vip() : base($"vip_{Guid.NewGuid().ToString().Substring(0, 5)}") {}
    }

}

