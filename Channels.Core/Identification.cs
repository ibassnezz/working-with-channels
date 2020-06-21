using System;

namespace Channels.Core
{
    public class Identification
    {
        public Identification(string name = null)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public string Name { get; }
        public Guid Id { get; }
    }
}