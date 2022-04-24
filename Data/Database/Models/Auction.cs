using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    /// <summary>
    /// Аукцион
    /// </summary>
    public class Auction
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Place { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Lot> Lots { get; set; } = new List<Lot>();

    }
}
