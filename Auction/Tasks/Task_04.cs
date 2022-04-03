using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Task
{
    public class Task_04
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 4. Для указанного интервала дат, вывести список предметов, " + Environment.NewLine +
                "которые были проданы на аукционах в этот период времени.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Введите дату ОТ: ");
            DateTime dateStart = Utils.TryParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.TryParseDate();

            var data = from auction in db.Auctions
                       join lot in db.Lots on auction.Id equals lot.AuctionId
                       join item in db.Items on lot.ItemId equals item.Id
                       where auction.Date >= dateStart && auction.Date <= dateEnd &&
                       item.CustomerId != null
                       select new
                       {    
                           auction,
                           item
                       };

            foreach (var item in data)
            {
                Console.WriteLine($"\t{item.item.Name}");
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
