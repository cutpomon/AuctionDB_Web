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
            string place;
            while ((place = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
            }

            Console.Write("Дата: ");
            DateTime date = Utils.ParseDate();

            Console.Write("Название: ");
            string name;
            while ((name = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
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
