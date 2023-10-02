using CustomerApiService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Infrastracture.Persistence
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        private readonly string customerContainer = Environment.GetEnvironmentVariable("CustomerContainer");
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultContainer(customerContainer);

            builder.Entity<Customer>()
           .ToContainer(customerContainer)
           .HasPartitionKey(c => c.Id)
           .HasNoDiscriminator();

        }
    }
}
