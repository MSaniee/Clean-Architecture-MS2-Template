using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using $ext_safeprojectname$.Domain.Core.Enums;
using $ext_safeprojectname$.WebFramework.API.Bases;

namespace $ext_safeprojectname$.WebFramework.API.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<object>(true, ResultStatus.Success, okObjectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(true, ResultStatus.Success);
                context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new ApiResult(false, ResultStatus.BadRequest);
                context.Result = new JsonResult(apiResult) { StatusCode = badRequestResult.StatusCode };
            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var message = "";
                if (context.ModelState.Values.ToList()[0].Errors.Count != 0)
                    message = context.ModelState.Values.ToList()[0].Errors[0].ErrorMessage;

                if (badRequestObjectResult.Value is SerializableError errors)
                {
                    var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                    message = string.Join(" | ", errorMessages);
                }

                if (badRequestObjectResult.Value is ValidationProblemDetails validationProblemDetails)
                {
                    var errorMessages = validationProblemDetails.Errors.SelectMany(p => (string[])p.Value).Distinct();
                    message = string.Join(" | ", errorMessages);
                }

                var apiResult = new ApiResult(false, ResultStatus.BadRequest, message);
                context.Result = new JsonResult(apiResult) { StatusCode = 200 }; //badRequestObjectResult.StatusCode };
            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult(true, ResultStatus.Success, contentResult.Content);
                context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new ApiResult(false, ResultStatus.NotFound);
                context.Result = new JsonResult(apiResult) { StatusCode = notFoundResult.StatusCode };
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new ApiResult<object>(false, ResultStatus.NotFound, notFoundObjectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                && !(objectResult.Value is ApiResult))
            {
                var apiResult = new ApiResult<object>(true, ResultStatus.Success, objectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
            }

            base.OnResultExecuting(context);
        }
    }
}
