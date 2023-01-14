using System;

namespace MS2Project.Common.SwaggerTools;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SwaggerIgnoreAttribute : Attribute
{
}

