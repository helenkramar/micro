using System;
using System.IO;

using consumer.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace consumer.Service
{
    public class ProviderService
    {
        private const string AppSettings = "appsettings.json";

        public static Provider GetProvider(string value)
        {
            var envFile = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSettings));
            return JsonConvert.DeserializeObject<JObject>(envFile).GetValue(value).ToObject<Provider>();
        }
    }
}