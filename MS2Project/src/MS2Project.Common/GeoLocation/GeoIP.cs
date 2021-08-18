using MaxMind.GeoIP2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS2Project.Common.GeoLocation
{
    public class GeoIP
    {
        public static string GetCountryNameByIp(string ip)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "\\GeoLite2-City.mmdb";
                using var reader = new DatabaseReader(path);

                return reader.City(ip).Country.Name;
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
