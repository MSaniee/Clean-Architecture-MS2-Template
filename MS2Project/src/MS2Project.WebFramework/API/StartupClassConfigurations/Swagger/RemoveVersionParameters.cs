﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace $ext_safeprojectname$.WebFramework.API.StartupClassConfigurations.Swagger;

public class RemoveVersionParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Remove version parameter from all Operations
        var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");
        if (versionParameter != null)
            operation.Parameters.Remove(versionParameter);
    }
}

