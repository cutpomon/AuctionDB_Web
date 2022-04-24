using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Task
{
    public class Task_09
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 9. Для указанного места, вывести список аукционов.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Место: ");
            string place;
            while ((place = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
            }

            var result = db.Auctions.Where(x => x.Place == place);

            foreach (var auct in result)
            {
                Console.WriteLine($"{Environment.NewLine}Название: {auct.Name} {Environment.NewLine}" +
                    $"Дата: {auct.Date} {Environment.NewLine}" +
                    $"Место: {auct.Place}");
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
