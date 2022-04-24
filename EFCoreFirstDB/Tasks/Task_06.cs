using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Task
{
    public class Task_06
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 6. Для указанного интервала дат вывести список продавцов " + Environment.NewLine +
                "с указанием общей суммы, полученной от продажи предметов в этот промежуток времени.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Введите дату ОТ: ");
            DateTime dateStart = Utils.ParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.ParseDate();

            #region Old
            //var data = from auction in db.Auctions
            //           join lot in db.Lots on auction.Id equals lot.AuctionId
            //           where auction.Date >= dateStart && auction.Date <= dateEnd
            //           select new
            //           {
            //               SellerName = lot.Seller.Name,
            //               BuyPrice = lot.BuyPrice
            //           };
            //var result = data.GroupBy(a => a.SellerName)
            //                 .Select(x => new
            //                 {
            //                     SellerName = x.Key,
            //                     ItemSum = x.Sum(i => i.BuyPrice)
            //                 })
            //                 .OrderByDescending(x => x.ItemSum);

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"Продавец: {item.SellerName}{Environment.NewLine}Сумма выручки: {item.ItemSum}");
            //    Console.WriteLine();
            //}
            #endregion

            var res = db.Lots
                    .Include(x => x.Auction)
                    .Include(x => x.Seller)
                    .Where(x => x.Auction.Date >= dateStart && x.Auction.Date <= dateEnd)
                    .ToList() //Без этого не работает GroupBy
                    .GroupBy(a => a.Seller)
                    .Select(x => new
                    {
                        SellerName = x.Key.Name,
                        ItemSum = x.Sum(i => i.BuyPrice)
                    })
                    .OrderByDescending(x => x.ItemSum);

            foreach (var item in res)
            {
                Console.WriteLine($"Продавец: {item.SellerName}{Environment.NewLine}Сумма выручки: {item.ItemSum}");
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
