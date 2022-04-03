using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreFirstDB.Models;

namespace EFCoreFirstDB.Task
{
    public class Task_05
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 5. Предоставить возможность добавления факта продажи " + Environment.NewLine +
                "на указанном аукционе заданного предмета.");
            Console.WriteLine("----------------------------------------------------------------");

            var data = from auction in db.Auctions
                       join lot in db.Lots on auction.Id equals lot.AuctionId
                       join item in db.Items on lot.ItemId equals item.Id
                       select new
                       {
                           auction,
                           item
                       };

            Console.WriteLine("Предметы:");
            foreach (var item in data)
            {
                Console.WriteLine($"{item.item.Id}. {item.item.Name} на аукционе {item.auction.Name}");
            }

            int itemId;
            Console.Write("Я выбираю №");
            while (true)
            {
                try
                {
                    itemId = int.Parse(Console.ReadLine().Trim());
                    if (itemId <= 0 || itemId > db.Items.Max(x => x.Id))
                    {
                        throw new NotImplementedException();
                    }
                    break;
                }
                catch
                {
                    Console.Write("Повторите ввод: ");
                    continue;
                }
            }

            Console.Write("Имя покупателя: ");
            string customerNew = Console.ReadLine().Trim();
            string customerWas = db.Items.Where(x => x.Id == itemId).Select(x => x.Customer.Name).FirstOrDefault();

            decimal priceNew;
            Console.Write("Стоимость покупки: ");
            while (true)
            {
                try
                {
                    priceNew = decimal.Parse(Console.ReadLine(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
                    break;
                }
                catch
                {
                    Console.Write("Повторите ввод: ");
                    continue;
                }
            }
            decimal? priceWas = db.Items.Where(x => x.Id == itemId).Select(x => x.BuyPrice).FirstOrDefault();

            var targetItem = db.Items.Find(itemId);

            //Устанавливаем новую сумму покупки
            targetItem.BuyPrice = priceNew;

            //Устанавливаем нового покупателя
            if (!db.Items.Any(x => x.Customer.Name == customerNew.Trim()))
            {
                targetItem.Customer = new Customer
                {
                    Name = customerNew
                };
            }
            else
            {
                int customerId = db.Customers.Where(x => x.Name == customerNew).Select(x => x.Id).FirstOrDefault();
                targetItem.CustomerId = customerId;
            }
            db.SaveChanges();

            Console.WriteLine($"Покупатель: {targetItem.Customer.Name}\tБыло: {customerWas}" + Environment.NewLine +
                $"Сумма покупки: {targetItem.BuyPrice}\tБыло: {priceWas}");

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
