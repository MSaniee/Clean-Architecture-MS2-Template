using Microsoft.AspNetCore.Mvc;
using MS2Project.Application.Bases.ServiceResult;
using MS2Project.Common.EnumTools;
using MS2Project.Domain.Core.Enums;
using Newtonsoft.Json;

namespace MS2Project.WebFramework.API.Bases;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public ResultStatus StatusCode { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    public ApiResult(bool isSuccess, ResultStatus statusCode, string message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }

    #region Implicit Operators

    public static implicit operator ApiResult(OkResult result)
    {
        return new ApiResult(true, ResultStatus.Success);
    }

    public static implicit operator ApiResult(BadRequestResult result)
    {
        return new ApiResult(false, ResultStatus.BadRequest);
    }

    public static implicit operator ApiResult(BadRequestObjectResult result)
    {
        var message = result.Value.ToString();
        if (result.Value is SerializableError errors)
        {
            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = string.Join(" | ", errorMessages);
        }
        return new ApiResult(false, ResultStatus.BadRequest, message);
    }

    public static implicit operator ApiResult(ContentResult result)
    {
        return new ApiResult(true, ResultStatus.Success, result.Content);
    }

    public static implicit operator ApiResult(NotFoundResult result)
    {
        return new ApiResult(false, ResultStatus.NotFound);
    }

    public static implicit operator ApiResult(SResult result)
    {
        return new ApiResult(result.IsSuccess, result.StatusCode, result.Message);
    }


    #endregion Implicit Operators
}

public class ApiResult<TData> : ApiResult where TData : class
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public TData Data { get; set; }

    public ApiResult(bool isSuccess, ResultStatus statusCode, TData data, string message = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }

    #region Implicit Operators

    public static implicit operator ApiResult<TData>(TData data)
    {
        return new ApiResult<TData>(true, ResultStatus.Success, data);
    }

    public static implicit operator ApiResult<TData>(SResult<TData> result)
    {
        return new ApiResult<TData>(result.IsSuccess,
                                     result.StatusCode,
                                     result.Data,
                                     result.Message);
    }


    public static implicit operator ApiResult<TData>(OkResult result)
    {
        return new ApiResult<TData>(true, ResultStatus.Success, null);
    }

    public static implicit operator ApiResult<TData>(OkObjectResult result)
    {
        return new ApiResult<TData>(true, ResultStatus.Success, (TData)result.Value);
    }

    public static implicit operator ApiResult<TData>(BadRequestResult result)
    {
        return new ApiResult<TData>(false, ResultStatus.BadRequest, null);
    }

    public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
    {
        var message = result.Value.ToString();
        if (result.Value is SerializableError errors)
        {
            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = string.Join(" | ", errorMessages);
        }
        return new ApiResult<TData>(false, ResultStatus.BadRequest, null, message);
    }

    public static implicit operator ApiResult<TData>(ContentResult result)
    {
        return new ApiResult<TData>(true, ResultStatus.Success, null, result.Content);
    }

    public static implicit operator ApiResult<TData>(NotFoundResult result)
    {
        return new ApiResult<TData>(false, ResultStatus.NotFound, null);
    }

    public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
    {
        return new ApiResult<TData>(false, ResultStatus.NotFound, (TData)result.Value);
    }

    #endregion Implicit Operators
}
