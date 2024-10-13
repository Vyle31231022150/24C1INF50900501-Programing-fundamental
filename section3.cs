@@ -0,0 + 1,114 @@
using System;
using System.Net.NetworkInformation;
namespace Session03
{
    internal class Baitap
    {
        public static void Main(string[] args)
        {
            //baitap01();
            //baitap02();
            //baitap03();
            //string01();
            //string02();
            //baitap05();
            //baitap06();
            baitap07();
        }
        static void baitap01()
        {
            Console.WriteLine("Nhap nhiet do co don vi do C: ");
            float a = Convert.ToSingle(Console.ReadLine());
            float b = (9 / 5) * a + 32;
            float c = a + 273;
            Console.WriteLine($"Vay {a} do C tuong duong voi {b} do F va {c} do K");
        }
        static void baitap02()
        {
            Console.WriteLine("Nhap ban kinh hinh cau: ");
            float r = Convert.ToSingle(Console.ReadLine());
            double pi = Math.PI;
            double surface = 4 * pi * Math.Pow(r, 2);
            double volume = (4 / 3) * pi * Math.Pow(r, 3);
            Console.WriteLine($"Dien tich xung quanh hinh cau la {surface}, the tich hinh cau la {volume}");
        }
        static void baitap03()
        {
            Console.WriteLine("Nhap so a va b: ");
            int a = Convert.ToInt32(Console.ReadLine());
            int b = Convert.ToInt32(Console.ReadLine());
            int c = a + b;
            int d = a - b;
            int e = a * b;
            float f = a / b;
            int g = a % b;
            Console.WriteLine($"{a} + {b} = {c}");
            Console.WriteLine($"{a} - {b} = {d}");
            Console.WriteLine($"{a} * {b} = {e}");
            Console.WriteLine($"{a} / {b} = {f}");
            Console.WriteLine($"{a} mod {b} = {g}");
        }
        static void string01()
        {
            Console.WriteLine("input the string: ");
            string a = Console.ReadLine();
            Console.WriteLine($"the string you entered is:{a} ");
        }
        static void string02()
        {
            Console.WriteLine("input the string: ");
            string a = Console.ReadLine();
            int b = 0;
            foreach (char chr in a)
            {
                b += 1;
            }
            Console.WriteLine($"length of the string is : {b}");
        }
        static void baitap05()
        {
            Console.WriteLine("x = y^2 - 2y + 1");
            int x, y;
            for (y = -5; y <= 5; y++)
            {
                x = y * y + 2 * y + 1;
                Console.WriteLine($"y = {y}, x = {y}^2+2*{y}+1={x}");
            }
        }
        static void baitap06()
        {
            Console.WriteLine("Nhap do dai quang dong voi don vi km: ");
            float km = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Nhap khoang thoi gian (gio):");
            float hour = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Nhap khoang thoi gian (phut):");
            float min = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Nhap khoang thoi gian (giay):");
            float sec = Convert.ToSingle(Console.ReadLine());
            float kmh = km / (hour + min / 60.0f + sec / 3600.0f);
            float mh = kmh / 1.609f;
            Console.WriteLine($"Toc do cua ban la {kmh} km/h");
            Console.WriteLine($"Toc do cua ban la {mh} mile/h");

        }
        static void baitap07()
        {
            char symbol;
            Console.Write("Input a symbol: ");
            symbol = Convert.ToChar(Console.ReadLine());
            if ((symbol == 'a') || (symbol == 'e') || (symbol == 'i') ||
                (symbol == 'o') || (symbol == 'u'))
            {
                Console.WriteLine("It's a lowercase vowel.");
            }
            else if ((symbol >= '0') && (symbol <= '9'))
            {
                Console.WriteLine("It's a digit.");
            }
            else
            {
                Console.Write("It's another symbol.");
            }
        }
    }
}