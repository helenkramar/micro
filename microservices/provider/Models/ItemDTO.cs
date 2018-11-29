using Newtonsoft.Json;
using System;

namespace provider.Models
{
    public class ItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}
