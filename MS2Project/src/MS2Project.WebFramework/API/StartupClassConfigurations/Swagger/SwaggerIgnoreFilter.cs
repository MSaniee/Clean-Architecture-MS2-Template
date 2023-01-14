using Microsoft.OpenApi.Models;
using MS2Project.Common.SwaggerTools;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MS2Project.WebFramework.API.StartupClassConfigurations.Swagger;

public class SwaggerIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext schemaFilterContext)
    {
        if (schema.Properties.Count == 0)
            return;

        const BindingFlags bindingFlags = BindingFlags.Public |
                                          BindingFlags.NonPublic |
                                          BindingFlags.Instance;
        var memberList = schemaFilterContext.Type // In v5.3.3+ use Type instead
                            .GetFields(bindingFlags).Cast<MemberInfo>()
                            .Concat(schemaFilterContext.Type // In v5.3.3+ use Type instead
                            .GetProperties(bindingFlags));

        var excludedList = memberList.Where(m =>
                                            m.GetCustomAttribute<SwaggerIgnoreAttribute>()
                                            != null)
                                     .Select(m =>
                                         (m.GetCustomAttribute<JsonPropertyAttribute>()
                                          ?.PropertyName
                                          ?? m.Name.ToCamelCase()));

        foreach (var excludedName in excludedList)
        {
            if (schema.Properties.ContainsKey(excludedName))
                schema.Properties.Remove(excludedName);
        }
    }

}
