using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    /// <summary>
    /// Типы предметов
    /// </summary>
    public class ItemType
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Type { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
