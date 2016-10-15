using System.Threading.Tasks;

namespace Swiss.Core
{
    public interface IChatChannel
    {
        Task SendMessageAsync(string message);
    }
}