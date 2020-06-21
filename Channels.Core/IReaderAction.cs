using System.Threading;
using System.Threading.Tasks;

namespace Channels.Core
{
    public interface IReaderAction<in TData>
    {
        Identification Identification { get; }
        Task Handle(TData data, CancellationToken cancellationToken);
    }
}