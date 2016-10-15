using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}
