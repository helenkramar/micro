using System;

namespace provider.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}
