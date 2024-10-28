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
            Method.Codinhkhung();
            Map.NameGame();
            Console.Clear();
            Method.Codinhkhung();
            Map.InputNameBox();
            Console.Clear();
            while (running)
            {
                // Reset trạng thái game
                score = 0;
                DK = true; // Cờ để điều khiển vòng lặp game
                again = true; // Cờ để điều khiển restart
                Method.Codinhkhung();
                Console.Clear();
                Map.DrawMap(width, height);
                for (int i = 0; i < 10; i++)
                {
                    Thread t1 = new Thread(() => { User.Info_User(); });
                    t1.Start(); t1.Priority = ThreadPriority.Highest;
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
                lock (consoleLock)
                {
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

            }
        }
       
        static void countScore(Point objects)
        {

            if (Head_user.IsHit(objects) == true)
                score += 10;
            else
                score -= 15;
            if (score < 0)
            {
                DK = false;
                Console.Clear();
                Console.SetCursorPosition(((width+1) * 2 - "Nhấn nút bất kỳ".Length) / 2, height + 2);
                Console.Write("Nhấn nút bất kỳ");
            }

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
        public class Map
        {
            // Phương thức vẽ bản đồ với chiều rộng và chiều cao được chỉ định  
            public static void DrawMap(int width, int height)
            {
                // Đặt màu sắc của chữ là màu xanh lá cây đậm   
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                // Vòng lặp qua từng hàng của bản đồ  
                for (int i = 0; i <= height; i++)
                {
                    // Vòng lặp qua từng cột của bản đồ  
                    for (int j = 0; j <= width * 2; j++)
                    {
                        // Khung ngoài  
                        if (i == 0) // Vẽ khung trên  
                        {
                            Console.Write("▄");
                        }
                        else if (i == height) // Vẽ khung dưới  
                        {
                            Console.Write("▀");
                        }
                        else if (j == 0) // Vẽ khung trái  
                        {
                            Console.Write("▌");
                        }
                        else if (j == width * 2) // Vẽ khung phải  
                        {
                            Console.Write("▐");
                        }
                        // Vẽ đường line chia ngang 1  
                        else if (i == height / 3)
                        {
                            Console.Write("─");
                        }
                        // Vẽ đường line chia ngang 2  
                        else if (i == 2 * height / 3)
                        {
                            Console.Write("─");
                        }
                        else
                        {
                            // Nếu không rơi vào các trường hợp trên, vẽ khoảng trắng  
                            Console.Write(" ");
                        }
                    }
                    // Xuống dòng sau khi vẽ xong một hàng  
                    Console.WriteLine();
                }
                // Đặt lại màu sắc về màu sắc mặc định của console  
                Console.ResetColor();
            }
        

        public static void NameGame()//Logo của game khi mới hiện lên
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
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
                Method.Print(ref namegame, Name, "Green");
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
@"▒ RÁC HỮU CƠ: Xương cá                   |    Phân loại rác đúng: +10 điểm                    ▒",
@"▒     ▄▄    ▄   ▄                        |    Phân loại rác sai: -15 điểm                     ▒",
@"▒   ▄███▄▄▄█▄▄▄█▀                        |    Nếu điểm số của bạn < 0 thì KẾT THÚC GAME       ▒",
@"▒    ▀██   ▀▄  ▀█                        |    Nếu bạn đụng chướng ngại vật thì KẾT THÚC GAME  ▒",
@"▒ RÁC TÁI CHẾ ĐƯỢC: Chai nhựa            |                                                    ▒",
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
                Method.Print(ref huongdan, InstructionsToPlay, "Green");
                Console.ReadKey(true);
                Console.Clear();
                Console.ResetColor();
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
                Method.Print(ref inputnamebox, savebox, "DarkBlue");
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
                        Method.Print(ref inputnamebox, savebox, "DarkBlue");
                        Console.SetCursorPosition(
                            (Console.BufferWidth - "Tên không được vượt quá 20 ký tự. Vui lòng nhập lại!!!".Length)
                            / 2, inputnamebox.Y + savebox.Length + 2);
                        Console.WriteLine("Tên không được vượt quá 20 ký tự. Vui lòng nhập lại!!!");
                    }
                    else
                    {
                        Console.Clear();
                        Method.Print(ref inputnamebox, savebox, "DarkBlue");
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
            public static bool GameOver() // Phương thức để hiển thị màn hình "Game Over" khi người chơi thua  
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // Mảng chứa các chuỗi đại diện cho hình ảnh và nội dung của màn hình "Game Over"  
                string[] gameover = new string[]
                {
        @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒", // Ranh giới phía trên của màn hình  
        @"▒               G A M E  O V E R                 ▒", // Tiêu đề màn hình "Game Over"  
        @"▒                                                ▒", // Khoảng trống   
        @"▒             ┏━━━━━┓                            ▒", // Ranh giới cho hộp thoại  
        @"▒             ┃Enter┃ to restart!                ▒", // Hướng dẫn cho người chơi để bắt đầu lại trò chơi  
        @"▒             ┗━━━━━┛                            ▒", // Đáy hộp thoại  
        @"▒                                                ▒", // Khoảng trống   
        @"▒             ┏━━━━━┓                            ▒", // Hộp thoại cho scoreboard  
        @"▒             ┃ Tab ┃ to open score board!       ▒", // Hướng dẫn cho người chơi để mở bảng điểm  
        @"▒             ┗━━━━━┛                            ▒", // Đáy hộp thoại  
        @"▒                                                ▒", // Khoảng trống   
        @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"  // Ranh giới phía dưới của màn hình  
                };

                // Xác định tọa độ X để căn giữa màn hình gameover theo chiều ngang  
                game.X = (Console.BufferWidth - gameover[0].Length) / 2;
                // Xác định tọa độ Y để căn giữa màn hình gameover theo chiều dọc  
                game.Y = (Console.BufferHeight - gameover.Length) / 2;

                // In nội dung của màn hình "Game Over" lên console với màu nền là "DarkBlue"  
                Method.Print(ref game, gameover, "DarkBlue");

                // Thiết lập vị trí con trỏ để viết hướng dẫn ở phía dưới màn hình  
                Console.SetCursorPosition((Console.BufferWidth - "Enter để chơi lại hoặc phím 0 để thoát ".Length) / 2, game.Y + gameover.Length + 2);
                // In hướng dẫn cho người chơi  
                Console.WriteLine("Enter để chơi lại hoặc phím 0 để thoát ");

                // Vòng lặp vô tận để chờ người chơi đưa ra lựa chọn  
                while (true)
                {
                    // Đọc phím nhấn từ người chơi mà không hiển thị phím đó trên console  
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true để không in ra phím  

                    // Nếu người chơi nhấn phím Enter, trả về true để bắt đầu lại trò chơi  
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        return true;  // Trả về true nếu người chơi muốn restart  
                    }
                    // Nếu người chơi nhấn phím '0' hoặc 'NumPad0', trả về false để thoát trò chơi  
                    else if (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.NumPad0)
                    {
                        return false; // Trả về false nếu người chơi muốn thoát  
                    }
                    // Nếu người chơi nhấn phím Tab, hiển thị bảng điểm  
                    else if (keyInfo.Key == ConsoleKey.Tab) // tab mở bxh  
                    {
                        Console.Clear(); // Xóa màn hình console  
                        Scoreboard(); // Gọi phương thức hiển thị bảng điểm  
                    }
                }
                Console.ResetColor();
            }
        }
        // Người chơi
        public class User
        {
            public static void DisplayScoreBoard(int score)
            {
                int scoreBoardX = ((width-2)*2-"Score".Length)/2;
                int scoreBoardY = height;

                // Đặt con trỏ tại vị trí bảng điểm
                Console.SetCursorPosition(scoreBoardX, scoreBoardY);

                // In bảng điểm, làm mới nội dung mỗi lầ
                Console.Write($" Score: {score} ");
            }
            public static void Info_User() 
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;//MAUF MEF
                // Mảng các chuỗi chứa hình ảnh đại diện cho người dùng.  
                // Mỗi chuỗi trong mảng là một dòng của hình đại diện.  
                string[] output = new string[]
                {
        @" ██▄▄▄▄▄▄▄▄▄▄█▀ ",
        @" ██          █▄ ",
        @"▄▀█  ▐▌  █   ██ ",
        @"▐▄▄▌   ▄▄    █  ",
        @"  █         █   ",
        @"   ▀▀█▄▄█▀▀     ",
                };

                Random rnd = new Random(); 
                int[] lane = { 2, 11, 20 }; 

                user.X = 3;   
                user.Y = lane[rnd.Next(0, lane.Length)]; // Gán tọa độ Y cho người dùng bằng một giá trị ngẫu nhiên từ mảng lane  

                // In hình đại diện của người dùng ra console lần đầu tiên với tọa độ đã thiết lập  
                Method.Print(ref user, output, "");

                // Vòng lặp chính, chạy liên tục cho đến khi DK (trạng thái hoạt động) là true  
                while (DK)
                {
                    // Đọc phím nhấn của người dùng mà không hiển thị nó trên console  
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    Method.Clear(user, output);

                    if (keyInfo.Key == ConsoleKey.UpArrow && user.Y > 2) // Nếu nhấn phím mũi tên lên và tọa độ Y lớn hơn 2  
                        user.Y -= 9; // Giảm tọa độ Y để di chuyển lên (9 đơn vị)  

                    if (keyInfo.Key == ConsoleKey.DownArrow && user.Y < 18) // Nếu nhấn phím mũi tên xuống và tọa độ Y nhỏ hơn 18  
                        user.Y += 9; // Tăng tọa độ Y để di chuyển xuống (9 đơn vị)  

                    if (keyInfo.Key == ConsoleKey.Escape) // Nếu nhấn phím Escape  
                    {
                        DK = false; // Đặt DK thành false để dừng vòng lặp  
                        Console.Clear(); // Xóa màn hình console khi thoát, dọn sạch giao diện  
                        break; // Thoát khỏi vòng lặp  
                    }

                    // Xóa đầu ra cũ của hình đại diện người dùng trên console trước khi in lại  

                    // Cập nhật vị trí cho biến Head_user dựa trên vị trí hiện tại của user  
                    Head_user.X = user.X + output[0].Length; // Đặt tọa độ X của Head_user ngay bên phải user   
                    Head_user.Y = user.Y; // Cập nhật tọa độ Y của Head_user để trùng với tọa độ Y của user  

                    // In lại hình đại diện mới của người dùng lên console với tọa độ đã cập nhật  
                    Method.Print(ref user, output, "");
                }
                Console.ResetColor();//RESET MAUF MEF
            }
        }
        //Các loại rác
        public class Object
        {
            // Tạo một đối tượng Random để sinh ra các giá trị ngẫu nhiên  
            private static Random rnd = new Random();

            // Phương thức để hiển thị hình ảnh của một con cá  
            public static void Fish()
            {
                // Mảng chuỗi chứa các dòng để vẽ hình con cá  
                string[] output = new string[]
                {
            @"     ▄▄   ▄  ▄ ",
            @"   ▄███▄▄█▄▄█▀ ",
            @"    ▀██  ▀▄ ▀█ ",
                };

                // Mảng chứa các làn (y-coordinate) mà con cá có thể xuất hiện  
                int[] lane = { 2, 11, 20 };

                // Đặt tọa độ X cho con cá, tính toán dựa trên chiều rộng của console  
                fish.X = width * 2 - output[0].Length;
                // Chọn một làn ngẫu nhiên cho con cá  
                fish.Y = lane[rnd.Next(0, lane.Length)];

                // Gọi phương thức Move để vẽ con cá  
                Method.Move(ref fish, output, "fish");
            }

            // Phương thức để hiển thị hình ảnh của một túi nhựa  
            public static void Plastic_bag()
            {
                // Mảng chuỗi chứa các dòng để vẽ hình túi nhựa  
                string[] output = new string[]
                {
            @"  █ █   ▐ █  ",
            @"  █  █ ▄█ ▐▌ ",
            @"  █  ▀▀▀  █ ",
            @"  ▀█▄▄  ▄█  ",
                };

                // Mảng chứa các làn (y-coordinate) mà túi nhựa có thể xuất hiện  
                int[] lane = { 2, 11, 20 };

                // Đặt tọa độ X cho túi nhựa  
                plastic_bag.X = width * 2 - output[0].Length;
                // Chọn một làn ngẫu nhiên cho túi nhựa  
                plastic_bag.Y = lane[rnd.Next(0, lane.Length)];

                // Gọi phương thức Move để vẽ túi nhựa  
                Method.Move(ref plastic_bag, output, "plastic bag");
            }

            // Phương thức để hiển thị hình ảnh của một chai thủy tinh  
            public static void Glass_Bottle()
            {
                // Mảng chuỗi chứa các dòng để vẽ hình chai thủy tinh  
                string[] output = new string[]
                {
            @"   █▀█  ",
            @"  █▀ ▀█ ",
            @"  █   █ ",
            @"  █   █ ",
            @"  ▀▀▀▀▀ ",
                };

                // Mảng chứa các làn (y-coordinate) mà chai thủy tinh có thể xuất hiện  
                int[] lane = { 2, 11, 20 };

                // Đặt tọa độ X cho chai thủy tinh  
                glass_bottle.X = width * 2 - output[0].Length;
                // Chọn một làn ngẫu nhiên cho chai thủy tinh  
                glass_bottle.Y = lane[rnd.Next(0, lane.Length)];

                // Gọi phương thức Move để vẽ chai thủy tinh  
                Method.Move(ref glass_bottle, output, "glass bottle");
            }

            // Phương thức để hiển thị hình ảnh của một khối  
            public static void BlockImage()
            {
                // Mảng chuỗi chứa các dòng để vẽ hình khối  
                string[] BlockImage =
                {
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
        };

                // Mảng chứa các làn (y-coordinate) mà khối có thể xuất hiện  
                int[] lane = { 2, 11, 20 };

                // Đặt tọa độ X cho khối  
                block.X = width * 2 - BlockImage[0].Length;
                // Chọn một làn ngẫu nhiên cho khối  
                block.Y = lane[rnd.Next(0, lane.Length)];

                // Gọi phương thức Move để vẽ khối  
                Method.Move(ref block, BlockImage, "block");
            }
        }

        // Các hàm xử lý
        public class Method
        {
            public static void Codinhkhung()
            {
                // Sử dụng khóa để đảm bảo chỉ một luồng có thể vào đây tại một thời điểm  
                lock (consoleLock)
                {
                    try
                    {
                        // Kiểm tra kích thước của Buffer so với kích thước đã lưu trữ  
                        if (width != Console.BufferWidth || height != Console.BufferHeight)
                            throw new Exception(); // Ném ngoại lệ nếu kích thước không khớp  
                    }
                    catch
                    {
                        // Nếu có ngoại lệ (kích thước không khớp), làm sạch console  
                        Console.Clear();

                        // Kiểm tra kích thước cửa sổ console  
                        if (Console.BufferWidth < 120 || Console.BufferHeight < 30)
                        {
                            // Thông điệp cảnh báo kích thước cửa sổ quá nhỏ  
                            string message = "Kích thước cửa sổ quá nhỏ!";

                            // In thông điệp ở giữa màn hình nếu có đủ không gian  
                            if (Console.BufferWidth > message.Length)
                            {
                                // Tính toán vị trí để in thông điệp ở giữa  
                                Console.SetCursorPosition((Console.BufferWidth - message.Length) / 2, Console.BufferHeight / 2);
                                Console.Write(message); // In thông điệp ra console  
                            }
                        }

                        // Chờ cho đến khi kích thước cửa sổ được điều chỉnh đủ lớn  
                        while (Console.BufferWidth < 120 || Console.BufferHeight < 30)
                        {
                            // Có thể in một thông báo hoặc xử lý tín hiệu từ người dùng  
                            // (Mã thực hiện ở đây có thể được thêm tùy vào yêu cầu)  
                        }
                    }
                }
            }
            //In ra màn hình
            public static void Print(ref Point point, string[] output, string color)
            {

                lock (consoleLock)// Khóa đối tượng để đảm bảo rằng phần này của code
                                  // không bị truy cập từ nhiều luồng cùng một lúc 
                {
                    ConsoleColor ColorOfImage = new ConsoleColor();// Khai báo biến lưu màu sắc  


                    if (Enum.TryParse(color, out ColorOfImage))// Kiểm tra nếu màu sắc đầu vào có thể chuyển thành kiểu ConsoleColor                        
                        Console.ForegroundColor = ColorOfImage;// Thiết lập màu sắc của văn bản đầu ra
                                                               // 
                    for (int i = 0; i < output.Length; i++)// Vòng lặp qua từng dòng của mảng output  

                    {

                        Console.SetCursorPosition(point.X, point.Y + i);// Thiết lập vị trí con trỏ trên console với tọa độ được chỉ định
                                                                        // với tọa độ X được cộng thêm k (để di chuyển theo chiều ngang)  
                                                                        // và tọa độ Y được cộng thêm i (để di chuyển theo chiều dọc)

                        Console.WriteLine(output[i]);// In dòng văn bản hiện tại  
                    }
                }
            }
            // Xóa những hình cũ
            public static void Clear(Point point, string[] output)
            {

                lock (consoleLock)// Khóa đối tượng để đảm bảo rằng phần này của code  
                                  // sẽ không bị truy cập từ nhiều luồng cùng một lúc
                {
                    for (int i = 0; i < output.Length; i++)// Vòng lặp qua từng dòng của mảng output 
                    {
                        for (int k = 0; k < output[i].Length; k++)// Vòng lặp qua từng ký tự của dòng hiện tại trong output  
                        {
                            Console.SetCursorPosition(point.X + k, point.Y + i);// Thiết lập vị trí con trỏ trên console với tọa độ được chỉ định  
                                                                                // với tọa độ X được cộng thêm k (để di chuyển theo chiều ngang)  
                                                                                // và tọa độ Y được cộng thêm i (để di chuyển theo chiều dọc)

                            Console.WriteLine(' ');// Thiết lập vị trí con trỏ
                                                   // Ghi một ký tự khoảng trắng (' ') tại vị trí hiện tại,  
                                                   // điều này giúp "xóa" nội dung hiện tại tại vị trí đó  
                                                   // bằng cách ghi đè lên các ký tự cũ bằng khoảng trắng 

                        }
                    }

                }

            }
            //Hàm di chuyển
            public static void Move(ref Point point, string[] print, string objectname)
            {
                int xspawn = point.X; // Lưu tọa độ X ban đầu

                Random rnd = new Random(); // Khởi tạo đối tượng Random để tạo số ngẫu nhiên

                int[] lane = { 2, 11, 20 }; // Mảng chứa các giá trị Y cho phép di chuyển khối mới

                while (point.X > 0 && DK == true) // Lặp cho đến khi tọa độ X không còn hợp lệ hoặc DK là false  
                {
                    lock (consoleLock) // Khóa để đảm bảo an toàn khi truy cập console  
                    {
                        bool hasCollided = false; // Biến đánh dấu nếu xảy ra va chạm  

                        //Di chuyển vật thể
                        Method.Clear(point, print); // Xóa vật thể cũ ở vị trí hiện tại  
                        Method.Print(ref point, print, ""); // In lại vật thể ở vị trí mới  
                        point.X--; // Di chuyển vật thể sang trái  
                        Thread.Sleep(30); // tốc độ di chuyển  


                        // Kiểm tra va chạm với đối tượng người dùng  
                        if ((Head_user.IsHit(point) && !hasCollided && objectname != "block") || (point.X < 2 && Head_user.IsHit(point) == false && objectname != "block")) // có va chạm, bỏ qua  
                        {
                            countScore(point); // Cập nhật điểm khi xảy ra va chạm  
                            hasCollided = true; // Đánh dấu đã xảy ra va chạm, tránh tính điểm lại trong lần va chạm này  
                        }

                        User.DisplayScoreBoard(score); // Hiển thị bảng điểm  

                        // Kiểm tra "đầu người dùng" va chạm với chướng ngoại vật 
                        if (objectname == "block" && Head_user.IsHit(point))
                        {
                            DK = false; // Dừng lại nếu va chạm với chướng ngại vật
                            Console.Clear();
                            Console.SetCursorPosition(((width+1) * 2 - "Nhấn nút bất kỳ".Length) / 2, height + 2);
                            Console.Write("Nhấn nút bất kỳ"); // Nhắc nhở người dùng nhấn phím  
                        }

                        if (point.X == 1 || hasCollided) // khi đi hết đường hoặc khi va chạm với người chơi
                        {
                            Method.Clear(point, print); // Xóa vật thể cũ  
                            point.X = xspawn; // Trả lại tọa độ X về điểm khởi đầu  
                            point.Y = lane[rnd.Next(0, lane.Length)]; // 
                            Method.Print(ref point, print, ""); // Tạo lại hình ảnh block mới  
                        }
                    }
                }
            }
        }

        public class Point
        {
            
            private int x; 
            private int y; 

            public int X
            {
                get => x; 
                set => x = value; 
            }

            public int Y
            {
                get => y; 
                set => y = value; 
            }

            public Point(int x, int y)
            {
                this.x = x; 
                this.y = y; 
            }

            // Kiểm tra va chạm  
            public bool IsHit(Point other)
            {
                return this.X > other.X && this.Y == other.Y;
            }
        }


    }
}
