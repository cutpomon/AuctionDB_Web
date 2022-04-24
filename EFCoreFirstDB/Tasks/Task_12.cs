using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Task
{
    class Task_12
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 12. Вывести список покупателей с указанием количества приобретенных предметов в указанный период времени.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Введите дату ОТ: ");
            DateTime dateStart = Utils.ParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.ParseDate();

            #region Old
            //var data = from auction in db.Auctions
            //           join lot in db.Lots on auction.Id equals lot.AuctionId
            //           join item in db.Items on lot.ItemId equals item.Id
            //           where auction.Date >= dateStart && auction.Date <= dateEnd
            //           select new
            //           {
            //               item.Applicant,
            //               item
            //           };
            //var result = data.GroupBy(a => a.Applicant.Name)
            //                 .Select(x => new
            //                 {
            //                     CustomerName = x.Key,
            //                     ItemCnt = x.Where(y => y.Applicant.Name == x.Key).Count()
            //                 });

            //foreach (var customer in result)
            //{
            //    Console.WriteLine($"Покупатель: {customer.CustomerName}{Environment.NewLine}" +
            //        $"Кол-во предметов: {customer.ItemCnt}");
            //    Console.WriteLine();
            //}
            #endregion

            var data2 = db.Lots
                .Include(x => x.Auction)
                .Include(x => x.Customer)
                .Where(x => x.Auction.Date >= dateStart && x.Auction.Date <= dateEnd && x.Customer != null)
                .ToList()  //Без этого не работает GroupBy
                .GroupBy(a => a.Customer)
                .Select(x => new
                {
                    CustomerName = x.Key.Name,
                    ItemCnt = x.Where(y => y.Customer == x.Key).Count()
                });

            foreach (var customer in data2)
            {
                Console.WriteLine($"Покупатель: {customer.CustomerName}{Environment.NewLine}" +
                    $"Кол-во предметов: {customer.ItemCnt}");
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
