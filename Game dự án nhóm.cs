using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters;
using System.Net.Http.Headers;
using System.Drawing;
using static Game.Program;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Collections.Generic;



namespace Game
{
    //Program chính
    public class Program
    {
        static int width = 50, height = 27;
        public static object consoleLock = new object();
        private static bool isPause;
        //private static bool isResume;
        //private static bool score;
        //private static string highestScore;
        //private static string level;
        static Point namegame = new Point(0, 0);
        static Point huongdan = new Point(0, 0);
        static Point inputnamebox = new Point(0, 0);
        static Point game = new Point(0, 0);
        static Point bang = new Point(0, 0);
        static Point user = new Point(0, 0);
        public static List<Point> occupiedPositions = new List<Point>();
        static Point fish = new Point(0, 0);
        static Point plastic_bag = new Point(0, 0);
        static Point glass_bottle = new Point(0, 0);
        static Point block = new Point(0, 0);
        static Point head_user = new Point(0, 0);
        public static Point Head_user { get => head_user; set => head_user = value; }
        static int score = 0;
        private static bool dK = true;
        public static bool DK { get => dK; set => dK = value; }
        static bool again = true;
        static bool running = true;



        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Codinhkhung();
            Map.NameGame();
            Console.Clear();
            Codinhkhung();
            Map.InputNameBox();
            Console.Clear();
            while (running)
            {
                // Reset trạng thái game
                score = 0;
                DK = true; // Cờ để điều khiển vòng lặp game
                again = true; // Cờ để điều khiển restart
                Codinhkhung();
                Console.Clear();
                Map.DrawMap(width, height);
                for (int i = 0; i < 10; i++)
                {
                    Thread t1 = new Thread(() => { User.Info_User(); });
                    t1.Start();t1.Priority = ThreadPriority.Highest;
                    Thread t2 = new Thread(() => { Object.BlockImage(); });
                    t2.Start();
                    Thread t3 = new Thread(() => { Object.Fish(); });
                    t3.Start();
                    Thread t4 = new Thread(() => { Object.Plastic_bag(); });
                    t4.Start();
                    Thread t5 = new Thread(() => { Object.Glass_Bottle(); });
                    t5.Start();
                    t1.Join();
                    t2.Join();
                    t3.Join();
                    t4.Join();
                    t5.Join();

                }
                Console.Clear();
                bool restart = Map.GameOver();
                // Nếu người chơi chọn restart, chạy lại game
                if (restart)
                {
                    Console.Clear();
                    continue;  // Quay lại đầu vòng lặp để bắt đầu lại game
                }
                else
                {
                    running = false;  // Thoát vòng lặp và kết thúc game
                }

            }
            //Map.BangXepHang();







        }
        static void Codinhkhung()
        {
            lock (consoleLock)
            {
                try
                {
                    // Kiểm tra kích thước của Buffer  
                    if (width != Console.BufferWidth || height != Console.BufferHeight)
                        throw new Exception();
                }
                catch
                {
                    // Cập nhật kích thước Buffer  

                    Console.Clear();

                    // Kiểm tra kích thước cửa sổ console  
                    if (Console.BufferWidth < 120 || Console.BufferHeight < 30)
                    {
                        string message = "Kích thước cửa sổ quá nhỏ!";
                        // In thông điệp ở giữa màn hình nếu có đủ không gian  
                        if (Console.BufferWidth > message.Length)
                        {
                            Console.SetCursorPosition((Console.BufferWidth - message.Length) / 2, Console.BufferHeight / 2);
                            Console.Write(message);

                        }
                    }

                    // Chờ cho đến khi kích thước cửa sổ được điều chỉnh đủ lớn  
                    while (Console.BufferWidth < 120 || Console.BufferHeight < 30)
                    {
                        // Có thể in một thông báo hoặc xử lý tín hiệu từ người dùng  
                    }
                }
            }
        }

        //static bool isHit(Point head_user)
        //{
        //    if (head_user.IsHit(trash)) return true;

        //    return false;
        //}


        //Kiểm tra thức ăn có trùng với vật cản động không
        //static bool IsCollideBlocks(Point food)
        //{
        //    foreach (HazardBlock block in blocks)
        //    {
        //        if (food.IsEqual(block.Block)) return true;

        //    }
        //    return false;
        //}

        #region ScoreBoard
        static void countScore(Point objects)
        {

            if (Head_user.IsHit(objects) == true)
                score += 10;
            else
                score -= 15;
            if (score<0) DK = false;



        }
        public static void SaveScore(string playerName, int score)
        {
            string filePath = "scores.txt";
            string scoreEntry = $"{playerName},{score}";
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(scoreEntry);
            }
        }


        public static void Scoreboard()
        {
            // Đọc file điểm và lưu vào mảng
            string filePath = "scores.txt";
            var lines = File.ReadAllLines(filePath);

            // Tạo mảng 2 chiều để lưu tên và điểm
            string[,] scores = new string[lines.Length, 2];


            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int nscore))
                {
                    scores[i, 0] = parts[0];          // Lưu tên vào cột 0
                    scores[i, 1] = nscore.ToString();  // Lưu điểm vào cột 1
                }
            }


            int max = 0;
            string topPlayer = "";

            for (int i = 0; i < scores.GetLength(0); i++)
            {
                if (int.TryParse(scores[i, 1], out int scoreCheck) && scoreCheck > max)
                {
                    max = scoreCheck;
                    topPlayer = scores[i, 0];
                }
            }

            // Xuất ra mảng để kiểm tra

            // Xác định chiều cao bảng động
            int dynamicHeight = Math.Max(6, scores.GetLength(0) + 4);  // 4 dòng thêm vào phần khung

            // Tạo khung bảng động
            string topBorder = @"┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓";
            string title = @"                                SCOREBOARD                                  ";
            string separator = @"┃━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┃";
            string bottomBorder = @"┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛";

            // Căn giữa bảng theo chiều rộng console
            int xPosition = (Console.BufferWidth - topBorder.Length) / 2;
            int yPosition = (Console.BufferHeight - dynamicHeight) / 2;

            // In khung trên và tiêu đề
            Console.SetCursorPosition(xPosition, yPosition);
            Console.WriteLine(topBorder);
            Console.SetCursorPosition(xPosition, yPosition + 1);
            Console.WriteLine(title);
            Console.SetCursorPosition(xPosition, yPosition + 2);
            Console.WriteLine(separator);

            Console.SetCursorPosition(xPosition * 2 - 2, yPosition + 3);
            Console.Write($"Top Player: {topPlayer} - Highest Score: {max}");

            // In nội dung bảng điểm
            for (int i = 0; i < scores.GetLength(0); i++)
            {
                Console.SetCursorPosition(xPosition * 2, yPosition + 5 + i);
                Console.WriteLine($"Player: {scores[i, 0]} -- Score: {scores[i, 1]}");
            }


            // In khung dưới
            Console.SetCursorPosition(xPosition, yPosition + dynamicHeight);
            Console.WriteLine(bottomBorder);
        }
        #endregion

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
            }

            public static void NameGame()//Logo của game khi mới hiện lên
            {

                string[] Name = new string[]
                {
@"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
@"▒▒▒▒▒██████▒██████▒███████▒███████▒██▒▒▒▒█▒▒▒▒▒",
@"▒▒▒▒▒█▒▒▒▒█▒█▒▒▒▒█▒█▒▒▒▒▒▒▒█▒▒▒▒▒▒▒███▒▒▒█▒▒▒▒▒",
@"▒▒▒▒▒█▒▒▒▒█▒██████▒█▒▒▒▒▒▒▒█▒▒▒▒▒▒▒█▒██▒▒█▒▒▒▒▒",
@"▒▒▒▒▒█▒▒▒▒▒▒██▒▒▒▒▒███████▒███████▒█▒▒█▒▒█▒▒▒▒▒",
@"▒▒▒▒▒█▒▒███▒█▒██▒▒▒█▒▒▒▒▒▒▒█▒▒▒▒▒▒▒█▒▒██▒█▒▒▒▒▒",
@"▒▒▒▒▒█▒▒▒▒█▒█▒▒██▒▒█▒▒▒▒▒▒▒█▒▒▒▒▒▒▒█▒▒▒███▒▒▒▒▒",
@"▒▒▒▒▒██████▒█▒▒▒██▒███████▒███████▒█▒▒▒▒██▒▒▒▒▒",
@"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
@"▒▒██████▒█▒▒▒▒█▒██▒▒▒▒█▒██▒▒▒▒█▒██████▒██████▒▒",
@"▒▒█▒▒▒▒█▒█▒▒▒▒█▒███▒▒▒█▒███▒▒▒█▒█▒▒▒▒▒▒█▒▒▒▒█▒▒",
@"▒▒██████▒█▒▒▒▒█▒█▒██▒▒█▒█▒██▒▒█▒█▒▒▒▒▒▒██████▒▒",
@"▒▒██▒▒▒▒▒█▒▒▒▒█▒█▒▒█▒▒█▒█▒▒█▒▒█▒██████▒██▒▒▒▒▒▒",
@"▒▒█▒██▒▒▒█▒▒▒▒█▒█▒▒██▒█▒█▒▒██▒█▒█▒▒▒▒▒▒█▒██▒▒▒▒",
@"▒▒█▒▒██▒▒█▒▒▒▒█▒█▒▒▒███▒█▒▒▒███▒█▒▒▒▒▒▒█▒▒██▒▒▒",
@"▒▒█▒▒▒██▒██████▒█▒▒▒▒██▒█▒▒▒▒██▒██████▒█▒▒▒██▒▒",
@"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
                };
                namegame.X = (Console.BufferWidth - Name[0].Length) / 2;
                namegame.Y = (Console.BufferHeight - Name.Length) / 2;
                Method.Print(ref namegame, Name);
                string HuongDan = "Nhấn Nút Bất Kỳ để sang trang HƯỚNG DẪN CHƠI GAME!";
                Console.SetCursorPosition((Console.BufferWidth - HuongDan.Length) / 2, (Console.BufferHeight + Name.Length + 4) / 2);
                Console.WriteLine(HuongDan);
                Console.ReadKey();
                Console.Clear();
                string[] InstructionsToPlay = new string[]
{
@"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
@"▒                                        |                                                    ▒",
@"▒ HƯỚNG DẪN CHƠI                         |  CÁCH TÍNH ĐIỂM                                    ▒",
@"▒ RÁC HỮU CƠ: Ấn phím 1_xương cá         |    Phân loại rác đúng: +10 điểm                    ▒",
@"▒     ▄▄    ▄   ▄                        |    Phân loại rác sai: -15 điểm                     ▒",
@"▒   ▄███▄▄▄█▄▄▄█▀                        |    Nếu điểm số của bạn < 0 thì KẾT THÚC GAME       ▒",
@"▒    ▀██   ▀▄  ▀█                        |    Nếu bạn đụng chướng ngại vật thì KẾT THÚC GAME  ▒",
@"▒ RÁC TÁI CHẾ ĐƯỢC: Ấn phím 2_chai nhựa  |                                                    ▒",
@"▒      █▀█                               |                                                    ▒",
@"▒     █▀ ▀█                              |                                                    ▒",
@"▒     █   █                              |                                                    ▒",
@"▒     ▀▀▀▀▀                              |                                                    ▒",
@"▒ RÁC THẢI CÒN LẠI: Ấn phím 3_túi nilon  |                                                    ▒",
@"▒    █  █  ▄█ ▐▌                         |                                                    ▒",
@"▒    █   ▀▀▀   █                         |                                                    ▒",
@"▒    ▀█▄▄    ▄█                          |                                                    ▒",
@"▒       ▀▀▀▀▀▀                           |                                                    ▒",
@"▒ Né nếu gặp VẬT CẢN                     |                                                    ▒",
@"▒     ████████                           |                                                    ▒",
@"▒     ████████                           |                                                    ▒",
@"▒     ████████                           |                                                    ▒",
@"▒                                        |                                                    ▒",
@"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"
};
                huongdan.X = (Console.BufferWidth - InstructionsToPlay[0].Length) / 2;
                huongdan.Y = (Console.BufferHeight - InstructionsToPlay.Length) / 2;
                Method.Print(ref huongdan, InstructionsToPlay);
                Console.ReadKey(true);
                Console.Clear();
            }

            public static void InputNameBox()
            {
                string[] savebox = new string[]
                {
        @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
        @"▒       YOUR NAME        ▒",
        @"▒┏━━━━━━━━━━━━━━━━━━━━━━┓▒",
        @"▒┃                      ┃▒",
        @"▒┗━━━━━━━━━━━━━━━━━━━━━━┛▒",
        @"▒ Nút Bất Kỳ để hoàn tất ▒",
        @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"
                };

                inputnamebox.X = (Console.BufferWidth - savebox[0].Length) / 2;
                inputnamebox.Y = (Console.BufferHeight - savebox.Length) / 2;
                Method.Print(ref inputnamebox, savebox);
                Console.SetCursorPosition(
                            (Console.BufferWidth - "Tên không được vượt quá 10 ký tự".Length)
                            / 2, inputnamebox.Y + savebox.Length + 2);
                Console.WriteLine("Tên không được vượt quá 10 ký tự");
                const int maxLength = 10; // Giới hạn ký tự nhập vào (20 ký tự trong ví dụ này)  
                Console.SetCursorPosition(inputnamebox.X + 3, inputnamebox.Y + 3);
                string s = Console.ReadLine();

                // Kiểm tra độ dài và xem chuỗi nhập vào có chỉ toàn khoảng trắng không  
                while (string.IsNullOrWhiteSpace(s) || s.Length > maxLength)
                {
                    if (s.Length > maxLength)
                    {
                        Console.Clear();
                        Method.Print(ref inputnamebox, savebox);
                        Console.SetCursorPosition(
                            (Console.BufferWidth - "Tên không được vượt quá 20 ký tự. Vui lòng nhập lại!!!".Length)
                            / 2, inputnamebox.Y + savebox.Length + 2);
                        Console.WriteLine("Tên không được vượt quá 20 ký tự. Vui lòng nhập lại!!!");
                    }
                    else
                    {
                        Console.Clear();
                        Method.Print(ref inputnamebox, savebox);
                        Console.SetCursorPosition(
                            (Console.BufferWidth - "Tên không được để trống. Vui lòng nhập lại!!!".Length)
                            / 2, inputnamebox.Y + savebox.Length + 2);
                        Console.WriteLine("Tên không được để trống. Vui lòng nhập lại!!!");
                    }

                    Console.SetCursorPosition(inputnamebox.X + 3, inputnamebox.Y + 3);
                    s = Console.ReadLine();
                }
                SaveScore(s, score);

            }
            public static bool GameOver()//Màn hình hiện khi thua
            {
                string[] gameover = new string[]
                  {
                    @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
                    @"▒               G A M E  O V E R                 ▒",
                    @"▒                                                ▒",
                    @"▒             ┏━━━━━┓                            ▒",
                    @"▒             ┃Enter┃ to restart!                ▒",
                    @"▒             ┗━━━━━┛                            ▒",
                    @"▒                                                ▒",
                    @"▒             ┏━━━━━┓                            ▒",
                    @"▒             ┃ Tab ┃ to open score board!       ▒",
                    @"▒             ┗━━━━━┛                            ▒",
                    @"▒                                                ▒",
                    @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"
                  };
                game.X = (Console.BufferWidth - gameover[0].Length) / 2;
                game.Y = (Console.BufferHeight - gameover.Length) / 2;
                Method.Print(ref game, gameover);
                Console.SetCursorPosition((Console.BufferWidth
                        - "Enter để chơi lại hoặc phím 0 để thoát ".Length) / 2, game.Y + gameover.Length + 2);
                Console.WriteLine("Enter để chơi lại hoặc phím 0 để thoát ");
                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true để không in ra phím 
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        return true;  // Trả về true nếu người chơi muốn restart
                    }
                    else if (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.NumPad0)
                    {
                        return false; // Trả về false nếu người chơi muốn thoát
                    }
                    else if (keyInfo.Key == ConsoleKey.Tab) // tab mở bxh
                    {
                        Console.Clear();
                        Scoreboard();
                    }

                }



            }

            public static int MaximumLengthOfPlayersName { get; private set; }

            public static void BangXepHang()
            {
                string[] bangxephang = new string[]
                {
                @"┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓",
                @"┃                                      RANK                                          ┃",
                @"┃━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┃                                                                                    ┃",
                @"┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛",
                };
                bang.X = (Console.BufferWidth - bangxephang[0].Length) / 2;
                bang.Y = (Console.BufferHeight - bangxephang.Length) / 2;
                Method.Print(ref bang, bangxephang);
            }

        }






        // Người chơi
        public class User
        {
            public static void DisplayScoreBoard(int score)
            {
                int scoreBoardX = 51;
                int scoreBoardY = 29;

                // Đặt con trỏ tại vị trí bảng điểm
                Console.SetCursorPosition(scoreBoardX, scoreBoardY);

                // In bảng điểm, làm mới nội dung mỗi lầ
                Console.Write($"  Score: {score}    ");

                // Đặt lại con trỏ về vị trí cũ sau khi in bảng điểm
                //Console.SetCursorPosition(currentLeft, currentTop);
            }
            public static void Info_User()
            {
                string[] output = new string[]
                {   @" ██▄▄▄▄▄▄▄▄▄▄█▀ ",
                    @" ██          █▄ ",
                    @"▄▀█  ▐▌  █   ██ ",
                    @"▐▄▄▌   ▄▄    █  ",
                    @"  █         █   ",
                    @"   ▀▀█▄▄█▀▀     ",
                };

                Random rnd = new Random();
                int[] lane = { 2, 11, 20 }; // thay doi Y, giu nguyen X
                user.X = 3;
                user.Y = lane[rnd.Next(0, lane.Length)];
                // In đầu ra ban đầu
                Method.Print(ref user, output);
                while (DK)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true để không in ra phím  
                    int truonghop = 0;
                    if (keyInfo.KeyChar == 'w')
                        truonghop = 1; // Di chuyển lên  
                    if (keyInfo.KeyChar == 's')
                        truonghop = 2; // Di chuyển xuống
                    if (keyInfo.Key == ConsoleKey.Escape)
                        truonghop = 3;

                    Method.Clear(user, output); // Xóa đầu ra cũ 
                    if (truonghop == 1 && user.Y > 2)  // Nếu không vượt quá giới hạn trên  
                    {
                        user.Y -= 9;

                    }
                    if (truonghop == 2 && user.Y < 18) // Nếu không vượt quá giới hạn dưới  
                    {
                        user.Y += 9;

                    }
                    if (truonghop == 3)
                    {
                        DK = false;
                        Console.Clear();
                        break;
                    }
                    Head_user.X = user.X + output[0].Length;
                    Head_user.Y = user.Y;
                    //// In lại đầu ra mới  
                    Method.Print(ref user, output);
                    //if (Method.Phan_Loai_Rac() == 1)
                    //    Method.Clear(ref fish, output);
                }
            }
        }
        //Các loại rác
        //Các loại rác
        public class Object
        {
            private static Random rnd = new Random();
            public static void Fish()
            {
                string[] output = new string[]
                {
            @"     ▄▄   ▄  ▄ ",
            @"   ▄███▄▄█▄▄█▀ ",
            @"    ▀██  ▀▄ ▀█ ",
                };

                int[] lane = { 2, 11, 20 };
                fish.X = (Console.BufferWidth - output[0].Length) / 2;
                fish.Y = lane[rnd.Next(0, lane.Length)];
                Method.Move(ref fish, output,"fish");
            }
            public static void Plastic_bag()
            {
                string[] output = new string[]
                {
        @"  █ █   ▐ █  ",
        @"  █  █ ▄█ ▐▌ ",
        @"  █  ▀▀▀  █ ",
        @"  ▀█▄▄  ▄█  ",
                };

                int[] lane = { 2, 11, 20 };
                plastic_bag.X = (Console.BufferWidth - output[0].Length) / 2;
                plastic_bag.Y = lane[rnd.Next(0, lane.Length)];
                Method.Move(ref plastic_bag, output,"plastic bag");
            }

            public static void Glass_Bottle()
            {
                string[] output = new string[]
                 {
        @"   █▀█  ",
        @"  █▀ ▀█ ",
        @"  █   █ ",
        @"  █   █ ",
        @"  ▀▀▀▀▀ ",
                    };

                int[] lane = { 2, 11, 20 };
                glass_bottle.X = (Console.BufferWidth - output[0].Length) / 2;
                glass_bottle.Y = lane[rnd.Next(0, lane.Length)];
                Method.Move(ref glass_bottle, output,"glass bottle");
            }
            public static void BlockImage()
            {
                string[] BlockImage =
                {   @" ████████ ",
                    @" ████████ ",
                    @" ████████ ",
                    @" ████████ ",
                    @" ████████ ",
                    @" ████████ ",
                };
                int[] lane = { 2, 11, 20 };
                block.X = (Console.BufferWidth - BlockImage[0].Length) / 2;
                block.Y = lane[rnd.Next(0, lane.Length)];
                Method.Move(ref block, BlockImage,"block");
            }

        }

        // Các hàm xử lý
        public class Method
        {
            static Point previousPoint = new Point(0, 0);

            //In ra màn hình
            public static void Print(ref Point point, string[] output)
            {
                lock (consoleLock)
                {
                    for (int i = 0; i < output.Length; i++)
                    {
                        Console.SetCursorPosition(point.X, point.Y + i);// Thiết lập vị trí con trỏ  
                        Console.WriteLine(output[i]);
                    }
                }
            }
            // Xóa những hình cũ
            public static void Clear(Point point, string[] output)
            {
                lock (consoleLock)
                {
                    for (int i = 0; i < output.Length; i++)
                    {
                        for (int k = 0; k < output[i].Length; k++)
                        {
                            Console.SetCursorPosition(point.X + k, point.Y + i);// Thiết lập vị trí con trỏ  
                            Console.WriteLine(' ');

                        }
                    }

                }

            }
            // Di chuyển
            public static void Move(ref Point point, string[] print,string objectname)
            {
                int xspawn = point.X;
                Random rnd = new Random();
                int[] lane = { 2, 11, 20 }; // thay doi Y, giu nguyen X
                // Cờ đánh dấu đã va chạm
                while (point.X > 0 && DK == true)

                {
                    lock (consoleLock)
                    {
                        bool hasCollided = false;
                        Method.Clear(point, print); // Xóa vật thể

                        Method.Print(ref point, print);  // In lại vật thể ở vị trí mới

                        point.X--;



                        if (Head_user.IsHit(point) && !hasCollided || point.X < 2 && Head_user.IsHit(point) == false) // có va chạm, bỏ qua 
                        {
                            countScore(point); // Cập nhật điểm khi có va chạm
                            hasCollided = true; // Đánh dấu đã va chạm, tránh tính lại điểm trong lần va chạm này

                        }

                        User.DisplayScoreBoard(score);

                        Thread.Sleep(30); // Tạm dừng để điều chỉnh tốc độ di chuyển
                        if (objectname == "block" && Head_user.IsHit(point))
                        
                            DK = false;


                            if (point.X == 1 || hasCollided)
                        {
                            Method.Clear(point, print);
                            point.X = xspawn;
                            // Thay đổi Y ngẫu nhiên cho khối mới  
                            point.Y = lane[rnd.Next(0, lane.Length)];
                            Method.Print(ref point, print);  // Tạo lại hình ảnh block
                        }


                    }

                }

            }
            //Check va chạm với người dùng với rác và chướng ngọai vật 
            public static bool hitBox(Point head_user, Point input)
            {
                head_user.X = user.X + 5;
                if (head_user.X == input.X)
                    return true;
                else return false;

            }

            //check để cho không in trùng tọa đọ cnv và rác 
            //public static bool isEqual(Point fish, Point plastic_bag, Point glass_bottle, Point block)
            //{
            //    if (
            //       fish.X == plastic_bag.X && fish.Y == plastic_bag.Y ||
            //       fish.X == glass_bottle.X && fish.Y == glass_bottle.Y ||
            //       fish.X == block.X && fish.Y == block.Y ||
            //       plastic_bag.X == glass_bottle.X && plastic_bag.Y == glass_bottle.Y ||
            //       plastic_bag.X == block.X && plastic_bag.Y == block.Y ||
            //       glass_bottle.X == block.X && glass_bottle.Y == block.Y
            //     )
            //        return false; // false là trùng
            //    else return true; // true không trùng
            //}

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
                //if (isEqual(head_user, block))
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
            public bool IsHit(Point other)
            {
                return this.X > other.X && this.Y == other.Y;
            }


        }


    }
}






