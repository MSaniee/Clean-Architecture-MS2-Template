using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using $ext_safeprojectname$.Common.SwaggerTools;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace $ext_safeprojectname$.WebFramework.API.StartupClassConfigurations.Swagger;

public class IgnorePropertyFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription == null || operation.Parameters == null)
            return;

        if (!context.ApiDescription.ParameterDescriptions.Any())
            return;


        var excludedProperties = context.ApiDescription.ParameterDescriptions.Where(p =>
            p.Source.Equals(BindingSource.Form));

        if (excludedProperties.Any())
        {

            foreach (var excludedPropertie in excludedProperties)
            {
                foreach (var customAttribute in excludedPropertie.CustomAttributes())
                {
                    if (customAttribute.GetType() == typeof(SwaggerIgnoreAttribute))
                    {
                        for (int i = 0; i < operation.RequestBody.Content.Values.Count; i++)
                        {
                            for (int j = 0; j < operation.RequestBody.Content.Values.ElementAt(i).Encoding.Count; j++)
                            {
                                if (operation.RequestBody.Content.Values.ElementAt(i).Encoding.ElementAt(j).Key ==
                                    excludedPropertie.Name)
                                {
                                    operation.RequestBody.Content.Values.ElementAt(i).Encoding
                                        .Remove(operation.RequestBody.Content.Values.ElementAt(i).Encoding
                                            .ElementAt(j));
                                    operation.RequestBody.Content.Values.ElementAt(i).Schema.Properties.Remove(excludedPropertie.Name);


                                }
                            }
                        }

                    }
                }
            }

        }
    }
}
