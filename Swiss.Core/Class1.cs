using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Swiss.Core
{
    public interface IChatServerFactory
    {
        IChatServer CreateServerInstance(string key);
    }

    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Authenticating,
        Connected,
        Disconnecting
    }

    public interface IChatServer
    {
        string Name { get; set; }
        ConnectionState ConnectionState { get; }
        Task ConnectAsync(CancellationToken cancellationToken);
        Task DisconnectAsync(CancellationToken cancellationToken);
    }
    public class DisposeCallback : IDisposable
    {
        public Action OnDispose { get; set; }
        public void Dispose()
        {
            OnDispose();
        }
    }
    public interface IChatChannel
    {
        Task SendMessageAsync(string message);
    }

    public interface IChatMessage
    {
        IChatChannel Channel { get; }
        string Message { get; }
        IChatUser User { get; }
    }

    public interface IChatUser
    {
        string Name { get; }
    }

    public class ChatMessage : IChatMessage
    {
        public virtual IChatChannel Channel { get; set; }
        public virtual string Message { get; set; }
        public virtual IChatUser User { get; set; }

    }
}
