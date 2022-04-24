using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreFirstDB.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreFirstDB.Task
{
    public class Task_05
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 5. Предоставить возможность добавления факта продажи " + Environment.NewLine +
                "на указанном аукционе заданного предмета.");
            Console.WriteLine("----------------------------------------------------------------");

            var data2 = db.Auctions
                .Select(s => new
                {
                    auction = s,
                    items = s.Lots.Select(x => x.Item)
                });

            Console.WriteLine("Предметы:");
            foreach (var auction in data2)
            {
                foreach (var item in auction.items)
                {
                    Console.WriteLine($"{item.Id}. {item.Name} на аукционе {auction.auction.Name}");
                }
            }

            Console.Write("Я выбираю №");
            int itemId = Utils.ParseId(db.Items.Max(x => x.Id));
            //TODO: Добавить проверку на несуществующие айди
            
            Console.Write("Имя покупателя: ");
            string ownerNameNew = Console.ReadLine().Trim();

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
            var targetLot = db.Lots.OrderBy(y => y.Id).LastOrDefault(x => x.ItemId == itemId);

            //Устанавливаем новую сумму покупки
            decimal? priceWas = targetLot.BuyPrice;
            targetLot.BuyPrice = priceNew;

            //Устанавливаем нового покупателя
            var ownerWas = db.Items
                .Where(x => x.Id == itemId)
                .Select(y => y.Applicant)
                .FirstOrDefault();
            var targetItem = db.Items.Find(itemId);

            var newOwner = db.Applicants.Where(x => x.Name == ownerNameNew.Trim()).FirstOrDefault();
            if (newOwner == null)
            {
                targetItem.Applicant = new Applicant
                {
                    Name = ownerNameNew
                };
            }
            else
            {
                targetItem.Applicant = newOwner;
            }

            targetLot.Customer = targetItem.Applicant;
            db.SaveChanges();

            Console.WriteLine($"Владелец: {targetItem.Applicant.Name}\tБыло: {ownerWas.Name}" + Environment.NewLine +
                $"Сумма покупки: {targetLot.BuyPrice}\tБыло: {priceWas}");

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
