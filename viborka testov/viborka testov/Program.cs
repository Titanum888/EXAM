using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; //Подключаем пространство имен для работы с файлами

namespace converter_kroktest_to_txt
{
    class Program
    {
        static void Main(string[] args)
        {
            string put,put2;
            bool indicator = true;
            int Counter=0;
            List<int> Masiv=new List<int> { };
            List<char> Maschar=new List<char> { };
            //List <int> ddd;
            Console.WriteLine("Программа выборка тестов");
            Console.WriteLine("---- разработчик Брик О В ----");
            Console.WriteLine("Введите путь к файлу");
            put = Console.ReadLine();
            FileInfo file = new FileInfo(put);
            if (file.Exists == false) //Если файл не существует
            {
                Console.WriteLine("такого файла не существует");
            }
            Console.WriteLine("Введите путь к файлу номеров тестов");
            put2 = Console.ReadLine();
            FileInfo file2 = new FileInfo(put2);
            if (file2.Exists == false) //Если файл не существует
            {
                Console.WriteLine("такого файла не существует");
            }
            // int buf=255;
            StreamWriter write_text = new StreamWriter("resultkroktest2.txt", false, Encoding.Default);  //Класс для записи в файл
                                                                                                        // new StreamWriter(writePath, false, System.Text.Encoding.Default);
            StreamReader streamReader = new StreamReader(put, Encoding.Default);
            StreamReader streamReader2 = new StreamReader(put2, Encoding.Default);
            string str = ""; //Объявляем переменную, в которую будем записывать текст из файла
                             // char[] str2;
                             // write_text = file.AppendText();
            string str2 = "";
            while (!streamReader2.EndOfStream) //Цикл длиться пока не будет достигнут конец файла
            {
               // str2 += ' ';
                str2 =streamReader2.ReadLine(); //В переменную str по строчно записываем содержимое файла
                Masiv.Add(Convert.ToInt32(str2));
                
            }
           // Maschar = str2.ToList();
            foreach (var ff in Masiv)
            {
               // Masiv.Add((int)ff);
                Console.WriteLine((int)ff);
            }
            while (!streamReader.EndOfStream) //Цикл длиться пока не будет достигнут конец файла
            {
                str = streamReader.ReadLine(); //В переменную str по строчно записываем содержимое файла
                if (str!="" && str[0] == '?')
                {
                    Counter++;
                    indicator = true;
                }
                
                foreach(var kk in Masiv)
                {
                    if (kk == Counter) indicator = false;
                }
                   if (str!="" && indicator== true) write_text.WriteLine(str); //запись в файл
            }
          /*  while (!streamReader.EndOfStream) //Цикл длиться пока не будет достигнут конец файла
            {
                str = streamReader.ReadLine(); //В переменную str по строчно записываем содержимое файла

                // str2=str.ToCharArray();
                // if (str2[0]==' ') str.
                if (str != "")
                {
                    if (str[0] == 'A')
                    {
                        str = str.Remove(0, 1);
                        str = str.Insert(0, "+");
                        str = str.Remove(1, 2);
                        //str = str.Remove(0, 1);
                    }
                    else if ((str[0] == 'B') || (str[0] == 'C') || (str[0] == 'D') || (str[0] == 'E'))
                    {
                        str = str.Remove(0, 1);
                        str = str.Insert(0, "-");
                        str = str.Remove(1, 1);
                    }
                    //else if (str[0] == (char)13) str = "";
                    else while (str[0] == '\t' || str[0] == ' ' || (str[0] >= 1 && str[0] <= 9))
                        {
                            str = str.Remove(0, 1);
                            if (str[0] >= 1 && str[0] <= 9)
                            {
                                str = "?\n";
                                //str = str.Insert(0, "?");
                            }
                        }
                    write_text.WriteLine(str);
                }
                //write_text.WriteLine(str);
                // Console.WriteLine(str);

            }*/
            write_text.Close();
            streamReader.Close();
            streamReader2.Close();
            Console.ReadLine();
        }
    }
}
