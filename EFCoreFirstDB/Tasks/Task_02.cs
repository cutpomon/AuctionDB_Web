using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreFirstDB.Models;

namespace EFCoreFirstDB.Task
{
    public class Task_02
    {
        public static void Execute(AuctionContext db)
        {
            Console.WriteLine("Задание 2. Добавить на указанный пользователем аукцион на продажу" + Environment.NewLine +
                "предмет искусства с указанием начальной цены.");
            Console.WriteLine("----------------------------------------------------------------");

            Console.WriteLine("Список доступных аукционов:");

            var auctionList = db.Auctions.ToList();

            foreach (var auction in auctionList)
            {
                Console.WriteLine($"\t{auction.Id}. {auction.Name} проходящий в {auction.Place}");
            }
            Console.WriteLine();

            Console.Write("ID аукциона: ");
            int auctionId = Utils.ParseId(auctionList.Count);
            
            Console.Write("Имя продавца: ");
            string applicant;
            while ((applicant = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
            }
            Applicant applicantDB = db.Applicants.FirstOrDefault(s => s.Name.ToUpper() == applicant.ToUpper());
            if (applicantDB == null)
            {
                applicantDB = new Applicant { Name = applicant };
            }

            Console.Write("Название предмета: ");
            string itemName;
            while ((itemName = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
            }

            Console.Write("Автор/создатель предмета (optional): ");
            string author = Console.ReadLine().Trim();

            Console.Write("Тип предмета: ");
            string itemType;
            while ((itemType = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
            }
            ItemType itemTypeDB = db.ItemTypes.FirstOrDefault(s => s.Type == itemType);
            if (itemTypeDB == null)
            {
                itemTypeDB = new ItemType { Type = itemType };
            }

            Console.Write("Описание предмета (optional):");
            var description = Console.ReadLine().Trim();

            Console.Write("Дата создания (optional): ");
            DateTime? itemDate;
            try
            {
                itemDate = DateTime.Parse(Console.ReadLine().Trim());
            }
            catch
            {
                itemDate = null;
            }

            Console.Write("Начальная цена (optional): ");
            decimal startPrice;
            try
            {
                startPrice = decimal.Parse(Console.ReadLine(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
            }
            catch
            {
                startPrice = 0;
            }

            db.Add(new Lot
            {
                AuctionId = auctionId,
                Seller = applicantDB,
                Item = new Item
                {
                    Applicant = applicantDB,
                    Name = itemName,
                    Author = author,
                    ItemType = itemTypeDB,
                    Description = description,
                    Date = itemDate
                },
                //TODO: не вбивается цена через точку
                StartPrice = startPrice
            });
            db.SaveChanges();

            Console.WriteLine("Предмет добавлен!");
            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
