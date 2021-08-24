using System;
using System.IO;

namespace MS2Project.Common.JsonTools
{
    public class SettingsJsonConfiguer
    {
        public static void AddOrUpdate<T>(string key, T value)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "AdminPanelsettings.json");
            string json = File.ReadAllText(filePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            var sectionPath = key.Split(":")[0];
            if (!string.IsNullOrEmpty(sectionPath))
            {
                var keyPath = key.Split(":")[1];
                jsonObj[sectionPath][keyPath] = value;
            }
            else
            {
                jsonObj[sectionPath] = value; // if no sectionpath just set the value
            }
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
        }

        public static string GetValue(string key)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "AdminPanelsettings.json");
            string json = File.ReadAllText(filePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            string value = "";

            var sectionPath = key.Split(":")[0];
            if (!string.IsNullOrEmpty(sectionPath))
            {
                var keyPath = key.Split(":")[1];
                value = Convert.ToString(jsonObj[sectionPath][keyPath]);
            }
            else
            {
                value = "Not Found!";
            }

            return value;
        }
    }
}