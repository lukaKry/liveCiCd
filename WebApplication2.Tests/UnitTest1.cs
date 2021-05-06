using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Controllers;
using Xunit;
using static WebApplication2.Data;

namespace WebApplication2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task CustomerIntegrationTest()
        {
            //create db context
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new CustomerContext(optionsBuilder.Options);


            //delete all customers in database
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
           

            //create controller
            var controller = new CustomersController(context);

            //add customer
            await controller.Add(new Customer { CustomerName = "Foo BAR" });

            //check: does getall return the adde customer
            var result = (await controller.GetAll()).ToArray();
            Assert.Single(result);
            Assert.Equal("Foo BAR", result[0].CustomerName);
        }
    }
}
