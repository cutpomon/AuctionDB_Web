using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Task
{
    public class Task_07
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 7. Вывести список покупателей, которые сделали приобретения в указанный интервал дат.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Введите дату ОТ: ");
            DateTime dateStart = Utils.ParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.ParseDate();

            #region Old
            //var data = from auction in db.Auctions
            //           join lot in db.Lots on auction.Id equals lot.AuctionId
            //           join item in db.Items on lot.ItemId equals item.Id
            //           where item.CustomerId != null
            //                && auction.Date >= dateStart && auction.Date <= dateEnd
            //           select item.Customer;

            //foreach (var customer in data.Distinct())
            //{
            //    Console.WriteLine($"\t{customer.Name}");
            //}
            #endregion

            var data2 = db.Lots
                .Include(x => x.Auction)
                .Where(a => a.Auction.Date >= dateStart && a.Auction.Date <= dateEnd)
                .Select(s => s.Customer.Name);

            foreach (var customerName in data2.Distinct())
            {
                Console.WriteLine($"\t{customerName}");
            }



            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
