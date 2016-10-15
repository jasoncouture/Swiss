using System;
using System.Threading;
using System.Threading.Tasks;
using Swiss.Core;

namespace Swiss.FakeServer
{
    public class TestServer : IChatServer
    {
        public TestServer(string name)
        {
            Name = name;
            ConnectionState = ConnectionState.Disconnected;
        }
        private bool _connected = false;
        public string Name { get; set; }
        public ConnectionState ConnectionState { get; set; }
        public async Task ConnectAsync(CancellationToken cancellationToken)
        {
            if (ConnectionState != ConnectionState.Disconnected) throw new InvalidOperationException("Already connected");
            ConnectionState = ConnectionState.Connecting;
            await Task.Delay(1000, cancellationToken);
            ConnectionState = ConnectionState.Authenticating;
            await Task.Delay(500, cancellationToken);
            ConnectionState = ConnectionState.Connected;
        }

        public async Task DisconnectAsync(CancellationToken cancellationToken)
        {
            if (ConnectionState == ConnectionState.Disconnected || ConnectionState == ConnectionState.Disconnecting)
                return;
            ConnectionState = ConnectionState.Disconnecting;
            await Task.Delay(500, cancellationToken);
            ConnectionState = ConnectionState.Disconnected;
        }
    }
}