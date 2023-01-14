using MS2Project.WebFramework.API.Bases;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;

namespace MS2Project.API.IntegrationTests.ControllersTests.Areas.Customers;

[Collection("MS2ProjectAPI - Full Integration Test #1")]
public class CustomersControllerTest : IntegrationTest
{
    public CustomersControllerTest(CustomWebApplicationFactory<Program> webApplicationFactory)
        : base(webApplicationFactory)
    {
        version = "1";
        area = "Customers";
        controller = "Customers";
    }

    [Fact]
    public async Task TestWebservice_WhenCall_ReturnIsSuccess()
    {
        //Arrange

        //Act
        var response = await testClient.PostAsync(GetUrl("TestWebservice"),null);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadAsAsync<ApiResult>();

        result.IsSuccess.Should().BeTrue();
    }

}

