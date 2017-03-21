using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestKrokM
{
    class MenuTKM
    {
        int Menu1;
        ManagerTKM Tvm=new ManagerTKM() { };
        public MenuTKM()
        {
            Menu1 = 100;
            Console.WindowWidth = 100;
            Console.WindowHeight = 50;
            //Console.SetWindowPosition(100, 100);
           // Console.ForegroundColor = ConsoleColor.DarkGreen;
           // Console.BackgroundColor = ConsoleColor.White;
            
        }
        public void Run()
        {
            // Цикл пока не ноль
            do
            {
                Console.Clear();
                Console.WriteLine("------ Программа для студентов медучреждений самостоятельной подготовки ------ \r\n ------ к сертификационному екзамену КРОКМ ----------------");
                Console.WriteLine("--- Введите номер задачи  1 .. 10 ( 0 - виход) --- \r\n");
                Console.WriteLine("1  Выбрать файл вопросов (путь к файлу тхт с тестами КРОКМ) << \r\n");
                Console.WriteLine("2  Пройти тест КРОКМ (сначала нужно выбрать файл тхт тестов (пункт 1))   \r\n");
                Console.WriteLine("3  Добавить вопросов <<  \r\n");
                Console.WriteLine("4  Сформировать тест с ответов (только неправильно отвеченные)   \r\n");
                Console.WriteLine("5  Посмотреть ответы    \r\n");
                Console.WriteLine("6  Аналитика сложности   \r\n");
                Console.WriteLine("7  Вывод всех результатов   \r\n");
                Console.WriteLine("8  Cделать тест доступным по сети   \r\n");
                Console.WriteLine("9  Сформировать c данных вопросов тест  \r\n");
                Console.WriteLine("10 Сохранить во внутренем зашифрованном формате   \r\n");
                Console.WriteLine("11 Убрать легкие вопросы <<  \r\n");
                Console.WriteLine("12 сложные вопросы первее <<  \r\n");
                Menu1=Convert.ToInt32(Console.ReadLine());
                switch (Menu1)
                {
                    case 1: //задача 1 инициализация
                        {
                            if (Tvm.hr!=null) Tvm.Init();
                            else
                            {
                                Tvm.hr.Clear();
                                // delete[] Tvm.M1;
                               // Tvm.M1.
                                Tvm.M1 = null;
                                Tvm.Init();
                            }
                            //prishlos dobavit break;
                            break;
                        }
                    case 2:// задача 2 запуск
                        {
                            //if (Tvm.hr != null) Tvm.Init();

                            Tvm.Randomize(); Tvm.BigSort(); Tvm.RunTestm();
                            Tvm.Result();
                            break;
                        }
                    case 3:// задача 3
                        {
                            break;
                        }
                    case 4: // задача 4
                        {
                            Tvm.FResult();
                            break;
                        }

                    case 5: //задача 5
                        {
                            break;
                        }
                    case 6: //задача 6
                        {
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            break;
                        }
                } //закрытие свича выбора задач
            } while (Menu1 != 0); // цикл - пока меню не ноль
        }
    }
}

