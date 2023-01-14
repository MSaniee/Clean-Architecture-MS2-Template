using System;

namespace MS2Project.Common.ExceptionTools;

public static class CheckNullArgument
{
    public static T ThrowIfNull<T>(this T argument)
    {
        ArgumentNullException.ThrowIfNull(argument);

        return argument;
    }
}

