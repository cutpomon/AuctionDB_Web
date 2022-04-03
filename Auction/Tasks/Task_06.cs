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
            DateTime dateStart = Utils.TryParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.TryParseDate();

            var data = from auction in db.Auctions
                       join lot in db.Lots on auction.Id equals lot.AuctionId
                       join item in db.Items on lot.ItemId equals item.Id
                       join seller in db.Sellers on item.SellerId equals seller.Id
                       where auction.Date >= dateStart && auction.Date <= dateEnd
                       select new
                       {
                           seller,
                           item
                       };
            var result = data.GroupBy(a => a.seller.Name)
                             .Select(x => new
                             {
                                 SellerName = x.Key,
                                 ItemSum = x.Sum(i => i.item.BuyPrice)
                             })
                             .OrderByDescending(x => x.ItemSum);

            foreach (var item in result)
            {
                Console.WriteLine($"Продавец: {item.SellerName}{Environment.NewLine}Сумма выручки: {item.ItemSum}");
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
