using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Instuments
{
    public static class FirstTaskRandom
    {
        //массив русских символов
        static string rusLetters = new string(Enumerable.Range(1040, 64).Select(x => ((char)x)).ToArray()) + "Ёё";
        //массив английских символов
        static string engLetters = new string(Enumerable.Range('A', 26).Select(x => ((char)x)).ToArray())
            + new string(Enumerable.Range('a', 26).Select(x => (char)x).ToArray());
        //значение промежутка от настоящего года
        static int nextYears = -5;
        //размер генерируемого набора символов
        static int length = 10;
        static int minInt = 1;
        //половина максимального целого значение
        static int halfMaxInt = 100000000 / 2 + 1;
        //количество значений после запятой
        public static int afterPoint = 8;
        static int minDouble = 1;
        //последнее целое число в интервале для генерации дробных чисел
        static int preMaxDouble = 19;
        public static DateTime ReturnRandomDate(Random random)
        {
            //Получение текущей даты за вычетом nextYears лет
            DateTime date = DateTime.Now.AddYears(nextYears);
            //получение интервала nextYears лет в днях
            int range = (DateTime.Now - date).Days;
            //генерация значения даты прибавкой случайного количества дней к дате nextYears лет назад
            return date.AddDays(random.Next(range));

        }
        public static string ReturnRandomEngString(Random random)
        {
            //генерация набора английских символов длины length
            string res = new string(Enumerable.Repeat(engLetters, length).Select(x => x[random.Next(engLetters.Length)]).ToArray());
            return res;
        }
        public static string ReturnRandomRusString(Random random) 
        {
            //генерация набора русских символов длины length
            string res = new string(Enumerable.Repeat(rusLetters, length).Select(x => x[random.Next(rusLetters.Length)]).ToArray());
            return res;
        }
        public static int ReturnRandomInt(Random random)
        {
            //генерация чётного числа от minInt до (halfMaxInt-1)*2
            return random.Next(minInt, halfMaxInt) * 2;
        }
        public static double ReturnRandomDouble(Random random)
        {
            //Генерация случайного дробного числа с minDouble до preMaxDouble + minDouble с округлением до afterPoint цифр после запятой
            return Math.Round(random.NextDouble()*preMaxDouble + minDouble,afterPoint);
        }
    }
}
