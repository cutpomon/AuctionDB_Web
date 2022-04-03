using System;
using System.Linq;
using System.Collections.Generic;
using EFCoreFirstDB.Models;
using EFCoreFirstDB.Task;

namespace EFCoreFirstDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AuctionContext())
            {
                //Пересоздаем БД
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //Заполняем БД
                FillDB(db);
                UpdateItemsBuyers(db);

                //Основной цикл
                while (true)
                {
                    Console.Write("Введите номер задачи, которую нужно выполнить (1-12): ");

                    int taskId = int.Parse(Console.ReadLine());
                    if (taskId < 1 || taskId > 12)
                    {
                        Console.WriteLine("Нет такой задачи! Попробуйте еще раз");
                        continue;
                    }

                    TaskExecutionFactory(taskId, db);

                    Console.Write("Ещё одну? (y/n) ");
                    if (Console.ReadLine() != "y")
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Mission comlited! Respect++");
        }

        /// <summary>
        /// Заполнение БД
        /// </summary>
        /// <param name="db">Контекст БД</param>
        public static void FillDB(AuctionContext db)
        {
            //Add и AddRange добавляют в случайном порядке, поэтому сохраняю после каждой вставки, т.к. опираюсь на Id
            #region Добавление продавцов

            var seller1 = new Seller { Name = "Pablo" };
            db.Add(seller1);
            db.SaveChanges();

            var seller2 = new Seller { Name = "Tanos" };
            db.Add(seller2);
            db.SaveChanges();
            #endregion

            #region Добавление покупателей
            var customer1 = new Customer { Name = "Ivan" };
            db.Add(customer1);
            db.SaveChanges();

            var customer2 = new Customer { Name = "Petr" };
            db.Add(customer2);
            db.SaveChanges();

            var customer3 = new Customer { Name = "Nikolay" };
            db.Add(customer3);
            db.SaveChanges();

            var customer4 = new Customer { Name = "Nikita" };
            db.Add(customer4);
            db.SaveChanges();

            var customer5 = new Customer { Name = "Alexey" };
            db.Add(customer5);
            db.SaveChanges();
            #endregion

            #region Добавление типов предметов
            var itemType1 = new ItemType { Type = "Painting" };
            db.Add(itemType1);
            db.SaveChanges();

            var itemType2 = new ItemType { Type = "Car" };
            db.Add(itemType2);
            db.SaveChanges();

            var itemType3 = new ItemType { Type = "Ring" };
            db.Add(itemType3);
            db.SaveChanges();
            #endregion

            #region Добавление свойств предметов
            var itemProperty1 = new ItemProperty { Name = "Oil" };
            db.Add(itemProperty1);
            db.SaveChanges();

            var itemProperty2 = new ItemProperty { Name = "Pastel" };
            db.Add(itemProperty2);
            db.SaveChanges();

            var itemProperty3 = new ItemProperty { Name = "Brilliant" };
            db.Add(itemProperty3);
            db.SaveChanges();

            var itemProperty4 = new ItemProperty { Name = "Gold" };
            db.Add(itemProperty4);
            db.SaveChanges();

            var itemProperty5 = new ItemProperty { Name = "Krasivoe" };
            db.Add(itemProperty5);
            db.SaveChanges();
            #endregion

            #region Добавление аукционов
            var auction1 = new Auction 
            {
                Place = "Monaco",
                Name = "Monaco Operational National Auction of Cat Overthinking (MONACO)",
                Date = new DateTime(2020, 1, 1),
            };
            db.Add(auction1);
            db.SaveChanges();

            var auction2 = new Auction
            {
                Place = "Иваново",
                Name = "Распродажа предметов роскоши изъятых в ходе судебных процессов " + 
                    "на основании Заключения № ИСЗ-12652-115",
                Date = new DateTime(2022, 1, 1)
            };
            db.Add(auction2);
            db.SaveChanges();
            #endregion

            #region Добавление предметов
            var item1 = new Item
            {
                SellerId = 1,
                Name = "Impala",
                Date = new DateTime(1971, 5, 22),
                Description = "Wow, such a nice car",
                ItemTypeId = 2,
                ItemProperties = new List<ItemProperty> { db.ItemProperties.Find(5) }
            };
            db.Add(item1);
            db.SaveChanges();

            var item2 = new Item
            {
                SellerId = 2,
                Name = "Ring of Infinity",
                Date = new DateTime(2020, 1, 10),
                Description = "Wow, such a nice ring",
                ItemTypeId = 3,
                ItemProperties = new List<ItemProperty> { db.ItemProperties.Find(3), db.ItemProperties.Find(5) }
            };
            db.Add(item2);
            db.SaveChanges();

            var item3 = new Item
            {
                SellerId = 2,
                Name = "Mona Lisa",
                Author = "Leonardo Da Vinci",
                Date = new DateTime(1517, 1, 1),
                Description = "Wow, such a famous painting (not fake I swear)",
                ItemTypeId = 1,
                ItemProperties = new List<ItemProperty> { db.ItemProperties.Find(1), db.ItemProperties.Find(5) }
            };
            db.Add(item3);
            db.SaveChanges();

            var item4 = new Item
            {
                SellerId = 1,
                Name = "Lisa Mona",
                Description = "Just a painting",
                ItemTypeId = 1,
                ItemProperties = new List<ItemProperty> { db.ItemProperties.Find(2), db.ItemProperties.Find(5) }
            };
            db.Add(item4);
            db.SaveChanges();
            #endregion

            #region Добавление лотов 
            var lot1 = new Lot
            {
                AuctionId = 1,
                ItemId = 1,
                StartPrice = 1000
            };
            db.Add(lot1);
            db.SaveChanges();

            var lot2 = new Lot
            {
                AuctionId = 1,
                ItemId = 4,
                StartPrice = 30000
            };
            db.Add(lot2);
            db.SaveChanges();

            var lot3 = new Lot
            {
                AuctionId = 2,
                ItemId = 2,
                StartPrice = 100000
            };
            db.Add(lot3);
            db.SaveChanges();

            var lot4 = new Lot
            {
                AuctionId = 2,
                ItemId = 3,
            };
            db.Add(lot4);
            db.SaveChanges();
            #endregion

            #region Добавление ставок
            var bid1 = new Bid
            {
                LotId = 1,
                CustomerId = 2,
                BidPrice = 4999
            };
            db.Add(bid1);
            db.SaveChanges();

            var bid2 = new Bid
            {
                LotId = 1,
                CustomerId = 1,
                BidPrice = 38512
            };
            db.Add(bid2);
            db.SaveChanges();

            var bid3 = new Bid
            {
                LotId = 1,
                CustomerId = 5,
                BidPrice = 38512.73M
            };
            db.Add(bid3);
            db.SaveChanges();

            var bid4 = new Bid
            {
                LotId = 2,
                CustomerId = 3,
                BidPrice = 150000
            };
            db.Add(bid4);
            db.SaveChanges();

            var bid5 = new Bid
            {
                LotId = 2,
                CustomerId = 1,
                BidPrice = 1000000
            };
            db.Add(bid5);
            db.SaveChanges();

            var bid6 = new Bid
            {
                LotId = 3,
                CustomerId = 4,
                BidPrice = 100500
            };
            db.Add(bid6);
            db.SaveChanges();

            var bid7 = new Bid
            {
                LotId = 3,
                CustomerId = 3,
                BidPrice = 300000000
            };
            db.Add(bid7);
            db.SaveChanges();

            var bid8 = new Bid
            {
                LotId = 4,
                CustomerId = 5,
                BidPrice = 81.75M
            };
            db.Add(bid8);
            db.SaveChanges();

            var bid9 = new Bid
            {
                LotId = 4,
                CustomerId = 2,
                BidPrice = 10756
            };
            db.Add(bid9);
            db.SaveChanges();
            #endregion
        }

        /// <summary>
        /// Аукционы завершены, определяем покупаетелй
        /// </summary>
        /// <param name="db">Контекст БД</param>
        public static void UpdateItemsBuyers(AuctionContext db)
        {
            //TODO: Сделать триггер на автоматическое добавление покупателя (пока костыль)
            var item1 = db.Items.Find(1);
            item1.CustomerId = 5;
            item1.BuyPrice = 38512.73M;

            var item2 = db.Items.Find(2);
            item2.CustomerId = 3;
            item2.BuyPrice = 300000000;

            var item3 = db.Items.Find(3);
            item3.CustomerId = 2;
            item3.BuyPrice = 10756;

            var item4 = db.Items.Find(4);
            item4.CustomerId = 1;
            item4.BuyPrice = 1000000;

            db.SaveChanges();
        }

        /// <summary>
        /// Фабрика заданий
        /// </summary>
        /// <param name="taskId">Номер задания</param>
        /// <param name="db">Контекст БД</param>
        public static void TaskExecutionFactory(int taskId, AuctionContext db)
        {
            Console.WriteLine();

            switch(taskId)
            {
                case 1:
                    Task_01.Execute(db);
                    break;
                case 2:
                    Task_02.Execute(db);
                    break;
                case 3:
                    Task_03.Execute(db);
                    break;
                case 4:
                    Task_04.Execute(db);
                    break;
                case 5:
                    Task_05.Execute(db);
                    break;
                case 6:
                    Task_06.Execute(db);
                    break;
                case 7:
                    Task_07.Execute(db);
                    break;
                case 8:
                    Task_08.Execute(db);
                    break;
                case 9:
                    Task_09.Execute(db);
                    break;
                case 10:
                    Task_10.Execute(db);
                    break;
                case 11:
                    Task_11.Execute(db);
                    break;
                case 12:
                    Task_12.Execute(db);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
