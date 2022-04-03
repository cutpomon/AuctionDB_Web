using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreFirstDB
{
    public static class Utils
    {
        /// <summary>
        /// Парсит введеную дату. Дает 3 попытки на ввод, потом завершает программу.
        /// </summary>
        /// <returns>Успешно распарсенная дата </returns>
        public static DateTime TryParseDate()
        {
            int maxTry = 3;
            int currentTry = 1;
            DateTime date = new DateTime();

            while (currentTry <= maxTry + 1)
            {
                DateTime.TryParse(Console.ReadLine(), out date);
                if (currentTry <= maxTry)
                {
                    if (date == DateTime.MinValue)
                    {
                        Console.WriteLine($"Не получилось распознать дату. Осталось попыток {maxTry - currentTry}");
                        currentTry++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Попыток больше не осталось!");
                    Environment.Exit(-1);
                }
            }

            return date;
        }
    }
}
