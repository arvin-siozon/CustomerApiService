using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using CustomerApiService;
using CustomerChangefeedService.IOC;

[assembly: FunctionsStartup(typeof(Startup))]
//[assembly: WebJobsStartup(typeof(Startup))]
namespace CustomerApiService
{
    public class Startup : FunctionsStartup
    {
        private readonly ServiceCollectionExtensions serviceCollections = new ServiceCollectionExtensions();
        public override void Configure(IFunctionsHostBuilder builder)
        {
            serviceCollections.AddCore(builder.Services);

        }
    }

}
