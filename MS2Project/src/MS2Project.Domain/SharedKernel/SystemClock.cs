using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS2Project.Domain.SharedKernel;

public static class SystemClock
{
    private static DateTime? _customDate;

    public static DateTime Now
    {
        get
        {
            if (_customDate.HasValue)
            {
                return _customDate.Value;
            }

            return DateTime.Now;
        }
    }

    public static void Set(DateTime customDate) => _customDate = customDate;

    public static void Reset() => _customDate = null;
}

