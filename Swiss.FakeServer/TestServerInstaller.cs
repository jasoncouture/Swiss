using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Swiss.Core;

namespace Swiss.FakeServer
{
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
}