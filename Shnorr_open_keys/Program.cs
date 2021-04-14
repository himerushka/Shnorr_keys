using System;
using System.Collections.Generic;
using System.Linq;
//using System;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shnorr_open_keys
{
    class Program
    {
        static void Main(string[] args)
        {
            //int  q, g, y;
            Console.Write("Введите простое число р: ");
            int p=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine($"Число p={p}");
            int[] mass = new int[1000];
            int j = 0;
            for (int i=50; i < 10000; i++)
            {
                if (((p - 1) % i) ==0) {
                    mass[j] = i;
                    Console.WriteLine($"Q{j}={mass[j]}");
                    j++;
                    }
            }
            Console.WriteLine();
            int q = 0;
            do
            {
                Console.WriteLine("Выберите Qj");
                Console.Write("j:");
                q = Convert.ToInt32(Console.ReadLine());
                if (q > 0 && q < 1000)
                {
                    q = mass[q];
                    Console.WriteLine($"q={q}");
                }
                else
                {
                    Console.WriteLine("Неверный индекс. Повторите попытку ввода.");
                    Console.WriteLine();
                    q = 0;
                }
            }
            while (q == 0);

            Array.Clear(mass, 0, 1000);
            Console.WriteLine();

            BigInteger g;
            j = 0;
            //g = BigInteger.ModPow(140,q,p);
            //g = Math.Pow(141.0, Convert.ToDouble(q));// % p;
            //Console.WriteLine(g);
            for (int i = 10; i < 200; i++)
            {
                g = BigInteger.ModPow(i, q, p);
                if (g == 1)
                {
                    mass[j] = i;
                    Console.WriteLine($"G{j}={mass[j]}");
                    j++;
                }
            }
            Console.WriteLine();
            
            g = 0;
            do
            {
                Console.WriteLine("Выберите Gj");
                Console.Write("j:");
                j = Convert.ToInt32(Console.ReadLine());
                if (j > 0 && j < 500)
                {
                    g = mass[j];
                    Console.WriteLine($"g={g}");
                }
                else
                {
                    Console.WriteLine("Неверный индекс. Повторите попытку ввода.");
                    Console.WriteLine();
                    g = 0;
                }
            }
            while (g == 0);

            Array.Clear(mass, 0, 1000);

            Console.WriteLine();
            int w = 0;
            do {
                try
                {
                    Console.WriteLine($"Введите закрытый ключ w такой, чтобы 0<w<q={q}");
                    Console.Write("W: ");
                    w = Convert.ToInt32(Console.ReadLine());
                    if (w<=0 || w>=q)
                    {
                        Console.WriteLine("Введен неверный ключ. Число должно быть в диапазоне (0;q).");
                        w = 0;
                    }
                }
                catch
                {
                    Console.WriteLine("Введен неверный ключ. Число должно быть натуральным.");
                } }
            while (w==0);
            
            Console.WriteLine();
            BigInteger y=BigInteger.Pow(g,w);
            j = 0;
            for (int i = 100; i < 100000; i++)
            {
                //y = BigInteger.Remainder(y*i, p);
                //Console.WriteLine("Вычисл. y=", y);
                if (((y * i) % p) == 1)
                {
                    mass[j] = i;
                    Console.WriteLine($"Y{j}={mass[j]}");
                    j++;
                }
            }
            Console.WriteLine();
            y = 0;

            do
            {
                Console.WriteLine("Выберите Yj");
                Console.Write("j:");
                j = Convert.ToInt32(Console.ReadLine());
                if (j >= 0 && j < 150) //откуда ограничение (0;50)??
                {
                    y = mass[j];
                    Console.WriteLine($"y={y}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неверный индекс. Повторите попытку ввода.");
                    Console.ResetColor();
                    Console.WriteLine();
                    y = 0;
                }
            }
            while (y == 0);
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Получены ключи");
            Console.ResetColor();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine($"Открытые:   p={p}     q={q}   g={g}   y={y}");
            Console.WriteLine($"Закрытый (пароль) = {w}");
            Console.WriteLine("-------------------------------------------------------------");

            //аутентификация:
            //проверка работоспособности ключей
            Random rnd = new Random();
            int r = rnd.Next(0, q);
            BigInteger x=0;
            x = BigInteger.ModPow(g, r, p);
            Console.WriteLine($"x={x}");
            int e = rnd.Next(0,Convert.ToInt32(Math.Pow(2, 20) - 1)); //2^t-1    t=20
            int s = Convert.ToInt32((r + w * e) % q); //не конвертируетсяЫ
            BigInteger xx = 0;
            xx = (BigInteger.Pow(g, s) * BigInteger.Pow(y, Convert.ToInt32(e))) % p;
            Console.WriteLine($"xx={xx}");
            if (x == xx)
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
            //
            Console.ReadKey();
        }
    }
}
