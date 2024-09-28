
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khoqua
{
    internal class Jagged_Array_Exercise02
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number of row: ");
            int rows = int.Parse(Console.ReadLine());
            int[][] a = new int[rows][];
            while (true)
            {
                int sel = menu();
                switch (sel)
                {
                    case 0: Console.WriteLine("bye");return;
                    case 1: UserDaTa(a, rows); break;
                    case 2: Random_DaTa(a, rows); break;
                    case 3: Print(a, rows);break;
                    case 4: max(a);break;
                    case 5: Sort_Row(a);break;
                    case 6: PrimeOfArray(a); break;
                    case 7: Search(a);break;

                }

            }
            static int menu()
            {
                Console.WriteLine("1 - UserData");
                Console.WriteLine("2 - RandomData");
                Console.WriteLine("3 - Print");
                Console.WriteLine("4 - Max");
                Console.WriteLine("5 - Sort");
                Console.WriteLine("6 - Prime");
                Console.WriteLine("7 - Position");
                Console.WriteLine("0 - Exit");
                int sel;
                while (true)
                {
                    bool c = int.TryParse(Console.ReadLine(), out sel);
                    if (c && sel >= 0 && sel <= 7)
                        break;
                    Console.WriteLine("Enter Number again");
                }
                return sel;
            }
            static void UserDaTa(int[][] a, int rows)
            {
                for (int i = 0; i < rows; i++)
                {
                    Console.Write($"Enter the Number Of Colums Of the Row {i}: ");
                    int cols = int.Parse(Console.ReadLine());
                    a[i] = new int[cols];
                    for (int j = 0; j < a[i].Length; j++)
                    {
                        a[i][j] = int.Parse(Console.ReadLine());
                    }
                }

            }
            static void Random_DaTa(int[][] a, int rows)
            {
                Random rnd = new Random();
                for (int i = 0; i < rows; i++)
                {
                    Console.Write($"Enter the Number Of Colums Of the Row {i}: ");
                    int cols = int.Parse(Console.ReadLine());
                    a[i] = new int[cols];
                    for (int j = 0; j < a[i].Length; j++)
                        a[i][j] = rnd.Next(10, 50);

                }
            }
            static void Print(int[][] a, int rows)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < a[i].Length; j++)
                    {
                        Console.Write(a[i][j] + " ");
                    }
                    Console.WriteLine();
                }
            }
            static void max(int[][] a)
            {
                int M = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    int m = 0;
                    for (int j = 0; j < a[i].Length; j++)
                    {
                        if (a[i][j] > M)
                            M = a[i][j];
                        if (a[i][j] > m)
                            m = a[i][j];
                    }
                    Console.Write($"The Max in row {i} is: {m}");
                    Console.WriteLine();
                }

                Console.Write($"The Max in array is: {M}");
                Console.WriteLine();
            }
            static void Sort_Arrray(int[] a)
            {
                int temp;
                for (int i = 0; i < a.Length; i++)
                {
                    for (int j = 0; j < a.Length; j++)
                    {
                        if (a[i] > a[j])
                        {
                            temp = a[i];
                            a[i] = a[j];
                            a[j] = temp;
                        }
                    }
                }
                for (int i=0; i<a.Length;i++)
                    Console.Write(a[i] + " ");

            }
            static void Sort_Row(int[][] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    Sort_Arrray(a[i]);
                    Console.WriteLine();
                }
            }
            static bool Check(int input)
            {
                int d = 0;
                for (int j = 1; j <= input; j++)
                    if (input% j == 0)
                        d++;
                if (d == 2) return true;
                return false;
            }
            static void PrimeOfArray(int[][] a)
            {
                bool c;
                for (int i = 0; i < a.Length; i++)
                    for (int j = 0; j < a[i].Length; j++)
                    {
                        c = Check(a[i][j]);
                        if (c == true)
                            Console.WriteLine($"{a[i][j]} appears at row [{i}], col [{j}]");
                    }
            }
            static void Search(int[][] a)
            {
                Console.Write("Enter Value To Search: ");
                int val = int.Parse(Console.ReadLine());
                int count = 0;
                int leng=0;
                for (int i = 0; i < a.Length; i++)
                    for (int j = 0; j < a[i].Length; j++)
                    {
                        leng++;
                        if (a[i][j] == val)
                            Console.WriteLine($"{val} appear at row {i} and colum {j}");
                            else count++;
                        
                    }
                if (count == leng)
                    Console.WriteLine($"{val} dont appear in array");

            }



        }



    }

}


