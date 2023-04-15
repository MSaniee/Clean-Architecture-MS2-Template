using $ext_safeprojectname$.Application.Dtos;

namespace $ext_safeprojectname$.Infrastructure.Processing;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(BaseDto<,>).Assembly;
}
