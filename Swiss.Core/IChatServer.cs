using System.Threading;
using System.Threading.Tasks;

namespace Swiss.Core
{
    public interface IChatServer
    {
        string Name { get; set; }
        ConnectionState ConnectionState { get; }
        Task ConnectAsync(CancellationToken cancellationToken);
        Task DisconnectAsync(CancellationToken cancellationToken);
    }
}