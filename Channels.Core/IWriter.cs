using System.Threading;
using System.Threading.Tasks;

namespace Channels.Core
{
    public interface IWriter<TData>
    {
        Task Push(TData data);
    }
}