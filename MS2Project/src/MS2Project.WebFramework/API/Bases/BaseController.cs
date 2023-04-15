using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using $ext_safeprojectname$.WebFramework.API.Filters;

namespace $ext_safeprojectname$.WebFramework.API.Bases;

[ApiController]
[Authorize]
[ApiResultFilter]
//[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[area]/[controller]/[action]")]
public class BaseController : ControllerBase
{
}

