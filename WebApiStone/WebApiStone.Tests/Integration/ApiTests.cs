using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebApiStone.Tests.Integration
{
    public class ApiTests
    {
        
        public ApiTests()
        {
        }

        [Fact]
        public async void Test_GetALl_Person()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            var response = await httpClient.GetAsync("/api/Person/GetAll");
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(stringResult);
        }
    }
}
