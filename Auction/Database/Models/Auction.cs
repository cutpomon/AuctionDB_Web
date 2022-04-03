using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Lot> Lots { get; set; } = new List<Lot>();

    }
}
