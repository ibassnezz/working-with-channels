using System.Threading;
using System.Threading.Tasks;

namespace Channels.Core
{
    public interface IReader
    {
        Task BeginConsume(CancellationToken cancellationToken);
    }
}