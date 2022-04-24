using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Models
{
    /// <summary>
    /// Предметы
    /// </summary>
    public class Item
    {
        public int Id { get; set; }
        //Текущий владелец предмета
        public int ApplicantId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Author { get; set; }
        public int ItemTypeId { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }

        public Applicant Applicant { get; set; }
        public ItemType ItemType { get; set; }
    }
}
