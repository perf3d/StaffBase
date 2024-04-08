using Newtonsoft.Json;
using StaffInterface.ApplicationLayer.Contracts;
using System.IO;

namespace StaffInterface.ApplicationLayer
{
    public class AppSettingsProvider
    {
        public AppSettings? appSettings { get; }
        public AppSettingsProvider()
        {
            string settings = string.Empty;
            using (var reader = new StreamReader("AppSettings.json"))
            {
                settings = reader.ReadToEnd();
            }
            appSettings = JsonConvert.DeserializeObject<AppSettings>(settings);
        }
    }
}
