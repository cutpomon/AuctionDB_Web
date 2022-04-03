using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Task
{
    public class Task_01
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 1. Для указанного интервала дат вывести список аукционов с " + Environment.NewLine +
                "указанием наименования, даты и места проведения.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Введите дату ОТ: ");
            var startDate = Utils.TryParseDate();

            Console.Write("Введите дату ДО: ");
            var finishDate = Utils.TryParseDate();

            var result = db.Auctions.Where(p => p.Date >= startDate)
                                    .Where(p => p.Date <= finishDate);

            foreach (var auction in result)
            {
                Console.WriteLine($"{Environment.NewLine}Название: {auction.Name} {Environment.NewLine}" +
                    $"Дата: {auction.Date} {Environment.NewLine}" +
                    $"Место: {auction.Place}");
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
