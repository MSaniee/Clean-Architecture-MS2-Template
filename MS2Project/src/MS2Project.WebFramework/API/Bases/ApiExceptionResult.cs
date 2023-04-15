using $ext_safeprojectname$.Common.EnumTools;
using $ext_safeprojectname$.Domain.Core.Enums;
using $ext_safeprojectname$.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.WebFramework.API.Bases
{
    public class ApiExceptionResult
    {
        public ApiExceptionResult(bool isSuccess, ResultStatus statusCode, string message = null, string stackTrace = null)
        {
            this.isSuccess = isSuccess;
            this.statusCode = statusCode;
            this.message = message ?? statusCode.ToDisplay();
            this.stackTrace = stackTrace ?? "";
        }

        public bool isSuccess { get; set; }
        public ResultStatus statusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }

        public string stackTrace { get; set; }
    }
}
