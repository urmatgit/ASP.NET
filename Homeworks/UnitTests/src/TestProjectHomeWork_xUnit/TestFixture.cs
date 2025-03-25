using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectHomeWork_xUnit
{
    public class TestFixture:IDisposable
    {
        public IServiceProvider ServiceProvider { get; set; }
        public TestFixture()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder.Build();
            var servicesCollection = new ServiceCollection();
            new Startup(configuration).ConfigureServices(servicesCollection);
            var serviceProvider = servicesCollection.BuildServiceProvider();
            ServiceProvider = serviceProvider;

        }

        public void Dispose()
        {
            
        }
    }
}
