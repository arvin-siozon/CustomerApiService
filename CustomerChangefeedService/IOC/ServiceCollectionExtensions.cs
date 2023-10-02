using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Reflection;

namespace CustomerChangefeedService.IOC
{
    public class ServiceCollectionExtensions
    {
        private readonly static string customerDBConnectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
        private readonly static string customerDbName = Environment.GetEnvironmentVariable("CosmoDbName");


        public void AddCore(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

    }
}
