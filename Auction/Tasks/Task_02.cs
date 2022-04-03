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

            foreach (var auction in db.Auctions)
            {
                Console.WriteLine($"\t{auction.Id}. {auction.Name} проходящий в {auction.Place}");
            }
            Console.WriteLine();

            int auctionId;
            Console.Write("ID аукциона: ");
            while (true)
            {
                try
                {
                    auctionId = int.Parse(Console.ReadLine());
                    if (auctionId <= 0 || auctionId > db.Auctions.Max(x => x.Id))
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
            

            Console.Write("Имя продавца: ");
            string seller = Console.ReadLine().Trim();
            while (seller == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
                seller = Console.ReadLine().Trim();
            }
            Seller sellerDB = db.Sellers.FirstOrDefault(s => s.Name == seller);

            Console.Write("Название предмета: ");
            string name = Console.ReadLine().Trim();
            while (name == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
                name = Console.ReadLine().Trim();
            }

            Console.Write("Автор/создатель предмета (optional): ");
            string author = Console.ReadLine().Trim();

            Console.Write("Тип предмета: ");
            string itemType = Console.ReadLine().Trim();
            while (itemType == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
                itemType = Console.ReadLine().Trim();
            }
            ItemType itemTypeDB = db.ItemTypes.FirstOrDefault(s => s.Type == itemType);

            //TODO: добавлять только несуществующие в базе свойства, а существующие привязывать
            Console.Write("Свойства предмета (optional). Перечисляйте через запятую: ");
            var itemProperties = Console.ReadLine().Split(',').ToList();
            List<ItemProperty> itemPropsDB = new List<ItemProperty>();
            foreach (var prop in itemProperties)
            {
                itemPropsDB.Add(new ItemProperty
                {
                    Name = prop.Trim()
                });
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
                Item = new Item
                {
                    Seller = sellerDB ?? new Seller { Name = seller },
                    Name = name,
                    Author = author,
                    ItemType = itemTypeDB ?? new ItemType { Type = itemType },
                    ItemProperties = itemPropsDB,
                    Description = description,
                    Date = itemDate
                },
                StartPrice = startPrice
            });
            db.SaveChanges();

            Console.WriteLine("Предмет добавлен!");
            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
