using System.Text; // Thư viện hỗ trợ làm việc với chuỗi ký tự, bao gồm mã hóa văn bản (Encoding).
using System.IO; // Thư viện hỗ trợ thao tác với file và luồng dữ liệu (Streams).
using System.Runtime.Serialization.Formatters; // Thư viện cung cấp các lớp cho việc tuần tự hóa đối tượng (serialization).
using System.Drawing; // Thư viện cung cấp các lớp để làm việc với đồ họa (Graphics), hình ảnh (Image), và màu sắc (Color).
using static Game.Program; // Cho phép truy cập trực tiếp vào các thành phần của lớp `Program` trong namespace `Game`.
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;

namespace Game
{
    //Program chính
    public class Program
    {
        static int width = 50, height = 27;
        public static object consoleLock = new object();
        static Point player = new Point(0, 0);
        static Point fishbone = new Point(0, 0);
        static Point plasticbag = new Point(0, 0);
        static Point glassbottle = new Point(0, 0);
        static Point block = new Point(0, 0);
        static Point playerhitbox = new Point(0, 0);
        public static Point playerHitbox { get => playerhitbox; set => playerhitbox = value; }
        static int score = 0;
        static string playername = "";
        private static bool dK = true;
        public static bool DK { get => dK; set => dK = value; }
        static bool running = true;



        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.CursorVisible = false; // Ẩn con trỏ
            Method.fixedBorder(); // Gọi phương thức cố định khung
            NameGame(); // Hiển thị tên trò chơi
            Console.Clear(); // Xóa màn hình console
            Method.fixedBorder(); // Gọi lại phương thức cố định khung
            inputNameBox(); // Hiển thị hộp nhập tên người chơi
            Console.Clear(); // Xóa màn hình console

            // Bắt đầu vòng lặp chính của game
            while (running)
            {
                score = 0; // Đặt lại điểm
                DK = true; // Điều khiển vòng lặp game
                Method.fixedBorder(); // Cố định khung

                drawMap(width, height);// Vẽ bản đồ 
                                       // Tạo và chạy các luồng để hoạt động game
                Thread t1 = new Thread(() => { Player(); });
                t1.Start();
                t1.Priority = ThreadPriority.Highest; // Đặt ưu tiên cao nhất

                Thread t2 = new Thread(() => { Block(); });
                t2.Start(); // Chạy luồng hiển thị chướng ngại vật

                Thread t3 = new Thread(() => { Fishbone(); });
                t3.Start(); // Chạy luồng hiển thị xương cá

                Thread t4 = new Thread(() => { Plasticbag(); });
                t4.Start(); // Chạy luồng hiển thị túi nhựa

                Thread t5 = new Thread(() => { GlassBottle(); });
                t5.Start(); // Chạy luồng hiển thị chai thủy tinh

                t1.Join(); // Đợi luồng t1 kết thúc
                t2.Join(); // Đợi luồng t2 kết thúc
                t3.Join(); // Đợi luồng t3 kết thúc
                t4.Join(); // Đợi luồng t4 kết thúc
                t5.Join(); // Đợi luồng t5 kết thúc


                SaveScore(playername, score); // Lưu điểm của người chơi
                Console.Clear();

                // Kiểm tra xem người chơi có muốn chơi lại không
                bool restart = GameOver(); // Gọi phương thức GameOver để hỏi người chơi
                if (restart)
                {
                    Console.Clear();
                    continue; // Quay lại đầu vòng lặp để bắt đầu lại game
                }
                else
                {
                    running = false; // Thoát vòng lặp và kết thúc game
                }
            }

        }





        // Vẽ bản đồ với chiều rộng và chiều cao 
        public static void drawMap(int width, int height)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            // Vòng lặp qua từng hàng của bản đồ  
            for (int i = 0; i <= height; i++)
            {
                // Vòng lặp qua từng cột của bản đồ  
                for (int j = 0; j <= width * 2; j++)
                {
                    // Khung chung  
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
                        // Ngược lại, vẽ khoảng trắng  
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();// Đặt lại màu sắc về màu sắc mặc định
        }



        //Logo của game khi mới hiện lên
        public static void NameGame()
        {
            Point gamename = new Point(0, 0);
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

            // Căn giữa màn hình 
            gamename.X = (Console.BufferWidth - Name[0].Length) / 2;
            gamename.Y = (Console.BufferHeight - Name.Length) / 2;
            Method.Print(ref gamename, Name, "Green");

            string a = "Nhấn Nút Bất Kỳ để sang trang HƯỚNG DẪN CHƠI GAME!";
            Console.SetCursorPosition((Console.BufferWidth - a.Length) / 2, (Console.BufferHeight + Name.Length + 4) / 2);
            Console.WriteLine(a);
            // Chờ người dùng nhấn phím để chuyển qua trang hướng dẫn chơi
            Console.ReadKey();
            Console.Clear();
            Method.fixedBorder();
            Console.Clear();

            Point instruction = new Point(0, 0);
            string[] InstructionsToPlay = new string[]
            {
                @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
                @"▒                                                                                             ▒",
                @"▒                                 HƯỚNG DẪN CHƠI                                              ▒",
                @"▒     RÁC HỮU CƠ: Xương cá                                                                    ▒",
                @"▒       ▄▄    ▄   ▄                      |         CÁCH TÍNH ĐIỂM                             ▒",
                @"▒     ▄███▄▄▄█▄▄▄█▀                      |   Nhặt rác       : +10 điểm                        ▒",
                @"▒      ▀██   ▀▄  ▀█                      |   Không nhặt rác : -15 điểm                        ▒",
                @"▒     RÁC TÁI CHẾ ĐƯỢC: Chai nhựa        |   Nếu điểm số của bạn < 0 thì KẾT THÚC GAME        ▒",
                @"▒        █▀█                             |   Nếu bạn đụng chướng ngại vật thì KẾT THÚC GAME   ▒",
                @"▒       █▀ ▀█                            |                                                    ▒",
                @"▒       █   █                            |                                                    ▒",
                @"▒       ▀▀▀▀▀                            |                                                    ▒",
                @"▒     RÁC THẢI CÒN LẠI: Túi nilon        |                                                    ▒",
                @"▒      █  █  ▄█ ▐▌                       |       ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓           ▒",
                @"▒      █   ▀▀▀   █                       |       ┃  Ấn Esc để kết thúc trò chơi   ┃           ▒",
                @"▒      ▀█▄▄    ▄█                        |       ┃   bất cứ lúc nào bạn muốn!!!   ┃           ▒",
                @"▒         ▀▀▀▀▀▀                         |       ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛           ▒",
                @"▒     Né nếu gặp VẬT CẢN                 |                                                    ▒",
                @"▒       ████████                         |                                                    ▒",
                @"▒       ████████                         |                                                    ▒",
                @"▒       ████████                         |                                                    ▒",
                @"▒                                                                                             ▒",
                @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"
            };
            // Căn giữa bảng hướng dẫn trên màn hình 
            instruction.X = (Console.BufferWidth - InstructionsToPlay[0].Length) / 2;
            instruction.Y = (Console.BufferHeight - InstructionsToPlay.Length) / 2;
            Method.Print(ref instruction, InstructionsToPlay, "Green");// In bảng hướng dẫn ra màn hình
            Console.ReadKey(true);// Chờ người dùng nhấn phím trước khi xóa màn hình
            Console.Clear();
        }


        //trang đăng nhập
        public static void inputNameBox()
        {
            Point inputnamebox = new Point(0, 0);
            string[] namebox = new string[]
            {
                @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒",
                @"▒       YOUR NAME        ▒",
                @"▒┏━━━━━━━━━━━━━━━━━━━━━━┓▒",
                @"▒┃                      ┃▒",
                @"▒┗━━━━━━━━━━━━━━━━━━━━━━┛▒",
                @"▒ Nút Bất Kỳ để hoàn tất ▒",
                @"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒"
            };

            // Hiển thị khung ở giữa màn hình   
            inputnamebox.X = (Console.BufferWidth - namebox[0].Length) / 2;
            inputnamebox.Y = (Console.BufferHeight - namebox.Length) / 2;
            Method.Print(ref inputnamebox, namebox, "Magenta");//Hiển thị khung nhập tên  

            // Hiển thị thông báo giới hạn ký tự bên dưới khung nhập  
            Console.SetCursorPosition((Console.BufferWidth - "Tên không được vượt quá 10 ký tự".Length) / 2, inputnamebox.Y + namebox.Length + 2);
            Console.WriteLine("Tên không được vượt quá 10 ký tự");
            const int maxLength = 10; // Giới hạn số ký tự nhập vào (10 ký tự)  

            Console.SetCursorPosition(inputnamebox.X + 3, inputnamebox.Y + 3);
            playername = Console.ReadLine(); // Đọc tên nhập vào từ người dùng  

            // Kiểm tra xem tên nhập vào có hợp lệ hay không  
            while (string.IsNullOrWhiteSpace(playername) || playername.Length > maxLength)
            {
                // Nếu tên nhập vào vượt quá giới hạn ký tự  
                if (playername.Length > maxLength)
                {
                    Console.Clear(); // Xóa màn hình console  
                    Method.Print(ref inputnamebox, namebox, "Magenta"); // Hiện lại khung nhập  
                    Console.SetCursorPosition(
                        (Console.BufferWidth - "Tên không được vượt quá 10 ký tự. Vui lòng nhập lại!!!".Length)
                        / 2, inputnamebox.Y + namebox.Length + 2);
                    // Hiển thị thông báo lỗi  
                    Console.WriteLine("Tên không được vượt quá 10 ký tự. Vui lòng nhập lại!!!");
                }
                // Nếu tên nhập vào chỉ chứa khoảng trắng  
                else
                {
                    Console.Clear(); // Xóa màn hình console  
                    Method.Print(ref inputnamebox, namebox, "Magenta"); // Hiện lại khung nhập  
                    Console.SetCursorPosition(
                        (Console.BufferWidth - "Tên không được để trống. Vui lòng nhập lại!!!".Length)
                        / 2, inputnamebox.Y + namebox.Length + 2);
                    // Hiển thị thông báo lỗi  
                    Console.WriteLine("Tên không được để trống. Vui lòng nhập lại!!!");
                }

                // Đặt lại con trỏ vào vị trí để người dùng nhập tên  
                Console.SetCursorPosition(inputnamebox.X + 3, inputnamebox.Y + 3);
                playername = Console.ReadLine(); // Đọc lại tên nhập vào từ người dùng  
            }
        }


        // Hiển thị bảng "Game Over" kết thúc trò chơi
        public static bool GameOver()
        {
            Point game = new Point(0, 0);
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
            game.X = (Console.BufferWidth - gameover[0].Length) / 2;//Căn giữa màn hình gameover theo chiều ngang  
            game.Y = (Console.BufferHeight - gameover.Length) / 2;//Căn giữa màn hình gameover theo chiều dọc 
            Method.Print(ref game, gameover, "Magenta");// In bảng "Game Over"   
            Console.SetCursorPosition((Console.BufferWidth -
            "Enter để chơi lại hoặc phím 0 để thoát ".Length) / 2, game.Y + gameover.Length + 2);
            Console.WriteLine("Enter để chơi lại hoặc phím 0 để thoát ");//Căn giữa

            // Vòng lặp vô tận để chờ người chơi đưa ra lựa chọn  
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Đọc phím nhấn từ người chơi mà không hiển thị 
                                                                // Người chơi nhấn phím Enter để bắt đầu lại trò chơi 
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return true;
                }
                // Người chơi nhấn phím '0' hoặc 'NumPad0' để thoát trò chơi  
                else if (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.NumPad0)
                {
                    return false;
                }
                // Nếu người chơi nhấn phím Tab, hiển thị bảng điểm  
                else if (keyInfo.Key == ConsoleKey.Tab)
                {
                    Console.Clear();
                    Scoreboard(); // Gọi phương thức hiển thị bảng điểm  
                }
            }
        }




        //Các phương thức lưu điểm, tính toán và in điểm
        //Tính điểm
        static void countScore(Point objects)
        {
            // Kiểm tra xem người chơi có va chạm với đối tượng không
            if (playerHitbox.IsHit(objects) == true)
                score += 10; // Tăng điểm nếu va chạm rác
            else
                score -= 15; // Giảm điểm nếu va chạm chướng ngại vật

            // Kiểm tra nếu điểm nhỏ hơn 0
            if (score < 0)
            {
                DK = false; // Đặt điều kiện kết thúc game
                Console.Clear(); // Xóa màn hình
                                 // Đặt vị trí con trỏ và thông báo cho người chơi
                Console.SetCursorPosition(((width + 1) * 2 - "Nhấn nút bất kỳ".Length) / 2, height + 2);
                Console.Write("Nhấn nút bất kỳ");
            }
        }

        public static void SaveScore(string playerName, int score)
        {
            // Đường dẫn tới tệp lưu điểm
            string filePath = "scores.txt";
            // Định dạng chuỗi lưu điểm
            string data = $"{playerName},{score}";
            // Mở tệp để thêm điểm
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(data); // Ghi điểm vào tệp
            }
        }
        //Bảng điểm 
        public static void Scoreboard()
        {
            // Đọc các dòng từ tệp và lấy 10 lần chơi gần nhất
            string filePath = "scores.txt";
            var data = File.ReadAllLines(filePath);
            var lines = data.TakeLast(10).Reverse().ToArray(); // Đảo ngược để in theo thứ tự mới nhất

            // Tạo mảng lưu tên và điểm
            (string playerName, int score)[] scores = new (string playerName, int score)[lines.Length];

            // Phân tách các dòng tên và điểm trong tệp và lưu vào mảng
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int nscore))
                {
                    scores[i] = (parts[0], nscore); // Lưu tên và điểm
                }
            }

            // Tìm điểm cao nhất
            var top = scores[0];
            for (int i = 1; i < scores.Length; i++)
            {
                if (scores[i].score > top.score)
                {
                    top = scores[i]; // Cập nhật điểm cao nhất
                }
            }

            // Xác định chiều cao bảng động
            int h = Math.Max(6, scores.Length + 6);  // 4 dòng thêm vào phần khung

            // Tạo khung bảng động
            string topBorder = @"┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓";
            string title = @"                                SCOREBOARD                                  ";
            string separator = @"┃━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┃";
            string bottomBorder = @"┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛";

            // Căn giữa bảng theo chiều rộng console
            int x = (Console.BufferWidth - topBorder.Length) / 2;
            int y = (Console.BufferHeight - h) / 2;

            // In khung trên và tiêu đề
            Console.SetCursorPosition(x, y);
            Console.WriteLine(topBorder);
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine(title);
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine(separator);

            // In tên và điểm của người chơi có điểm cao nhất
            Console.SetCursorPosition(x * 2 - 2, y + 3);
            Console.WriteLine($"Top player: {top.playerName} - Top Score: {top.score}");

            // In nội dung bảng điểm
            for (int i = 0; i < scores.Length; i++)
            {
                Console.SetCursorPosition(x * 2, y + 5 + i);
                Console.WriteLine($"Player: {scores[i].playerName} -- Score: {scores[i].score}");
            }

            // In khung dưới
            Console.SetCursorPosition(x, y + h);
            Console.WriteLine(bottomBorder);
        }

        //in điểm khi chơi
        public static void DisplayScoreBoard(int score)
        {
            Console.SetCursorPosition(((width - 2) * 2 - "Score".Length) / 2, height);
            Console.Write($" Score: {score} ");// In  điểm, làm mới nội dung liên tục
        }





        // Người chơi
        public static void Player()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
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
            player.X = 3;
            player.Y = lane[rnd.Next(0, lane.Length)];
            // In hình đại diện của người dùng ra console lần đầu tiên với tọa độ đã thiết lập  
            Method.Print(ref player, output, "");

            // Vòng lặp chính, chạy liên tục cho đến khi DK false 
            while (DK)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);// Đọc phím nhấn của người dùng mà không hiển thị  
                Method.Clear(player, output);// Xóa hình người chơi ở vị trí cũ 

                if (keyInfo.Key == ConsoleKey.UpArrow && player.Y > 2) // Chặn không cho người chơi vượt biên trên
                    player.Y -= 9; // Giảm tọa độ Y để di chuyển lên 

                if (keyInfo.Key == ConsoleKey.DownArrow && player.Y < 20) // Chặn không cho người chơi vượt biên dưới
                    player.Y += 9; // Tăng tọa độ Y để di chuyển xuống 

                if (keyInfo.Key == ConsoleKey.Escape) // Thoát trò chơi
                {
                    DK = false; // Đặt DK thành false để dừng vòng lặp  
                    Console.Clear();
                    break; // Thoát khỏi vòng lặp  
                }
                // Cập nhật vị trí liên tục cho thuộc tính playerHitbox dựa trên vị trí hiện tại của user  
                playerHitbox.X = player.X + output[0].Length;
                playerHitbox.Y = player.Y;
                Method.Print(ref player, output, ""); // In lại hình người chơi với tọa độ đã cập nhật  
            }
        }





        //Các loại rác và chướng ngại vật
        private static Random rnd = new Random();

        // Xương cá
        public static void Fishbone()
        {
            string[] output = new string[]
            {
            @"     ▄▄   ▄  ▄ ",
            @"   ▄███▄▄█▄▄█▀ ",
            @"    ▀██  ▀▄ ▀█ ",
            };

            int[] lane = { 2, 11, 20 };
            fishbone.X = width * 2 - output[0].Length;
            fishbone.Y = lane[rnd.Next(0, lane.Length)];
            Method.Move(ref fishbone, output, "fish");// Gọi phương thức Move để hoạt động 
        }

        // Túi nhựa  
        public static void Plasticbag()
        {
            string[] output = new string[]
            {
            @"  █ █   ▐ █  ",
            @"  █  █ ▄█ ▐▌ ",
            @"  █  ▀▀▀  █ ",
            @"  ▀█▄▄  ▄█  ",
            };

            int[] lane = { 2, 11, 20 };
            plasticbag.X = width * 2 - output[0].Length;
            plasticbag.Y = lane[rnd.Next(0, lane.Length)];
            Method.Move(ref plasticbag, output, "plastic bag");// Gọi phương thức Move để hoạt động  

        }

        // Chai thủy tinh  
        public static void GlassBottle()
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
            glassbottle.X = width * 2 - output[0].Length;
            glassbottle.Y = lane[rnd.Next(0, lane.Length)];
            Method.Move(ref glassbottle, output, "glass bottle");// Gọi phương thức Move để hoạt động
        }

        // Chướng ngại vật
        public static void Block()
        {
            string[] BlockImage =
            {
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
            @" ████████ ",
                };

            int[] lane = { 2, 11, 20 };
            block.X = width * 2 - BlockImage[0].Length;
            block.Y = lane[rnd.Next(0, lane.Length)];
            Method.Move(ref block, BlockImage, "block");// Gọi phương thức Move để hoạt động
        }




        // Các hàm xử lý
        public class Method
        {
            public static void fixedBorder()
            {

                // Kiểm tra kích thước cửa sổ console  
                if (Console.BufferWidth < 120 || Console.BufferHeight < 30)
                {
                    Console.Clear();
                    string message = "Kích thước cửa sổ quá nhỏ!";
                    // In thông báo ở giữa màn hình nếu có đủ không gian  
                    if (Console.BufferWidth > message.Length)
                    {
                        // Tính toán vị trí để in thông báo ở giữa  
                        Console.SetCursorPosition((Console.BufferWidth - message.Length) / 2, Console.BufferHeight / 2);
                        Console.Write(message);
                    }

                }

                // Chờ cho đến khi kích thước cửa sổ được điều chỉnh đủ lớn  
                while (Console.BufferWidth < 120 || Console.BufferHeight < 30)
                { }
                Console.Clear();
            }

            //In ra màn hình
            public static void Print(ref Point point, string[] output, string color)
            {
                lock (consoleLock)// Khóa để đảm bảo an toàn khi truy cập console  
                {
                    ConsoleColor ColorOfImage = new ConsoleColor();// Khai báo biến lưu màu sắc  


                    if (Enum.TryParse(color, out ColorOfImage))// Kiểm tra nếu màu sắc đầu vào                    
                        Console.ForegroundColor = ColorOfImage;
                    for (int i = 0; i < output.Length; i++)// Vòng lặp qua từng dòng 

                    {

                        Console.SetCursorPosition(point.X, point.Y + i);// Thiết lập vị trí con trỏ 
                                                                        // với tọa độ X được cộng thêm k (để di chuyển theo chiều ngang)  
                                                                        // và tọa độ Y được cộng thêm i (để di chuyển theo chiều dọc)

                        Console.WriteLine(output[i]);
                    }
                }
            }
            // Xóa những hình cũ
            public static void Clear(Point point, string[] output)
            {

                lock (consoleLock)// Khóa để đảm bảo an toàn khi truy cập console                         
                {
                    for (int i = 0; i < output.Length; i++)// Vòng lặp qua từng dòng 
                    {
                        for (int k = 0; k < output[i].Length; k++)// Vòng lặp qua từng ký tự của dòng hiện tại  
                        {
                            Console.SetCursorPosition(point.X + k, point.Y + i);// Thiết lập vị trí con trỏ   
                                                                                // với tọa độ X được cộng thêm k (để di chuyển theo chiều ngang)  
                                                                                // và tọa độ Y được cộng thêm i (để di chuyển theo chiều dọc)

                            Console.WriteLine(' ');// Ghi đè khoảng trắng lên các ký tự cũ  
                        }
                    }
                }
            }

            //Hàm tạo sự di chuyển, kiểm tra va chạm để cập nhật điểm
            public static void Move(ref Point point, string[] print, string objectname)
            {
                int xspawn = point.X; // Lưu tọa độ X ban đầu

                Random rnd = new Random();

                int[] lane = { 2, 11, 20 };

                while (point.X > 0 && DK == true)
                {
                    lock (consoleLock) // Khóa để đảm bảo an toàn khi truy cập console  
                    {
                        bool hasCollided = false; // Đánh dấu nếu xảy ra va chạm  

                        //Di chuyển vật thể
                        Method.Clear(point, print); // Xóa vật thể cũ ở vị trí hiện tại  
                        Method.Print(ref point, print, ""); // In lại vật thể ở vị trí mới  
                        point.X--; // Di chuyển vật thể sang trái  
                        Thread.Sleep(30); // tốc độ di chuyển  


                        //Người chơi va chạm với rác. Điều kiện 1 khi người chơi đụng rác. Điều kiện 2 khi người chơi không đụng rác                      
                        if ((playerHitbox.IsHit(point) && !hasCollided && objectname != "block") || (point.X < 2 && playerHitbox.IsHit(point) == false && objectname != "block"))
                        {
                            countScore(point); // Cập nhật điểm 
                            hasCollided = true; // Đánh dấu đã xảy ra va chạm, tránh tính điểm lại trong lần va chạm này  
                        }

                        DisplayScoreBoard(score); // Hiển thị bảng điểm  

                        // Kiểm tra va chạm với chướng ngại vật 
                        if (objectname == "block" && playerHitbox.IsHit(point))
                        {
                            DK = false; // Dừng lại nếu va chạm với chướng ngại vật
                            Console.Clear();
                            Console.SetCursorPosition(((width + 1) * 2 - "Nhấn nút bất kỳ".Length) / 2, height + 2);
                            Console.Write("Nhấn nút bất kỳ"); // Nhắc nhở người dùng nhấn phím  
                        }

                        //Tạo lại vật thể mới ở vị trí bất kì
                        if (point.X == 1 || hasCollided) // khi đi hết đường hoặc khi va chạm với người chơi
                        {
                            Method.Clear(point, print); // Xóa vật thể cũ  
                            point.X = xspawn; // Trả lại tọa độ X về điểm khởi đầu  
                            point.Y = lane[rnd.Next(0, lane.Length)];
                            Method.Print(ref point, print, ""); // Tạo lại hình ảnh vật thể mới  
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
