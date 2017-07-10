using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using CarRentalApplication;
using NUnit.Framework;
using System.IO;

namespace CarRentalApplication.IntegrationTests
{
    [TestFixture]
    public class ApiControllerTests
    {
        [Test]
        public async Task RenderApplication()
        {
            //This specifies the following
            //1. Web Application Root
            //2. Testing Environment
            //3. Configuration File to Use(StartUp.cs)
            //Specify Web application testing environment.
            var builder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            //Test Server for Hosting the Web Application
            var server = new TestServer(builder);

            //Http Client for initiating Http Requests.
            var client = server.CreateClient();

            var response = await client.GetAsync("/login");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(responseString);
        }
    }
}
