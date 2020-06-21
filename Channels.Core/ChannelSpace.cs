using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Channels.Core
{
    public class ChannelSpace<TData>
    {
        private readonly Channel<TData> _channel;
        private CancellationTokenSource _source = new CancellationTokenSource();
        public ChannelSpace(int? backPressureLimit = null)
        {
            _channel = backPressureLimit.HasValue
                ? Channel.CreateBounded<TData>(backPressureLimit.Value)
                : Channel.CreateUnbounded<TData>();
        }

        public void ApplyReader(IReaderAction<TData> readerAction)
        {
            var reader = new Reader<TData>(_channel.Reader, readerAction);
            var awaiter = reader.BeginConsume(_source.Token).GetAwaiter();
            if (awaiter.IsCompleted)
                throw new OperationCanceledException($"Name: {reader.Identification.Name}>{readerAction.Identification.Id}");
        }

        public IWriter<TData> RetrieveWriter(Identification identification)
        {
            var writer = new Writer<TData>(_channel.Writer, _source.Token, identification);
            return writer;
        }

        public void Complete()
        {
            _source.Cancel();
            _channel.Writer.Complete();
        }
    }
}
