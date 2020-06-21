using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Channels.Core
{
    internal class Reader<TData> : IReader
    {
        public Identification Identification => _readerAction.Identification;
        private readonly ChannelReader<TData> _channel;
        private readonly IReaderAction<TData> _readerAction;

        public Reader(ChannelReader<TData> channel, IReaderAction<TData> readerAction)
        {
            if(readerAction.Identification is null) 
                throw new ArgumentNullException(nameof(readerAction.Identification));

            _channel = channel;
            _readerAction = readerAction;
        }

        public async Task BeginConsume(CancellationToken cancellationToken)
        {
            try
            {
                while (await _channel.WaitToReadAsync(cancellationToken))
                {
                    if (!_channel.TryRead(out var data))
                        Console.WriteLine("Message has been intercepted!");

                    try
                    {
                        await _readerAction.Handle(data, cancellationToken);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Reader {Identification.Name} {Identification.Id} stopped");
            }
        }
    }
}