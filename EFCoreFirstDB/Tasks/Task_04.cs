using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreFirstDB.Task
{
    public class Task_04
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 4. Для указанного интервала дат, вывести список предметов, " + Environment.NewLine +
                "которые были проданы на аукционах в этот период времени.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.Write("Введите дату ОТ: ");
            DateTime dateStart = Utils.ParseDate();

            Console.Write("Введите дату ДО: ");
            DateTime dateEnd = Utils.ParseDate();

            #region Old
            //var result = from auction in db.Auctions
            //             join lot in db.Lots on auction.Id equals lot.AuctionId
            //             where auction.Date >= dateStart && auction.Date <= dateEnd && lot.BuyPrice != null
            //             select new
            //             {
            //                 auction,
            //                 lot.Item.Name
            //             };

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"\t{item.Name}");
            //}
            #endregion

            var test = db.Auctions
                .Where(a => a.Date >= dateStart && a.Date <= dateEnd)
                .Select(s => new
                {
                    auction = s,
                    itemNames = s.Lots.Where(l => l.BuyPrice != null).Select(l => l.Item.Name)
                });

            foreach (var auc in test)
            {
                foreach (var itemName in auc.itemNames)
                {
                    Console.WriteLine($"\t{itemName}");
                }
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
