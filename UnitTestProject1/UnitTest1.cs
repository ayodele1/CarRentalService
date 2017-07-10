using System;
using System.Linq;
using Xunit;

namespace UnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            //This specifies the following
            //1. Web Application Root
            //2. Testing Environment
            //3. Configuration File to Use(StartUp.cs)
            //Specify Web application testing environment.
            //var builder = new WebHostBuilder()
            //    .UseContentRoot(@"C:\www\VisualStudioProjects\CarRentalApplication\src\CarRentalApplication")
            //    .UseEnvironment("Development")
            //    .UseStartup<Startup>();

            ////Test Server for Hosting the Web Application
            //var server = new TestServer(builder);

            ////Http Client for initiating Http Requests.
            //var client = server.CreateClient();

            //var response = await client.GetAsync("/login");

            //response.EnsureSuccessStatusCode();

            //var responseString = await response.Content.ReadAsStringAsync();

            //Assert.Contains("Login", responseString);
        }
    }
}
