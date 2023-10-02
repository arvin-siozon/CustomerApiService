using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CustomerApiService.Infrastracture.Persistence;
using System;
using CustomerApiService.Contracts;
using CustomerApiService.Infrastracture.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using CustomerApiService.Command.CreateCustomer;
using CustomerApiService.Models;
using CustomerApiService.Command.UpdateCustomer;

namespace CustomerApiService.IOC
{
    public class ServiceCollectionExtensions
    {
        private readonly static string customerDBConnectionString = Environment.GetEnvironmentVariable("CosmosConnectionString");
        private readonly static string customerDbName = Environment.GetEnvironmentVariable("CosmoDbName");


        public void AddCore(IServiceCollection services)
        {
    
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddScoped<IValidator<CustomerCreateModel>, CreateCustomerModelValidator>();
            services.AddScoped<IValidator<CustomerUpdateModel>, UpdateCustomerModelValidator>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
           
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDbContext<CustomerContext>(options => options.UseCosmos(customerDBConnectionString, customerDbName), ServiceLifetime.Transient);
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();

          //  services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        }

    }
}
