using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    /// <summary>
    /// Ставки
    /// </summary>
    public class Bid
    {
        public int Id { get; set; }
        public int LotId { get; set; }
        public int ApplicantId { get; set; }
        public decimal BidPrice { get; set; }

        public Lot Lot { get; set; }
        public Applicant Applicant { get; set; }
    }
}
