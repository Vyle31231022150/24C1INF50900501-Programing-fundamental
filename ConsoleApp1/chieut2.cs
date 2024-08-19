using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class chieut2
    {
        static void Main(string[] args)
        {//BT01();
           // BT02();
            BT03();
        }
        static void BT01()
        {
            Console.WriteLine();
            Console.WriteLine(" Nhap do Celuis: ");
            float C, F, K;
            C = Convert.ToSingle(Console.ReadLine());
            K = C + 273; ;
            F = (C* 18 /10)+ 32;
            Console.WriteLine(" {0} do Celuis = {1} do Kelvin", C, K);
            Console.WriteLine("{0} do Celuis = {1} do Fahrenhit", C, F);
            Console.ReadKey();
        }
        static void BT02()
        {
            Console.WriteLine();
            double r, s, v;
            float pi = 3.14f;
            Console.WriteLine(" Nhap radius squared: ");
            r = Convert.ToSingle(Console.ReadLine());
            s = 4 * pi * r *r;
            v = 4 / 3 * pi * r * r*r ;
            s = Math.Round(s, 3);
            v = Math.Round(v, 3);
            Console.WriteLine(" Surface la: {0}", s);
            Console.WriteLine(" volume la :{0}", v);
            Console.ReadKey();
        }
        static void BT03()
        {
            Console.WriteLine();
            double a, b;
            Console.WriteLine(" Nhap so thu nhat: ");
            a= Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" Nhap so thu hai: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" Tong hai so la: {0} ", a+b);
            Console.WriteLine(" Hieu hai so la: {0} ", a-b);
            Console.WriteLine(" Tich hai so la: {0} ", a*b);
            Console.WriteLine(" Thuong hai so la: {0} ", a/b);
            Console.WriteLine(" Chia lay so du 2 so la: {0} ", a%b);
        }
    }
}

