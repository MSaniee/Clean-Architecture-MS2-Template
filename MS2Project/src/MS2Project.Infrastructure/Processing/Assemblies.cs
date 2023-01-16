using MS2Project.Application.Dtos;

namespace MS2Project.Infrastructure.Processing;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(BaseDto<,>).Assembly;
}
