using System;
using System.Threading;
using System.Threading.Tasks;
using Channels.Core;

namespace WorkingWithChannels
{
    public class ReaderAction : IReaderAction<string>
    {
        public ReaderAction(Identification identification)
        {
            Identification = identification;
        }
        public Identification Identification { get; }
        public Task Handle(string data, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{Identification.Name} > {data}");
            return Task.CompletedTask;
        }
    }
}