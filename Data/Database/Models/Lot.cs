using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    /// <summary>
    /// Лоты
    /// </summary>
    public class Lot
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public int SellerId { get; set; }
        public int? CustomerId { get; set; }
        public int ItemId { get; set; }
        public decimal StartPrice { get; set; } = default(decimal);
        public decimal? BuyPrice { get; set; }

        public Auction Auction { get; set; }
        public Applicant Seller { get; set; }
        public Applicant Customer { get; set; }
        public Item Item { get; set; }
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}
