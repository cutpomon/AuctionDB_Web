using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    /// <summary>
    /// Участник аукциона
    /// </summary>
    public class Applicant
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Bid> Bid { get; set; } = new List<Bid>();
        public ICollection<Item> Item { get; set; } = new List<Item>();
    }
}
