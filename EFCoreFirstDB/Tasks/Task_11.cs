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
            int operation = Utils.ParseId(2);

            if (operation == 1)
            {
                AddEntity(db);
            }
            else
            {
                EditEntity(db);
            }
            db.SaveChanges();

            Console.WriteLine("Изменения применены успешно!");

            Console.WriteLine("----------------------------------------------------------------");
        }

        public static void AddEntity(AuctionContext db)
        {
            Console.Write("Имя: ");
            string name;
            while ((name = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
            }

            db.Applicants.Add(new Applicant
            {
                Name = name
            });
        }

        public static void EditEntity(AuctionContext db)
        {
            Console.WriteLine("Выберите кого редактировать (ID): ");

            foreach (var applicant in db.Applicants)
            {
                Console.WriteLine($"\t{applicant.Id}. {applicant.Name}");
            }

            Console.Write("ID продавца: ");
            int applicantId = Utils.ParseId(db.Applicants.Max(x => x.Id));

            Applicant targetSeller = db.Applicants.FirstOrDefault(x => x.Id == applicantId);

            Console.Write("Имя: ");
            string name;
            while ((name = Console.ReadLine().Trim()) == string.Empty)
            {
                Console.Write("Это обязательное поле! Повторите ввод: ");
            }

            targetSeller.Name = name;
        }
    }  
}
