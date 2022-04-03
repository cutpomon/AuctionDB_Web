using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    public class Lot
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public decimal StartPrice { get; set; } = default(decimal);
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}
