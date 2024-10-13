
using System;
namespace exercises_01
{
    public class BTVNne
    {
        static void Main(string[] args)
        {
            //BT01();
            //BT02();
            //BT03();
            //BT04();
            //BT05();
            //BT06();
            //BT07();
            //BT08();
            //BT09();
            BT10();
        }
        static void BT01()
        {
            int a, b;
            Console.Write("Nhap so thu nhat: ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhap so thu hai: ");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Tong hai so: {0} + {1} = {2}", a, b, a + b);
            Console.ReadKey();
        }
        static void BT02()
        {
            int c, d;
            Console.Write("Nhap so thu nhat: ");
            c = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhap so thu hai: ");
            d = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Gia tri truoc khi hoan doi: {0} va {1} ", c, d);
            c = c * d;
            Console.WriteLine("Gia tri sau khi hoan doi: {0} va {1} ", d, c / d);
            Console.ReadKey();
        }
        static void BT03()
        {
            double sonhat, sohai, a;
            Console.Write("Nhap so thu nhat: ");
            sonhat = Convert.ToDouble(Console.ReadLine());
            Console.Write("Nhap so thu hai: ");
            sohai = Convert.ToDouble(Console.ReadLine());
            Console.Write("Tich cua hai so thuc: " + sonhat * sohai);
            Console.ReadKey();

        }
        static void BT04()
        {
            double a, b;
            Console.Write(" Nhap so Feet: ");
            a = Convert.ToDouble(Console.ReadLine());
            b = a / 0.3048;
            b = Math.Round(b, 3);
            Console.Write("{0} Feet = {1} met ", a, b);
            Console.ReadKey();
        }
        static void BT05()
        {
            double c, f;
            Console.Write("Nhap do c: ");
            c = Convert.ToDouble(Console.ReadLine());
            f = c * 1.8 + 32;
            f = Math.Round(f, 3);
            Console.WriteLine("{0} do c = {1} do f ", c, f);
            Console.Write("Hay nhap do f:");
            f = Convert.ToDouble(Console.ReadLine());
            c = (f - 32) / 1.8;
            c = Math.Round(c, 3);
            Console.Write("{0} do f = {1} do c ", f, c);
            Console.ReadKey();
        }
        static void BT06()
        {
            Console.WriteLine("Size cua byte: {0}", sizeof(Byte));
            Console.WriteLine("Size cua sbyte: {0}", sizeof(sbyte));
            Console.WriteLine("Size cua ushort: {0}", sizeof(ushort));
            Console.WriteLine("Size cua int: {0}", sizeof(int));
            Console.WriteLine("Size cua uint: {0}", sizeof(UInt64));
            Console.WriteLine("Size cua long: {0}", sizeof(long));
            Console.WriteLine("Size cua ulong: {0}", sizeof(ulong));
            Console.WriteLine("Size cua char: {0}", sizeof(char));
            Console.WriteLine("Size cua bool: {0}", sizeof(bool));
            Console.WriteLine("Size cua float: {0}", sizeof(float));
            Console.WriteLine("Size cua double: {0}", sizeof(double));
            Console.WriteLine("Size cua decimal: {0}", sizeof(decimal));
            Console.ReadKey();
        }

        static void BT07()
        {
            Console.WriteLine(" Nhap: ");
            Console.Write("Gia tri ASCII cua ky tu char duoc nhap vao: {0}", Console.Read());
            Console.ReadKey();
            Console.ReadKey();
        }
        static void BT08()
        {
            double a;
            const double pi = 3.14;
            Console.Write("Nhap ban kinh hinh tron:");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Dien tich hinh tron: {0}", Math.Round(pi * a * a, 3));
            Console.ReadKey();
        }
        static void BT09()
        {
            double a;
            Console.Write("Nhap canh cua hinh vuong:");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Dien tich hinh vuong: {0}", Math.Round(a * a, 3));
            Console.ReadKey();
        }
        static void BT10()
        {
            int songay, y, w, d;
            Console.Write("Nhap so ngay: ");
            songay = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("So nam la:{0} ", songay / 365);
            Console.WriteLine("So tuan la: {0}", songay % 365 / 7);
            Console.WriteLine("So ngay la: {0}", songay % 365 % 7);
            Console.ReadKey();

        }
    }

}
