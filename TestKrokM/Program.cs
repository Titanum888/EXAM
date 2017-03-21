using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestKrokM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------ Программа для студентов медучреждений улучшеной самостоятельной подготовки ----- \r\n\r\n ------- к сертификационному екзамену КРОКМ --------\r\n");
            Console.WriteLine("авторские права принадлежат программисту Брык О. В. titanum8@ukr.net \r\n https://vk.com/alex_bryk \r\n");
            string parol;
            MenuTKM m1 = new MenuTKM();
            Console.WriteLine("Введите пароль входа в программу");
            parol = Console.ReadLine();
            if (parol != "Dobrobut"+Convert.ToString(DateTime.Today.Day))
                Console.WriteLine("Неправильный пароль обратитесь к разработчику");
            else if (DateTime.Now.Month < 5)
            {
                if (DateTime.Today.Month == DateTime.Now.Month)
                    m1.Run(); //Запуск
                else Console.WriteLine("Неправильная Дата(месяц) на компютере");
            }
            else Console.WriteLine("Закончился строк действия данного пароля обратитесь к разработчику");
            Console.WriteLine("Нажмите Ентер для выхода из программы");
            Console.ReadLine();
        }
    }
}