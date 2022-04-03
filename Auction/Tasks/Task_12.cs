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
            DateTime dateStart = Utils.TryParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.TryParseDate();

            var data = from auction in db.Auctions
                       join lot in db.Lots on auction.Id equals lot.AuctionId
                       join item in db.Items on lot.ItemId equals item.Id
                       where auction.Date >= dateStart && auction.Date <= dateEnd
                       select new
                       {
                           item.Customer,
                           item
                       };
            var result = data.GroupBy(a => a.Customer.Name)
                             .Select(x => new
                             {
                                 CustomerName = x.Key,
                                 ItemCnt = x.Where(y => y.Customer.Name == x.Key).Count()
                             });

            foreach (var customer in result)
            {
                Console.WriteLine($"Покупатель: {customer.CustomerName}{Environment.NewLine}" +
                    $"Кол-во предметов: {customer.ItemCnt}");
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
