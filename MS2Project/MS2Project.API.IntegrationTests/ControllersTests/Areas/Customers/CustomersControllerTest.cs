﻿using $ext_safeprojectname$.WebFramework.API.Bases;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;

namespace $ext_safeprojectname$.API.IntegrationTests.ControllersTests.Areas.Customers;

[Collection("$ext_safeprojectname$API - Full Integration Test #1")]
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

