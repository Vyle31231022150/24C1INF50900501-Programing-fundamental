using System;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BT_26_thang_8
{
	public class BTVN
	{ static void Main(string[] args)
		{
			//BT01();//So le va so chan
			//BT02();//So lon nhat trong 3 so
			//BT03();//Xac dinh goc phan tu trong he x,y
			//BT04();//Xac dinh tam giac can, deu, vuong
			//BT05();//Tinh trung binh cong va tong cua 10 so duoc nhap tu ban phim
			//BT06();//Bang cuu chuong cua mot so nguyen cho truoc
			//BT07();//Xuat hinh tam giac voi mot so
			//BT08();//Chuoi dieu hoa va tong cua no
			//BT09();//So hoan hao
			BT10();//So nguyen to
        }

		static void BT01()
		{
			int a;
			Console.WriteLine("Enter number a: ");
			a = Convert.ToInt32(Console.ReadLine());
			if (a % 2 == 0)
				Console.WriteLine("a is even number ");
			else Console.WriteLine("a is odd number ");
			Console.ReadKey();
		}
		static void BT02()
		{
            float a,b,c,max;
            Console.WriteLine("Enter number a: ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number b: ");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number c: ");
            c = Convert.ToInt32(Console.ReadLine());
			if (a > b)
				 max = a;
			else
				max = b;
			if (c > max)
			{
				max = c;
				Console.WriteLine("The largets of three number is: {0}", max);
			}
			else
				Console.WriteLine("The largets of three number is: {0}", max);

			Console.ReadKey();
        }
		static void BT03()
		{
            int x, y;
            Console.WriteLine("Enter number x: ");
            x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number y: ");
			y = Convert.ToInt32(Console.ReadLine());
			if (x > 0)

				if (y > 0)
					Console.WriteLine("first quadrant");
				else Console.WriteLine("fourth quadrant");
			else if (y>0)
                Console.WriteLine("second quadrant");
			    else Console.WriteLine("third quadrant");
			Console.ReadKey();
        }
		static void BT04()
		{
			
			float AB, BC, AC, tong, hieu;
			do
			{
				Console.WriteLine("Enter the AB side in the ABC triangle: ");
				AB = Convert.ToSingle(Console.ReadLine());
				Console.WriteLine("Enter the BC side in the ABC triangle: ");
				BC = Convert.ToSingle(Console.ReadLine());
				Console.WriteLine("Enter the AC side in the ABC triangle: ");
				AC = Convert.ToSingle(Console.ReadLine());
				tong = BC + AC;
				hieu = Math.Abs(BC - AC);
				Console.WriteLine("Check if it is a triangle or not");
				if (tong > AB)
					if (AB > hieu)
					{
						Console.WriteLine("This is a triangle");
						break;

					}
					else
					{
						Console.WriteLine("This is not a triangle");

					}

				else
				{
					Console.WriteLine("This is not a triangle");
				}
				Console.WriteLine("Enter the side in the triangle again");

			} while (true);
			if (AB==AC)
                if (AB != BC)
					if (AB*AB + AC*AC ==BC*BC)
                        Console.WriteLine("This is an isosceles right triangle at A");
			        else Console.WriteLine("This is an isosceles triangle at A");
            if (AB == BC)
                if (AB != AC)
                    if (AB * AB + BC * BC == AC * AC)
                        Console.WriteLine("This is an isosceles right triangle at B");
                    else Console.WriteLine("This is an isosceles triangle at B");
            if (BC == AC)
                if (AB != BC)
                    if (BC * BC + AC * AC == AB * AB)
                        Console.WriteLine("This is an isosceles right triangle at C");
			        else Console.WriteLine("This is an isosceles triangle at C");
			if (AB * AB + AC * AC == BC * BC)
				Console.WriteLine("This is a right triangle at A");
            if (AB * AB + BC * BC == AC * AC)
                Console.WriteLine("This is a right triangle at B");
            if (BC * BC + AC * AC == AB * AB)
                Console.WriteLine("This is a right triangle at C");
            if (AB == BC)
				  if (AB == AC)
					Console.WriteLine("This is an equilateral triangle");
			Console.ReadKey();
        }
		static void BT05()
		{
			int i;
			int sum = 0;
			float average;
			int[] j = new int[11];
			for (i = 1; i <= 10; i++)
			{
				Console.WriteLine("Enter number {0}:",i);
                j[i] = int.Parse(""+Console.ReadLine());
				sum += j[i];

			}
			average = sum / (i-1);
			Console.WriteLine("Average of 10 numbers is: {0}", average);
            Console.WriteLine("Sum of 10 numbers is: {0}", sum);
        }
		static void BT06()
		{
			int i,number,mul;
			Console.Write("Enter a integer to display the multiplication table: ");
			number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Multiplication table of a integer {number}: ");
            for (i=1; i<=10; i++)
			{
				mul = number * i;
				Console.WriteLine($"{number} x {i} = {mul}");

			}
			Console.ReadKey();
		}
		static void BT07()
		{
			int number;
			Console.Write("Enter number: ");
			number = Convert.ToInt32(Console.ReadLine());
			for (int i = 1; i <= number; i++)
			{ 
				for (int j = 1; j <= i; j++)
					Console.Write($" {j}");
                Console.WriteLine();
            }
            Console.WriteLine();
            int b = 1;
			for (int i = 1; i <= number; i++)
			{
			
				for (int j = 1; j <= i; j++)
				{
					Console.Write($" {b}");
				     b++;
                   
                }
                Console.WriteLine();
            }
			
			b = 1;
			string t = " ";
            for (int i = 1; i <= number; i++)
            {
				for (int c = 1; c <= number - i; c++)
					Console.Write(t);

                for (int j = 1; j <= i; j++)
                { 
                    Console.Write($" {b}");
                    b++;
                }
                Console.WriteLine();
            }


            Console.ReadKey();
		}
        static void BT08()
        {
			int number;
			double sum = 0d;
			Console.Write("Enter number: ");
            number = Convert.ToInt32(Console.ReadLine());
			for (int i = 1; i <= number; i++)
			{
				Console.Write($"1/{i} ");
				sum+= 1d/ i;

			}
			Console.WriteLine($"Sum = {sum}");
			Console.ReadKey();

        }
		static void BT09()
		{
			int number;
			int sum = 0;
			Console.WriteLine("Enter Number: ");
			number = Convert.ToInt32(Console.ReadLine());
			for (int i = 1; i <= number; i++)
			{
				if (number % i == 0)
					sum += i;

			}
			if (sum == 2*number)
				Console.WriteLine($"{number} is perfect number");
			else Console.WriteLine($"{number} is not perfect number");
			Console.ReadKey();
        }
		static void BT10()
		{
			int number;
			int count=0;
            Console.WriteLine("Enter Number: ");
            number = Convert.ToInt32(Console.ReadLine());
			for (int i=1; i<= number; i++)
			{
				if (number % i == 0)
					count++;
			}
			if (count==2)
				Console.WriteLine($"{number} is prime number");
			else Console.WriteLine($"{number} is not prime number");
            Console.ReadKey();

		}
    }

}

