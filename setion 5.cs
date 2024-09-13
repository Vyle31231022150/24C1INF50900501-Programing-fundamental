using System;
namespace BTVN909
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Security;
    using System.Text;
    using System.Threading.Tasks;
    public class setion5
    {

        static void Main(string[] args)
        {
            //BT01a();// Max cua 3 so
            //BT01b();// Max cua n so va co it nhat 1 so
            //BT02();// Giai thua 1 so nguyen duong
            //BT03();// Kiem tra so nguyen to
            //BT04();// Tat ca so nguyen to nho hon n,so nguyen to dau tien
            //BT05();// So hoan hao
            BT06();//
        }

        static void BT01a()
        {
            int a, b, c;
            a = Convert.ToInt16(Console.ReadLine());
            b = Convert.ToInt16(Console.ReadLine());
            c = Convert.ToInt16(Console.ReadLine());
            int M = Max(a, b, c);
            Console.WriteLine($"Max is {M}");

            static int Max(int a, int b, int c)
            {
                return Math.Max(Math.Max(a, b), c);

            }
            Console.ReadKey();
        }
        static void BT01b()
        {

            int M = Max(1);
            Console.WriteLine(M);
            int M1 = Max(1, 2, 3, 4);
            Console.WriteLine(M1);
            int M2 = Max(1, 5, 6, 7);
            Console.WriteLine(M2);
            static int Max(int a, params int[] mang)
            {
                if (mang.Length == 0)
                    return a;
                int m = mang[0];
                foreach (int i in mang)

                {

                    if (i > m)
                        m = i;
                }
                return Math.Max(a, m);

            }
            Console.ReadKey();
        }
        static void BT02()
        {
            int n;

            Console.WriteLine("Enter Number: ");
            n = Convert.ToInt32(Console.ReadLine());
            long f = factorial(n);
            Console.WriteLine($"Factorial is: {f}");
            static long factorial(int n)
            {
                int f = 1;
                for (int i = 1; i <= n; i++)
                {
                    f *= i;
                }
                return f;
            }
            Console.ReadKey();
        }
        static void BT03()
        {
            int n;
            Console.WriteLine("Enter Number: ");
            n = Convert.ToInt32(Console.ReadLine());
            bool c = check(n);
            if (c == true)
                Console.WriteLine($"{n} is prime number");
            else Console.WriteLine($"{n} is not prime number");

            static bool check(int n)
            {
                int count = 0;
                for (int i = 1; i <= n; i++)
                {
                    if (n % i == 0)
                        count++;
                }
                if (count == 2)
                    return true;
                else return false;
            }

        }
        static void BT04()
        {
            int n;
            Console.WriteLine("Enter Number: ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Print Prime Number Under N:");
            static bool check(int n)
            {
                int count = 0;
                for (int i = 1; i <= n; i++)
                {
                    if (n % i == 0)
                        count++;
                }
                if (count == 2)
                    return true;
                else return false;
            }
            static void PrimeNumberUnderN(int n)
            {
                for (int i = 1; i < n; i++)
                {
                    bool c = check(i);
                    if (c == true)
                        Console.WriteLine(i);
                }
            }
            static void TheFristNPrimeNumber(int n)
            {
                bool c;
                for (int i = 1; i <= n; i++)
                {
                    c = check(i);
                    if (c == true)
                    {
                        Console.WriteLine($"The First N Prime Number is{i}");
                        break;
                    }
                }
            }
            PrimeNumberUnderN(n);
            TheFristNPrimeNumber(n);
            Console.ReadKey();

        }
        static void BT05()
        {

            static bool check(int n)
            {
                int sum = 0;
                for (int i = 1; i < n; i++)
                    if (n % i == 0)
                        sum += i;
                if (sum == n)
                    return true;
                else return false;
            }
            Console.WriteLine("Perfect Number Under 1000 is:");
            for (int i = 1; i < 1000; i++)
            {
                if (check(i) == true)
                    Console.WriteLine(i);

            }
            Console.ReadKey();
        }
        static void BT06()
        {

            Console.WriteLine("Enter string: ");
            string str = Console.ReadLine();
            str = str.ToLower();
            bool Result = CHECK(str);
            if (Result == true)
                Console.WriteLine("This is Pangram");
            else Console.WriteLine("This is not pangram");
            static bool CHECK(string input)
            {
                bool[] check = new bool[26];
                foreach (char u in input)
                    for (char a = 'a'; a <= 'z'; a++)
                    {
                        if (u == a)
                        {
                            int dau = u - 'a';
                            check[dau] = true;
                        }
                    }
                foreach (bool c in check)
                    if (c == false)
                        return false;
                return true;
            }
            Console.ReadKey();
        }
    }
}