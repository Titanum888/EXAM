using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestKrokM
{
    class ManagerTKM
    {
       public struct Modul
        {
          public int Npp; //id cтроки
            public int Nmp; // id номер модуля
            public int Vrstrok; // вопрос варіант или прав ответ или неправ
            public int Nvn; // внутр номер если вопрос всегда 0.
            public int Rd; //рядок поля
                    //= 1; //рядок
                    //int OtvetN = -1;
        }
       public struct Resultat
        {
            public int Nmp;
            public int Otvet;
        };
        public string data { get; set; }
        Random rand = new Random();
        public List <string> hr=new List<string>();
        List<Resultat> Resultati=new List<Resultat>();
        Resultat Rt;
        Resultat tempRt;
        bool marker;
        int idStroka = 0; //-1
        int NVoprosa = 0;
        int index1, index2;
        int n = 0; // количество вопросов
        string b; //bufer2
        string Putkfailu;
        int kriterij = 0;
        int chetchik_voprosa = 0;
        int pravotvet, kolpravotvetov, otvetusera;
        FileStream fs;
        StreamReader F; //поток чтения файла
        StreamWriter outF; //поток записи в файл
        int RazmerStrok;// = hr.size() + 1;
        public Modul [] M1;
        Modul tempmodul;
        public void showvector(string str2)
        {
            Console.WriteLine("{0}\r\n", str2);
        }
        public void ShowModul()
        {
            for (int i = 1; i < RazmerStrok; i++)
            {
                Console.WriteLine("{0,3} {1,3} {2,3} {3,3} {4,3} {5,3}\r\n", M1[i].Npp, M1[i].Nmp, M1[i].Vrstrok , M1[i].Nvn, M1[i].Rd);
            }
            Console.ReadLine();
        }
        public void Modulobmen(int tempi, int tempj) //перемена строк структури кодировки тестов местами.
        {
            tempmodul = M1[tempi];
            M1[tempi] = M1[tempj];
            M1[tempj] = tempmodul;
        }
        public void Zamenai()
        {
            if (b != "") b = b.Remove(0, 1);
            b=b.Replace('і', 'i');
            b=b.Replace('І', 'I');
        }
        public void Init()
        {
            pravotvet = -1; kolpravotvetov = 0; otvetusera = 0;
            Console.WriteLine("Введите название файла\r\n");
            Putkfailu = Console.ReadLine(); ;

            if (!File.Exists(Putkfailu))
            {
                throw new Exception("Такого файла не существет");
            }
            else
            {
                fs = new FileStream(Putkfailu, FileMode.Open, FileAccess.Read);
                F = new StreamReader(fs, Encoding.Default);
                data = F.ReadToEnd();
                if (data == String.Empty)
                {
                    throw new Exception("Отсутсвуют данные для чтения");
                }
            }
            string[] gg = data.Split('\n');
            if (F!=null)
            {
                foreach (var dd in gg)
                {
                    hr.Add(dd);
                }
            }
            else
            {
                Console.WriteLine("{0} Файл не найден", Putkfailu);
                Console.ReadLine();
            }
            F.Close();
            hr.Add("?");
            RazmerStrok = hr.Count() + 1;
            Modul[] M1 = new Modul[RazmerStrok];
            //------------
            M1[0].Npp = 0;
            M1[0].Vrstrok = 0;
            M1[0].Rd = 0;
            M1[0].Nmp = 1;
            M1[0].Nvn = 0;
            foreach (var it in hr)
            {
                b = it;
                if (b!="") switch (b[0])
                {
                    case '?':
                        {
                            kriterij = 0; // вопрос
                            chetchik_voprosa = 0;
                            NVoprosa++;
                            idStroka++;
                            M1[idStroka].Npp = idStroka;
                            M1[idStroka].Vrstrok = 0;
                            M1[idStroka].Rd = 1;
                            M1[idStroka].Nmp = NVoprosa;
                            M1[idStroka].Nvn = chetchik_voprosa;
                            n++;
                            break;
                        }
                    case '+':
                        {
                            kriterij = 1;//правильний ответ
                            chetchik_voprosa++;
                            pravotvet = chetchik_voprosa;
                            idStroka++;
                            M1[idStroka].Npp = idStroka;
                            M1[idStroka].Nmp = NVoprosa;
                            M1[idStroka].Vrstrok = 1;
                            M1[idStroka].Rd = 1;
                            M1[idStroka].Nvn = chetchik_voprosa;
                            break;
                        }
                    case '-':
                        {
                            kriterij = 2;//неправильний ответ
                            chetchik_voprosa++;
                            idStroka++;
                            M1[idStroka].Npp = idStroka;
                            M1[idStroka].Nmp = NVoprosa;
                            M1[idStroka].Vrstrok = 2;
                            M1[idStroka].Rd = 1;
                            M1[idStroka].Nvn = chetchik_voprosa;
                            break;
                        }
                    default:
                        {
                            idStroka++;
                            M1[idStroka].Npp = idStroka;
                            M1[idStroka].Nmp = NVoprosa;
                            M1[idStroka].Vrstrok = kriterij;
                            M1[idStroka].Rd = M1[idStroka - 1].Rd + 1;
                            M1[idStroka].Nvn = chetchik_voprosa;
                        }
                        break;
                }	
            }
            this.M1 = M1;
        }
        public void Randomize()
        {
            Console.WriteLine("перемешивание!!!\r\n");
            for (int i = 1; i < 1000; i++)
            {
                index1 = 1 + rand.Next() % (RazmerStrok - 2);
                index2 = 1 + rand.Next() % (RazmerStrok - 2);
                //поменять местами
                Modulobmen(index1, index2);
            }
            //ShowModul();
        }
        public void BigSort()
        {
            /////////////////сортировка!!!
            Console.WriteLine("сортировка по номеру вопроса\r\n");
            for (int i = 2; i < RazmerStrok - 1; i++)
            {
                for (int j = i + 1; j < RazmerStrok - 1; j++)
                {
                    if (M1[i - 1].Nmp == M1[j].Nmp)
                    {
                        //поменять местами
                        Modulobmen(i, j);
                    }
                }
            }
            //ShowModul(); //отладка

            ///////////////// внутри сортировка!!! 
            Console.WriteLine("сортировка вопрос поднять выше\r\n");
            for (int i = 1; i < RazmerStrok - 1; i++)
            {
                //for (int z = 0; z < 3; z++)
                for (int j = i + 1; j < RazmerStrok - 1; j++)
                {
                    if ((M1[i].Nmp == M1[j].Nmp) && (M1[j].Vrstrok == 0))
                    {
                        //поменять местами
                        Modulobmen(i, j);
                    }
                    //else break;
                }
            }
            //ShowModul(); //отладка
            //////////////////// Глубокая сортировка????
            Console.WriteLine("сортировка по внутренним номерам\r\n");
            for (int i = 1; i < RazmerStrok - 1; i++)
            {
                for (int j = i + 1; j < RazmerStrok - 1; j++)
                {
                    if ((M1[i].Nmp == M1[j].Nmp) && (M1[i].Vrstrok != 0) && (M1[i - 1].Nvn == M1[j].Nvn))
                    {
                        //поменять местами
                        Modulobmen(i, j);
                    }
                }
            }
            //ShowModul(); //отладка
            ///////////////Внутренний проход
            Console.WriteLine("сортировка по рядкам\r\n");
            for (int i = 1; i < RazmerStrok - 1; i++)
            {
                for (int j = i + 1; j < RazmerStrok - 1; j++)
                {
                    if ((M1[i].Nmp == M1[j].Nmp) && (M1[i].Vrstrok == M1[j].Vrstrok) && (M1[i].Nvn == M1[j].Nvn) && (M1[i].Rd > M1[j].Rd))
                    {
                        //поменять местами
                        Modulobmen(i, j);
                    }
                }
            }
            //ShowModul();
            ////////////// -------------------------------
        }
        public void RunTestm()
        {
            Console.Clear();
            n = 0; // количество вопросов
            idStroka = 0; //-1
            NVoprosa = 0;
            for (int i = 1; i < RazmerStrok; i++)
            {
                if (b != "") b = hr[M1[i].Npp - 1];
               if (b!="") switch (b[0])
                {
                    case '?':
                        {
                            Console.WriteLine("\r\n\r\n\r\n"); 
                            if (n != 0)
                            {
                                if (n == 1)
                                    Console.WriteLine("{0,10}+{1,10}-{2,10}% \r\n", kolpravotvetov, n - 1 - kolpravotvetov,kolpravotvetov * 100 / n);
                                else
                                    Console.WriteLine("{0,10}+{1,10}-{2,10}% \r\n",kolpravotvetov, n - 1 - kolpravotvetov,kolpravotvetov * 100 / (n - 1) );
                                otvetusera= Convert.ToInt32(Console.ReadLine());
                                if (otvetusera == pravotvet)
                                {
                                    kolpravotvetov++;// proverka otveta
                                    Rt.Nmp = M1[i - 1].Nmp;
                                    Rt.Otvet = 1;
                                    Resultati.Add(Rt);
                                }
                            }
                            kriterij = 0; // вопрос
                            Console.Clear();
                            chetchik_voprosa = 0;
                           // b[0] = ' ';
                           // b = b.Remove(0, 1);
                            NVoprosa++;
                            Zamenai();
                            Console.WriteLine("{0}\r\n",b );
                            n++;
                            break;
                        }
                    case '+':
                        {
                            kriterij = 1;//правильний ответ
                           // Console.WriteLine("\r\n\r\n"); //для отладки (правильность) перед правильным ответом отступ
                                Console.WriteLine("\r\n");
                                chetchik_voprosa++;
                            pravotvet = chetchik_voprosa;
                           // b[0] = ' ';
                           // b = b.Remove(0, 1);
                            Zamenai();
                            Console.WriteLine("{0} {1}",chetchik_voprosa,b);
                            break;
                        }
                    case '-':
                        {
                            Console.WriteLine("\r\n");
                            kriterij = 2;//неправильний ответ
                            chetchik_voprosa++;
                            //b[0] = ' ';
                           // b = b.Remove(0, 1);
                            Zamenai();
                            Console.WriteLine("{0} {1}", chetchik_voprosa, b);
                            break;
                        }
                    default:
                        {
                            Zamenai();
                            Console.WriteLine("{0}", b);
                        }
                        break;
                }
                //F.close();	
            }
        }
        public void Result()
        {
            if (n == 1)
                Console.WriteLine("{0,10}+{1,10}-{2,10}%\r\n", kolpravotvetov,n - 1 - kolpravotvetov,kolpravotvetov * 100 / n);
            else
                Console.WriteLine("\r\nr\n Количество вопросов= {0} правильных ответов: {1}  оценка: {2} \r\n",n - 1,kolpravotvetov,((kolpravotvetov * 100) / (n - 1) + 19) / 20);
            pravotvet = -1; kolpravotvetov = 0; otvetusera = 0; //обнуление резульататов для повторного прохождения
            Console.ReadLine();
        }

        public void FResult()
        {
            fs = new FileStream("pfresult.txt", FileMode.Create, FileAccess.Write);
            outF = new StreamWriter(fs, Encoding.UTF8);
            if (data == String.Empty)
            {
                throw new Exception("Отсутсвуют данные для записи");
            }
            Console.WriteLine("Верніе\r\n");
            if (Resultati!=null)
                foreach (var it2 in Resultati)
                {
                    tempRt = it2;
                    Console.WriteLine("{0}\r\n",tempRt.Nmp);
                }
            Console.ReadLine();
            Console.Clear();
            for (int i = 1; i < RazmerStrok - 1; i++)
            {
                marker = false;
                if (Resultati!=null)
                    foreach(Resultat var in Resultati)
        
            {
                        if (var.Nmp == M1[i].Nmp)
                        {
                            marker = true;
                            continue;
                        }
                    }
                if (marker == false)
                {
                    b = hr[M1[i].Npp - 1];
                    Zamenai();
                    Console.WriteLine("{0}\r\n",  b);
                    outF.WriteLine(b + "\r\n");
                }

            }
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("CLS");
            outF.Close();
        }
        ~ManagerTKM()
        {
            if (hr != null)
            {
                hr.Clear();
            }
            if (M1 != null) M1=null;
            Console.WriteLine("Деструктор\r\n"); //для отладки
        }
    }
}
