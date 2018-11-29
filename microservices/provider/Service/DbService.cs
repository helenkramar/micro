using System;
using System.IO;
using System.Collections.Generic;

using provider.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace provider.Service
{
    public static class DbService
    {
        private const string db = @"db\items.json";
        static string path = $@"{Environment.CurrentDirectory}\{db}";

        public static IEnumerable<ItemDTO> ReadItems()
        {
            var envFile = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<JArray>(envFile).ToObject<IEnumerable<ItemDTO>>();
        }

        public static void WriteItems(IEnumerable<ItemDTO> items)
        {
            string content = JsonConvert.SerializeObject(items);
            File.WriteAllText(path, content);
        }
    }
}