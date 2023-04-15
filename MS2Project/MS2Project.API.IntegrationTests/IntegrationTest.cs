using Microsoft.EntityFrameworkCore;
using $ext_safeprojectname$.Domain.UserAggregate;
using $ext_safeprojectname$.Infrastructure.Data.SqlServer.EfCore.Context;
using System.Net.Http.Headers;

namespace $ext_safeprojectname$.API.IntegrationTests;

[TestCaseOrderer("$ext_safeprojectname$.API.IntegrationTests", "$ext_safeprojectname$.API.IntegrationTests")]
public class IntegrationTest
{
    private readonly DbContextOptions<ApplicationDbContext> _optionsDb;
    private readonly IServiceProvider _serviceProvider;

    protected readonly CustomWebApplicationFactory<Program> appFactory;

    protected readonly HttpClient testClient;
    protected readonly ApplicationDbContext dbContext;

    protected string version = "1";
    protected string area = "Common";
    protected string controller = "Account";
    protected string subFolderUrl = null;

    protected ApplicationDbContext DbContext => new(_optionsDb);
    protected IServiceProvider ServiceProvider => _serviceProvider;

    protected IntegrationTest(CustomWebApplicationFactory<Program> webApplicationFactory)
    {
        appFactory = webApplicationFactory;
        testClient = appFactory.CreateClient();
        dbContext = appFactory.DbContext;

        if (!webApplicationFactory.isDataInitialized)
        {
            //TestDataInitializer.InitializeData(dbContext);
            webApplicationFactory.isDataInitialized = true;
        }

        _serviceProvider = appFactory.Services;

        _optionsDb = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseSqlServer("Data Source=.;Initial Catalog=TestDb;Integrated Security=true")
                      .Options;
    }



    protected async Task<User> UserAuthenticateAsync(User user)
    {
        testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetUserJwtAsync(user));

        return user;
    }


    private async Task<string> GetUserJwtAsync(User user)
    {
        //if (!TestItem.listNumber.Contains(user.PhoneNumber)) TestItem.listNumber.Add(user.PhoneNumber);

        //HttpResponseMessage response1 = await testClient.PostAsJsonAsync("api/v1/Users/Account/SendActivationCode", new SendActivationCodeDto
        //{
        //    PhoneNumber = user.PhoneNumber,
        //});

        //if (response1.IsSuccessStatusCode)
        //{
        //    var response2 = await testClient.PostAsJsonAsync("api/v1/Users/Account/Login", new UserLoginDto
        //    {
        //        PhoneNumber = user.PhoneNumber,
        //        PhoneNumberCode = user.PhoneNumberCode,
        //        Code = 1234
        //    });

        //    var result = await response2.Content.ReadAsAsync<ApiResult<UserLoginResultDtoResponse>>();
        //    return result.Data.Tokens.access_token.value;
        //}

        return null;
    }



    protected string GetUrl(string webServiceName, params string[] values)
    {
        subFolderUrl = subFolderUrl is not null && !subFolderUrl.Contains('/') ? subFolderUrl + "/" : subFolderUrl;
        webServiceName = webServiceName is not null ? "/" + webServiceName : null;

        string url = "api/" + "v" + version + "/" + area + "/" + subFolderUrl + controller + webServiceName;

        if (values.Length != 0)
        {
            url += "?";

            for (int i = 0; i < values.Length; i += 2)
            {
                url = url + values[i] + "=" + values[i + 1] + "&";
            }
        }

        return url;
    }
}
