using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using CustomerApiService;
[assembly: FunctionsStartup(typeof(Startup))]
//[assembly: WebJobsStartup(typeof(Startup))]
namespace CustomerApiService
{
    public class Startup : FunctionsStartup
    {
        private readonly string customerDBConnectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
        private readonly string customerDbName = Environment.GetEnvironmentVariable("CosmoDbName");
        private readonly IOC.ServiceCollectionExtensions serviceCollections = new IOC.ServiceCollectionExtensions();
       public override void Configure(IFunctionsHostBuilder builder)
        {
          
            
            serviceCollections.AddCore(builder.Services);

        }
    }

}
