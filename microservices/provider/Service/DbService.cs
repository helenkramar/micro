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

        public static IEnumerable<Item> GetItems()
        {
            var envFile = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<JArray>(envFile).ToObject<IEnumerable<Item>>();
        }

        public static void PutItems(IEnumerable<Item> items)
        {
            string content = JsonConvert.SerializeObject(items);
            File.WriteAllText(path, content);
        }
    }
}