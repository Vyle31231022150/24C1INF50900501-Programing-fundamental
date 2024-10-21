using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.Drawing;
using static Game.Program;
using System.Net.NetworkInformation;

namespace Game
{
    //Program chính
    public class Program
    {
        static int width = 50, height = 37;
        private static bool isPause;
        //private static bool isResume;
        //private static bool score;
        //private static string highestScore;
        //private static string level;
        static Point user = new Point(0, 0);
        static Point head_user = new Point(0, 0);
        static Point fish = new Point(0, 0);
        static Point plastic_bag = new Point(0, 0);
        static Point glass_bottle = new Point(0, 0);
        static Point block = new Point(0, 0);
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Map.DrawInstructions(width, height);
            Map.DrawMap(width, height);
            Thread t1 = new Thread(() =>
            {
                User.Info_User();
            });
            Thread t2 = new Thread(() => {
                Thread.Sleep(10);
                   Block.BlockImage();
                });
            t1.Start();
            t2.Start();



           // User.Info_User();
            // Console.SetCursorPosition(0, 27);
            //Console.WriteLine("-----------------------------------------------");
             //Block.BlockImage();
            //   Trash.Fish();
            // Trash.Plastic_bag();
            //Trash.Glass_Bottle();
            //Method.Nguoi_Cham_Chuong_Ngoai_Vat();
            //Console.ReadLine();
           

        }
        //Map 
        public class Map
        {
           public static void DrawMap(int width, int height)
            {
                for (int i = 0; i <= height; i++)
                {
                    for (int j = 0; j <= width * 2; j++)
                    {
                        // Khung ngoài
                        if (i == 0)//khung trên
                        {
                            Console.Write("▄");
                        }
                        else if (i == height)//khung dưới
                        {
                            Console.Write("▀");
                        }
                        else if (j == 0)//khung trái
                        {
                            Console.Write("▌");
                        }
                        else if (j == width * 2)//khung phải
                        {
                            Console.Write("▐");
                        }
                        // Đường line chia ngang 1
                        else if (i == height / 3)
                        {
                            Console.Write("─");
                        }
                        // Đường line chia ngang 2
                        else if (i == 2 * height / 3)
                        {
                            Console.Write("─");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
            }// Đã fix
           public static void DrawInstructions(int height, int width)
            {
                string textMove = "Phím ↑ ↓: Di chuyển lên xuống";
                int x = 1, y = 1;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(x, y);
                Console.WriteLine(textMove);

            } //Đã fix
        }

        // Người chơi
        public class User
        {
            public static void Info_User()
            {
                string[] output = new string[]
                    {
@"    ▄▀▀█        ",
@"   █   █▀▀▀█    ",
@" ██    ▀   ▐█   ",
@" ██▄▄▄▄▄▄▄▄▄▄█▀ ",
@" ██          █▄ ",
@"▄▀█  ▐▌  █   ██ ",
@"▐▄▄▌   ▄▄    █  ",
@"  █         █   ",
@"   ▀▀█▄▄█▀▀     ", };


                user.X = 3;
                user.Y = 2 * height / 3 + 5;
                // In đầu ra ban đầu  
                Method.Print(user.X, user.Y, output);
                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true để không in ra phím  
                    int truonghop = 0;

                    if (keyInfo.KeyChar == 'w')
                        truonghop = 1; // Di chuyển lên  
                     if (keyInfo.KeyChar == 's')
                        truonghop = 2; // Di chuyển xuống
                    if (keyInfo.KeyChar == 'q')
                        truonghop = 3;

                    Method.Clear(user.X, user.Y, output); // Xóa đầu ra cũ 
                                                          // Duy chuyển và in lại  
                    if (truonghop == 1 && user.Y > 8)  // Nếu không vượt quá giới hạn trên  
                    {
                        user.Y -= 12;

                    }
                    if (truonghop == 2 && user.Y < 2 * height / 3 + 5 - 8) // Nếu không vượt quá giới hạn dưới  
                    {
                        user.Y += 12;

                    }
                    if (truonghop == 3)
                    {
                        break;

                    }

                    // In lại đầu ra mới  
                    Method.Print(user.X, user.Y, output);
                }
                

            }
        }
            // Các loại rác
            public class Trash
            {

                //Rác cá
                public static void Fish()
                {
                    Random rnd = new Random();
                    int[] valueY = { 0, 10, 20 };
                    string[] output = new string[]
                            {
@"     ▄▄    ▄   ▄",
@"   ▄███▄▄▄█▄▄▄█▀",
@"    ▀██   ▀▄  ▀█",
                            };
                    fish.X = rnd.Next(20, 90);
                    fish.Y = valueY[rnd.Next(valueY.Length)];
                    Method.Print(fish.X, fish.Y, output);
                    Method.Move(ref fish);
                    if (Method.Phan_Loai_Rac() == 1 )
                        Method.Clear(fish.X+1, fish.Y, output);
                }
                public static void Plastic_bag()
                {
                    Random rnd = new Random();
                    int[] valueY = { 0, 10, 20 };
                    string[] output = new string[]
                            {
                    @"   █ █   ▐ █  ",
                    @"  █  █  ▄█ ▐▌ ",
                    @"  █   ▀▀▀   █ ",
                    @"  ▀█▄▄    ▄█  ",
                    @"     ▀▀▀▀▀▀   ",
                            };
                    int x = rnd.Next(20, 90);
                    int y = valueY[rnd.Next(valueY.Length)];
                    Method.Print(x, y, output);
                    Method.Move(ref plastic_bag);
                if (Method.Phan_Loai_Rac() == 2)
                        Method.Clear(x, y, output);
                }
                public static void Glass_Bottle()
                {
                    Random rnd = new Random();
                    int[] valueY = { 0, 10, 20 };
                    string[] output = new string[]
                                {
@"   █▀█  ",
@"  █▀ ▀█ ",
@"  █   █ ",
@"  █   █ ",
@"  ▀▀▀▀▀ ",
                                };
                    int x = rnd.Next(20, 90);
                    int y = valueY[rnd.Next(valueY.Length)];
                    Method.Print(x, y, output);
                Method.Move(ref glass_bottle);
                if (Method.Phan_Loai_Rac() == 3 )
                        Method.Clear(x, y, output);
                }
            }
            // Các hàm xử lý
            public class Method
            {
                //In ra màn hình
                public static void Print(int x, int y, string[] output)
                {

                    for (int i = 0; i < output.Length; i++)
                    {
                        Console.SetCursorPosition(x, y + i);// Thiết lập vị trí con trỏ  
                        Console.WriteLine(output[i]);
                    }
                }
                // Xóa những hình cũ
                public static void Clear(int x, int y, string[] output)
                {
                    for (int i = 0; i < output.Length; i++)
                    {
                        for (int k = 0; k < output[i].Length; k++)
                        {
                            Console.SetCursorPosition(x + k, y + i);// Thiết lập vị trí con trỏ  
                            Console.WriteLine(' ');

                        }
                    }

                }
            // Di chuyển
            public static void Move(ref Point input)
            {
                input.X-=1;
            }
            //Check va chạm với người dùng với rác và chướng ngọai vật 
            public static bool Check_va_cham_voi_nguoi_dung(Point head_user, Point input)
                {
                 head_user.X = user.X + 5;
                if (head_user.X == input.X)
                    return true;
                else return false;

                }
            //check để cho không in trùng tọa đọ cnv và rác 
            public static bool DK_khong_in_trung_cnvvarac(Point fish, Point plastic_bag, Point glass_bottle, Point block)
            {
                if (
                   fish.X == plastic_bag.X && fish.Y == plastic_bag.Y ||
                   fish.X == glass_bottle.X && fish.Y == glass_bottle.Y ||
                   fish.X == block.X && fish.Y == block.Y ||
                   plastic_bag.X == glass_bottle.X && plastic_bag.Y == glass_bottle.Y ||
                   plastic_bag.X == block.X && plastic_bag.Y == block.Y ||
                   glass_bottle.X == block.X && glass_bottle.Y == block.Y
                 )
                    return false;
                else return true;
            }

                public static int Phan_Loai_Rac()
                {
                    int truonghop = 0;
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true để không in ra phím
                    if (keyInfo.KeyChar == '1')
                        truonghop = 1;
                    if (keyInfo.KeyChar == '2')
                        truonghop = 2;
                    if (keyInfo.KeyChar == '3')
                        truonghop = 3;
                    return truonghop;

                }
            public static void Nguoi_Cham_Chuong_Ngoai_Vat()
            {
                if (Check_va_cham_voi_nguoi_dung(head_user, block))
                    Console.WriteLine("Game Over");

            }
            }

            public class Point
            {
                private int x;
                private int y;

                public int X { get => x; set => x = value; }
                public int Y { get => y; set => y = value; }

                public Point(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }
               // public bool IsEqual(Point other)
                //{
                  //  return this.X == other.X && this.Y == other.Y;
                //}


            }

          public class Block
            {
                public static void BlockImage()
                {
                   
                    string[] BlockImage =
                    {   @" ████████ ",
                @" ████████ ",
                @" ████████ ",
                @" ████████ ",
                @" ████████ ",
                @" ████████ "
                };

                block.X = width*2 - BlockImage[0].Length;
                block.Y = 20;
                Move( ref  block, BlockImage);
                Clear(block.X, block.Y, BlockImage);

            }



            static void Print(int x, int y, string[] name)
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        Console.SetCursorPosition(x, y + i);
                        Console.WriteLine(name[i]);
                    }
                }
                static void Clear(int x, int y, string[] name)
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        Console.SetCursorPosition(x, y + i);
                        Console.WriteLine("");
                    }
                }
            static void Move(ref Point block, string[] print)
            {
                while (block.X > 0) // Di chuyển vật thể sang trái cho đến khi đến biên
                {
                    Block.Clear(block.X, block.Y, print); // Xóa màn hình
                    Print(block.X,block.Y, print);//in đầu ra mới 
                    block.X--; // Giảm x để vật thể dịch chuyển sang trái
                    Thread.Sleep(100); // Tạm dừng để điều chỉnh tốc độ di chuyển
                }




            }
         }

    }
}
