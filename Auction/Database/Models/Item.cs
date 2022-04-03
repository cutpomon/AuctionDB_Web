using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }
        public ICollection<ItemProperty> ItemProperties { get; set; } = new List<ItemProperty>();
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public decimal? BuyPrice { get; set; }
    }
}
