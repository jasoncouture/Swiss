using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Swiss.Core;

namespace Swiss.FakeServer
{
    public class TestServerFactory : IChatServerFactory
    {
        private readonly IWindsorContainer _container;

        public TestServerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        private readonly ConcurrentDictionary<string, IChatServer> _chatServerInstances = new ConcurrentDictionary<string, IChatServer>(StringComparer.InvariantCultureIgnoreCase);

        public IChatServer CreateServerInstance(string key)
        {
            return _chatServerInstances.GetOrAdd(key, GetChatServerInstanceFromDependencyFramework);
        }

        private IChatServer GetChatServerInstanceFromDependencyFramework(string arg)
        {
            return _container.Resolve<TestServer>(new { name = arg });
        }
    }

    public class TestServerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<TestServer>().ImplementedBy<TestServer>().LifestyleTransient(),
                Component.For<IChatServerFactory>().ImplementedBy<TestServerFactory>().LifestyleSingleton()
                );
        }
    }

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
