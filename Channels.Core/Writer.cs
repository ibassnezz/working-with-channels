using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Channels.Core
{
    public class Writer<TData> : IWriter<TData>
    {
        private readonly ChannelWriter<TData> _writer;
        private readonly CancellationToken _cancellationToken;
        private readonly Identification _identification;

        internal Writer(ChannelWriter<TData> writer, CancellationToken cancellationToken, Identification identification)
        {
            _writer = writer;
            _cancellationToken = cancellationToken;
            _identification = identification;
        }

        public async Task Push(TData data)
        {
            try
            {
                if (await _writer.WaitToWriteAsync(_cancellationToken))
                {
                    _writer.TryWrite(data);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Reader {_identification.Name} {_identification.Id} stopped");
            }
        }
    }
}