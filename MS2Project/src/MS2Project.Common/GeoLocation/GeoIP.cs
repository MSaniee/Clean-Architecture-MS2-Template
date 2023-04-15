using MaxMind.GeoIP2;
using System.IO;

namespace $ext_safeprojectname$.Common.GeoLocation
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
