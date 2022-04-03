using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreFirstDB.Models;

namespace EFCoreFirstDB.Task
{
    public class Task_08
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 8. Предоставить возможность добавления записи о проводимом аукционе(место, время).");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Место: ");
            string place = Console.ReadLine().Trim();
            while (place == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
                place = Console.ReadLine().Trim();
            }

            Console.Write("Дата (dd.MM.yyyy): ");
            DateTime date = Utils.TryParseDate();

            Console.Write("Название: ");
            string name = Console.ReadLine().Trim();
            while (name == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
                name = Console.ReadLine().Trim();
            }

            db.Auctions.Add(new Auction
            {
                Name = name,
                Place = place,
                Date = date
            });
            db.SaveChanges();

            Console.WriteLine("Запись успешно добавлена!");

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
