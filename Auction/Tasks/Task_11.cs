using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreFirstDB.Models;

namespace EFCoreFirstDB.Task
{
    class Task_11
    {
        public static void Execute(AuctionContext db)
        {
            //TODO: Релизовать проверку на существование добавляемой/редактируемой сущности

            Console.WriteLine("Задание 11. Предоставить возможность добавления и изменения информации о продавцах и покупателях");
            Console.WriteLine("----------------------------------------------------------------");

            Console.WriteLine($"Действие:{Environment.NewLine}\t1.Добавить{Environment.NewLine}\t2.Редактировать");

            Console.Write("Номер действия: ");
            int operation;
            while (true)
            {
                try
                {
                    operation = int.Parse(Console.ReadLine());
                    if (operation <= 0 || operation > 2)
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

            Console.WriteLine($"Сущность:{Environment.NewLine}\t1.Продавец{Environment.NewLine}\t2.Покупатель");

            Console.Write("Номер сущности: ");
            int entityType;
            while (true)
            {
                try
                {
                    entityType = int.Parse(Console.ReadLine());
                    if (entityType <= 0 || entityType > 2)
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

            if (operation == 1)
            {
                Console.Write("Имя: ");
                string name = Console.ReadLine().Trim();
                while (name == string.Empty)
                {
                    Console.Write("Это обязательное поле! Повторите ввод: ");
                    name = Console.ReadLine().Trim();
                }

                if (entityType == 1)
                {
                    db.Sellers.Add(new Seller 
                    { 
                        Name = name
                    });
                }
                else
                {
                    db.Customers.Add(new Customer
                    {
                        Name = name
                    });
                }
            }
            else
            {
                Console.WriteLine("Выберите кого редактировать (ID): ");

                if (entityType == 1)
                {
                    foreach (var seller in db.Sellers)
                    {
                        Console.WriteLine($"\t{seller.Id}. {seller.Name}");
                    }

                    Console.Write("ID продавца: ");
                    int sellerId;
                    while (true)
                    {
                        try
                        {
                            sellerId = int.Parse(Console.ReadLine());
                            if (sellerId <= 0 || sellerId > db.Sellers.Max(x => x.Id))
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
                    Seller targetSeller = db.Sellers.Where(x => x.Id == sellerId).FirstOrDefault();

                    Console.Write("Имя: ");
                    string name = Console.ReadLine().Trim();
                    while (name == string.Empty)
                    {
                        Console.Write("Это обязательное поле! Повторите ввод: ");
                        name = Console.ReadLine().Trim();
                    }

                    targetSeller.Name = name;
                }
                else
                {
                    foreach (var customer in db.Customers)
                    {
                        Console.WriteLine($"\t{customer.Id}. {customer.Name}");
                    }

                    Console.Write("ID покупателя: ");
                    int customerId;
                    while (true)
                    {
                        try
                        {
                            customerId = int.Parse(Console.ReadLine());
                            if (customerId <= 0 || customerId > db.Sellers.Max(x => x.Id))
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
                    Customer targetCustomer = db.Customers.Where(x => x.Id == customerId).FirstOrDefault();

                    Console.Write("Имя: ");
                    string name = Console.ReadLine().Trim();
                    while (name == string.Empty)
                    {
                        Console.Write("Это обязательное поле! Повторите ввод: ");
                        name = Console.ReadLine().Trim();
                    }

                    targetCustomer.Name = name;
                }
            }
            db.SaveChanges();

            Console.WriteLine("Изменения применены успешно!");

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
