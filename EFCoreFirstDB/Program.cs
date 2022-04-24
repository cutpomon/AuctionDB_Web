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
            using (var db = new AuctionContext(null))
            {
                //Пересоздаем БД
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //Заполняем БД
                //FillDB(db);
                //UpdateItemsBuyers(db);

                //Основной цикл
                while (true)
                {
                    Console.Write("Введите номер задачи, которую нужно выполнить (1-12): ");

                    //TODO: Проверка на символы
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
            #region Добавление участников аукциона

            var applicant1 = new Applicant { Name = "Pablo" };
            db.Add(applicant1);
            db.SaveChanges();

            var applicant2 = new Applicant { Name = "Tanos" };
            db.Add(applicant2);
            db.SaveChanges();

            var applicant3 = new Applicant { Name = "Ivan" };
            db.Add(applicant3);
            db.SaveChanges();

            var applicant4 = new Applicant { Name = "Petr" };
            db.Add(applicant4);
            db.SaveChanges();

            var applicant5 = new Applicant { Name = "Nikolay" };
            db.Add(applicant5);
            db.SaveChanges();

            var applicant6 = new Applicant { Name = "Nikita" };
            db.Add(applicant6);
            db.SaveChanges();

            var applicant7 = new Applicant { Name = "Alexey" };
            db.Add(applicant7);
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
                ApplicantId = 1,
                Name = "Impala",
                Date = new DateTime(1971, 5, 22),
                Description = "Wow, such a nice car",
                ItemTypeId = 2
            };
            db.Add(item1);
            db.SaveChanges();

            var item2 = new Item
            {
                ApplicantId = 2,
                Name = "Ring of Infinity",
                Date = new DateTime(2020, 1, 10),
                Description = "Wow, such a nice ring",
                ItemTypeId = 3
            };
            db.Add(item2);
            db.SaveChanges();

            var item3 = new Item
            {
                ApplicantId = 2,
                Name = "Mona Lisa",
                Author = "Leonardo Da Vinci",
                Date = new DateTime(1517, 1, 1),
                Description = "Wow, such a famous painting (not fake I swear)",
                ItemTypeId = 1
            };
            db.Add(item3);
            db.SaveChanges();

            var item4 = new Item
            {
                ApplicantId = 1,
                Name = "Lisa Mona",
                Description = "Just a painting",
                ItemTypeId = 1
            };
            db.Add(item4);
            db.SaveChanges();
            #endregion

            #region Добавление лотов 
            var lot1 = new Lot
            {
                AuctionId = 1,
                ItemId = 1,
                SellerId = 1,
                StartPrice = 1000
            };
            db.Add(lot1);
            db.SaveChanges();

            var lot2 = new Lot
            {
                AuctionId = 1,
                ItemId = 2,
                SellerId = 2,
                StartPrice = 30000
            };
            db.Add(lot2);
            db.SaveChanges();

            var lot3 = new Lot
            {
                AuctionId = 2,
                ItemId = 3,
                SellerId = 2,
                StartPrice = 100000
            };
            db.Add(lot3);
            db.SaveChanges();

            var lot4 = new Lot
            {
                AuctionId = 2,
                ItemId = 4,
                SellerId = 1
            };
            db.Add(lot4);
            db.SaveChanges();
            #endregion

            #region Добавление ставок
            var bid1 = new Bid
            {
                LotId = 1,
                ApplicantId = 2,
                BidPrice = 4999
            };
            db.Add(bid1);
            db.SaveChanges();

            var bid2 = new Bid
            {
                LotId = 1,
                ApplicantId = 1,
                BidPrice = 38512
            };
            db.Add(bid2);
            db.SaveChanges();

            var bid3 = new Bid
            {
                LotId = 1,
                ApplicantId = 5,
                BidPrice = 38512.73M
            };
            db.Add(bid3);
            db.SaveChanges();

            var bid4 = new Bid
            {
                LotId = 2,
                ApplicantId = 3,
                BidPrice = 150000
            };
            db.Add(bid4);
            db.SaveChanges();

            var bid5 = new Bid
            {
                LotId = 2,
                ApplicantId = 1,
                BidPrice = 1000000
            };
            db.Add(bid5);
            db.SaveChanges();

            var bid6 = new Bid
            {
                LotId = 3,
                ApplicantId = 4,
                BidPrice = 100500
            };
            db.Add(bid6);
            db.SaveChanges();

            var bid7 = new Bid
            {
                LotId = 3,
                ApplicantId = 3,
                BidPrice = 300000000
            };
            db.Add(bid7);
            db.SaveChanges();

            var bid8 = new Bid
            {
                LotId = 4,
                ApplicantId = 5,
                BidPrice = 81.75M
            };
            db.Add(bid8);
            db.SaveChanges();

            var bid9 = new Bid
            {
                LotId = 4,
                ApplicantId = 2,
                BidPrice = 10756
            };
            db.Add(bid9);
            db.SaveChanges();
            #endregion
        }

        /// <summary>
        /// Добавление факта продажи
        /// </summary>
        /// <param name="db">Контекст БД</param>
        public static void UpdateItemsBuyers(AuctionContext db)
        {
            //TODO: Сделать триггер на автоматическое добавление покупателя (пока костыль)
            var item1 = db.Lots.Find(1);
            item1.CustomerId = 5;
            item1.BuyPrice = 38512.73M;

            var item2 = db.Lots.Find(2);
            item2.CustomerId = 1;
            item2.BuyPrice = 1000000;

            var item3 = db.Lots.Find(3);
            item3.CustomerId = 3;
            item3.BuyPrice = 300000000;

            var item4 = db.Lots.Find(4);
            item4.CustomerId = 2;
            item4.BuyPrice = 10756;

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
