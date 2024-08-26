using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    internal class gamehihi
    { static void Main(string[]args)
        {
            Console.Write("Ban co 1000$");
            Console.WriteLine();
            int Tien = 1000;
            int j = 0;
            int thang = 0;
            int thua = 0;
            do
            {
                if (Tien<50)
                {
                    Console.WriteLine("Khong du tien");
                    break;
                }
                Tien -= 50;

                int sonhap, i;
                Random rd = new Random();
                int somay = rd.Next(1, 100);
                Console.Write(somay);
                   
                sonhap = 0;
                for (i = 1; i <= 5; i++) 
                {
                    Console.Write("Nhap so tu ban phim vao: ");
                    sonhap = int.Parse(Console.ReadLine());
                    if (somay == sonhap)
                    {
                        Tien += 100;
                        thang++;
                        Console.Write("Gioi qua hihi");
                        break;
                    }
                    else
                    if (sonhap > somay)
                        Console.Write("so ban nhap lon hon so may");
                    else
                        Console.Write("so ban nhap nho hon so may");
                }
                if (sonhap != somay)
                {
                    thua++;
                    Tien -= 50;
                }
                Console.WriteLine();
                Console.WriteLine("choi tiep khong Y/N");
                string chon = Console.ReadLine();
                if (chon.ToUpper().Equals("N"))
                { Console.Write("Tam biet");
                    j++;
                    break;
                }
            } while (true);
            Console.Write("So lan choi la:{0}", j);
            Console.WriteLine();
            Console.Write("So lan thang la {0}", thang);
            Console.WriteLine();
            Console.Write("So lan thua {0}", thua);
            Console.WriteLine();
            Console.Write("So tien con lai la: {0}", Tien);
            Console.ReadKey();



            }
    } 
}
