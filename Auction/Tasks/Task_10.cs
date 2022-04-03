using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB.Task
{
    class Task_10
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 10. Для указанного интервала дат вывести список продавцов, " + Environment.NewLine +
                "которые принимали участие в аукционах, проводимых в этот период времени.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Введите дату ОТ: ");
            DateTime dateStart = Utils.TryParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.TryParseDate();

            var data = from auction in db.Auctions
                       join lot in db.Lots on auction.Id equals lot.AuctionId
                       join item in db.Items on lot.ItemId equals item.Id
                       where auction.Date >= dateStart && auction.Date <= dateEnd
                       select item.Seller;

            foreach (var seller in data.Distinct())
            {
                Console.WriteLine($"\t{seller.Name}");
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
