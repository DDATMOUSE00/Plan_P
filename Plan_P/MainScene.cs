namespace Plan_P
{
    internal class MainScene
    {
        public static Character _player = new Character();
        public static Inventory inventory = new Inventory();
        static void Main(string[] args)
        {
                
            _player.GetName();
            _player.GetJob();
            ItemList.ItmeListUpdate();
            inventory.StartItemSetting();
            Gamescene();



            /*
                ██████╗ ██╗      █████╗ ███╗   ██╗        ██████╗ 
                ██╔══██╗██║     ██╔══██╗████╗  ██║        ██╔══██╗
                ██████╔╝██║     ███████║██╔██╗ ██║        ██████╔╝
                ██╔═══╝ ██║     ██╔══██║██║╚██╗██║        ██╔═══╝ 
                ██║     ███████╗██║  ██║██║ ╚████║███████╗██║     
                ╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚═╝     
            */

        }
        public static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
        public static int CheckValidInput(int number)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret == number)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static void DrawWalls() // 윈도우 콘솔 기본 창 좌표 (120,60)
        {   // 왜 안나와.. 뭐가 문제지 
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 60; i++)
            {
                Console.Write('=');
            }

            for (int i = 0; i < 29; i++)
            {

                Console.SetCursorPosition(0, i + 1);
                Console.Write('=');
                Console.SetCursorPosition(118, i + 1);
                Console.Write('=');

            }
            Console.SetCursorPosition(0, 29); //가장 테두리 마지막 가로선
            for (int i = 0; i < 60; i++)
            {

                Console.Write('=');

            }
        }
        public static void Gamescene()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8; //.cs
            Console.WriteLine(" ");
            Console.WriteLine(" » ──────» ୨୧⸝⸝˙˳⑅˙⋆꒰🍨꒱﻿⋆﻿˙⑅˙˳⸜⸜୨୧ «────── «");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("██████╗ ██╗      █████╗ ███╗   ██╗        ██████╗ ");
            Console.ResetColor();
            Console.WriteLine("██╔══██╗██║     ██╔══██╗████╗  ██║        ██╔══██╗");
            Console.WriteLine("██████╔╝██║     ███████║██╔██╗ ██║        ██████╔╝");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("██╔═══╝ ██║     ██╔══██║██║╚██╗██║        ██╔═══╝ ");
            Console.ResetColor(); // 컬러 리셋진행
            Console.WriteLine("██║     ███████╗██║  ██║██║ ╚████║███████╗██║     ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚═╝     ");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine(" » ──────» ୨୧⸝⸝˙˳⑅˙⋆꒰🍨꒱﻿⋆﻿˙⑅˙˳⸜⸜୨୧ «────── «");
            Console.WriteLine(" ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" ");
            Console.WriteLine("1. GAME START ");
            Console.WriteLine("0. GAME EXIT ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:

                    break;

                case 1:
                    DisplayGameIntro();
                    break;

            }

        }

        public static void DisplayGameIntro()
        {
            Console.Clear(); // 화면을 비운다.
            Console.Write("                    ");
            Console.WriteLine("                    ");
            Console.WriteLine("                    ");
            Console.WriteLine("                    ");
            Console.WriteLine("                    ");
            Console.Write("                    ");
            Console.WriteLine("[            1. 상태보기            ]");
            Console.WriteLine();
            Console.Write("                    ");
            Console.WriteLine("[            2. 인벤토리            ]");
            Console.WriteLine();
            Console.Write("                    ");
            Console.WriteLine("[            3. 던전가기            ]");
            Console.WriteLine();
            Console.Write("                    ");
            Console.WriteLine("[            0.  나가기             ]");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:

                    break;

                case 1:
                    _player.DisPlayMyInfo();
                    break;

                case 2:
                    Inventory.InventoryScene();
                    break;

                case 3:
                    Battle.BattleMenu(_player);
                    break;
            }

        }

        public static void GameOverScene()
        {
            Console.Clear();

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("당신의 캐릭터는 사망하였습니다.");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine(" 1. 재시작 ");
            Console.WriteLine("");
            Console.WriteLine(" 0. 게임종료");
            Console.WriteLine("");
            Console.WriteLine(" 원하시는 행동을 입력해주세요");
            Console.Write(" >> ");
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 1:
                    DisplayGameIntro();
                    break;
                case 0:
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" 게임을 종료합니다. ");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;
            }
        }
    }

}