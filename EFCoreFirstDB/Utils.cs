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
        /// Парсит введеную дату
        /// </summary>
        /// <returns>Успешно распарсенная дата </returns>
        public static DateTime ParseDate()
        {
            DateTime date;
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    break;
                }
                else
                {
                    Console.Write($"Не получилось распознать дату. Повторите ввод: ");
                    continue;
                }
            }

            return date;
        }

        public static int ParseId(int max)
        {
            int result;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result <= 0 || result > max)
                    {
                        Console.Write("Нет такого идентификатора. Повторите ввод: ");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.Write("Не удалось распознать число. Повторите ввод: ");
                    continue;
                }
                
            }

            return result;
        }
    }
}
