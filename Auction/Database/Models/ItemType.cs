using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    public class ItemType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
