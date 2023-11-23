namespace Plan_P
{
    internal class MainScene
    {
        public static Character _player = new Character();
        public static Inventory inventory = new Inventory();

        static void Main(string[] args)
        {
            story();
            Gamescene();
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

        public static void story()
        {

            Border();

            storyLetter();




            Console.SetCursorPosition(40, 26);

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("..... ENTER 눌러서 다음으로 넘어가기 ..... ");
            Console.ResetColor();
            Console.SetCursorPosition(57, 28);
            Console.ReadLine();


        }
        public static void Border()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8; //.cs
            Console.SetCursorPosition(0, 0);
            Console.Write('╔');
            for (int i = 0; i < 118; i++)
            {
                Console.Write('═');
            }
            Console.SetCursorPosition(119, 0);
            Console.Write('╗');

            for (int i = 0; i < 28; i++) // 양사이드 테두리
            {

                Console.SetCursorPosition(0, (int)i + 1);
                Console.Write('║');
                Console.SetCursorPosition(119, (int)i + 1);
                Console.Write('║');
            }

            Console.SetCursorPosition(0, 29); // 모서리
            Console.Write('╚');

            for (int i = 0; i < 118; i++) // 중간테두리
            {
                Console.Write('═');
            }

            Console.SetCursorPosition(119, 29);
            Console.Write('╝'); //모서리
        }

        public static void Gamescene()
        {
            Border();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(3, 4);
            Console.WriteLine("   ▄██████▄     ▄█    █▄     ▄██████▄     ▄████████     ███              ███      ▄██████▄   ▄█     █▄  ███▄▄▄▄   ");
            Console.SetCursorPosition(3, 5);
            Console.WriteLine("  ███    ███   ███    ███   ███    ███   ███    ███ ▀█████████▄      ▀█████████▄ ███    ███ ███     ███ ███▀▀▀██▄");
            Console.SetCursorPosition(3, 6);
            Console.WriteLine("  ███    █▀    ███    ███   ███    ███   ███    █▀     ▀███▀▀██         ▀███▀▀██ ███    ███ ███     ███ ███   ███");
            Console.SetCursorPosition(3, 7);
            Console.WriteLine(" ▄███         ▄███▄▄▄▄███▄▄ ███    ███   ███            ███   ▀          ███   ▀ ███    ███ ███     ███ ███   ███ ");
            Console.ResetColor(); // 컬러 리셋진행
            Console.SetCursorPosition(3, 8);
            Console.WriteLine("▀▀███ ████▄  ▀▀███▀▀▀▀███▀  ███    ███ ▀███████████     ███              ███     ███    ███ ███     ███ ███   ███ ");
            Console.SetCursorPosition(3, 9);
            Console.WriteLine("  ███    ███   ███    ███   ███    ███          ███     ███              ███     ███    ███ ███     ███ ███   ███");
            Console.SetCursorPosition(3, 10);
            Console.WriteLine("  ███    ███   ███    ███   ███    ███    ▄█    ███     ███              ███     ███    ███ ███ ▄█▄ ███ ███   ███ ");
            Console.SetCursorPosition(3, 11);
            Console.WriteLine("  ████████▀    ███    █▀     ▀██████▀   ▄████████▀     ▄████▀           ▄████▀    ▀██████▀   ▀███▀███▀   ▀█   █▀");
            Console.SetCursorPosition(50, 19);
            Console.WriteLine("[ 1. GAME START ]");
            Console.SetCursorPosition(50, 20);
            Console.WriteLine(" ");
            Console.SetCursorPosition(50, 21);
            Console.WriteLine("[ 0. GAME EXIT  ]");
            Console.SetCursorPosition(45, 25);
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(57, 27);
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(49, 28);
                    Console.WriteLine(" 게임을 종료합니다. ");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                    break;

                case 1:
                    _player.GetName();
                    break;

            }
            Console.ReadLine();

        }

        public static void DisplayGameIntro()
        {
            Border();
            Console.SetCursorPosition(40, 5);
            Console.Write("[            1. 상태보기            ]");

            Console.SetCursorPosition(40, 10);
            Console.Write("[            2. 인벤토리            ]");
            Console.SetCursorPosition(40, 13);

            Console.SetCursorPosition(40, 15);
            Console.Write("[            3. 던전가기            ]");

            Console.SetCursorPosition(40, 20);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[            0. 종료하기            ]");
            Console.ResetColor();

            Console.SetCursorPosition(1, 23);
            for (int i = 2; i < 120; i++) // 중간테두리
            {

                Console.Write('─');
            }
            Console.SetCursorPosition(45, 25);
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(57, 27);
            int input = CheckValidInput(0, 3);
            switch (input)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(49, 28);
                    Console.WriteLine(" 게임을 종료합니다. ");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Environment.Exit(0);
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
            Console.ReadLine();
        }
        public static void PlayerID()
        {
            Border();
            _player.GetName();


        }
        public static void ShowAttackScene(Character _player, Monster targetMonster, int damageDealt)
        {

            Battle.BattleMenu(_player);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" {_player.Name} 가 {targetMonster.Name} 를 공격했습니다!");
            Console.WriteLine($" {targetMonster.Name} 에게 {damageDealt} 만큼의 피해를 입혔습니다.");
            Border();
            for (int i = 0; i < 19; i++) // 양사이드 테두리
            {


                Console.SetCursorPosition(70, (int)i + 1);
                Console.Write('│');
            }

            Console.SetCursorPosition(1, 19);
            for (int i = 2; i < 120; i++) // 중간테두리
            {

                Console.Write('─');
            }
            Console.SetCursorPosition(43, 21);
            _player.CriticalPoint = false;

            Console.WriteLine("");
            Console.SetCursorPosition(20, 20);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" {_player.Name} 의 턴입니다.");
            Console.ResetColor();
            Console.WriteLine("");
            Console.SetCursorPosition(20, 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [ 몬스터 정보 ]");
            Console.ResetColor();
            Console.WriteLine("");
            Console.SetCursorPosition(15, 5);
            // Battle.PlayerTurn(_player);


        }
        public static void Battletextscene()
        {
            Border();

            for (int i = 0; i < 28; i++) // 양사이드 테두리
            {

                Console.SetCursorPosition(0, (int)i + 1);
                Console.Write('║');
                Console.SetCursorPosition(119, (int)i + 1);
                Console.Write('║');
            }




        }

        public static void GameOverScene()
        {
            Border();
            Console.SetCursorPosition(4, 4);
            Console.WriteLine("   ▄██████▄     ▄████████   ▄▄▄▄███▄▄▄▄      ▄████████       ▄██████▄   ▄█    █▄     ▄████████    ▄████████ ");
            Console.SetCursorPosition(4, 5);
            Console.WriteLine("  ███    ███   ███    ███ ▄██▀▀▀███▀▀▀██▄   ███    ███      ███    ███ ███    ███   ███    ███   ███    ███ ");
            Console.SetCursorPosition(4, 6);
            Console.WriteLine("  ███    █▀    ███    ███ ███   ███   ███   ███    █▀       ███    ███ ███    ███   ███    █▀    ███    ███ ");
            Console.SetCursorPosition(4, 7);
            Console.WriteLine(" ▄███          ███    ███ ███   ███   ███  ▄███▄▄▄          ███    ███ ███    ███  ▄███▄▄▄      ▄███▄▄▄▄██▀ ");
            Console.SetCursorPosition(4, 8);
            Console.WriteLine("▀▀███ ████▄  ▀███████████ ███   ███   ███ ▀▀███▀▀▀          ███    ███ ███    ███ ▀▀███▀▀▀     ▀▀███▀▀▀▀▀   ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(4, 9);
            Console.WriteLine("  ███    ███   ███    ███ ███   ███   ███   ███    █▄       ███    ███ ███    ███   ███    █▄  ▀███████████ ");
            Console.SetCursorPosition(4, 10);
            Console.WriteLine("  ███    ███   ███    ███ ███   ███   ███   ███    ███      ███    ███ ███    ███   ███    ███   ███    ███ ");
            Console.SetCursorPosition(4, 11);
            Console.WriteLine("  ████████▀    ███    █▀   ▀█   ███   █▀    ██████████       ▀██████▀    ▀████▀     ██████████   ███    ███ ");
            Console.SetCursorPosition(4, 12);
            Console.WriteLine("  ████   ██    ██ ██    ██ █████ ██                                                              ███    ███ ");
            Console.SetCursorPosition(4, 13);
            Console.WriteLine("   █      █    █  █      █  ██ █  █                                                                     █ █ ");

            Console.SetCursorPosition(44, 16);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("당신의 캐릭터는 사망하였습니다.");
            Console.ResetColor();

            Console.WriteLine("");
            Console.SetCursorPosition(50, 19);
            Console.WriteLine("[ 1. 다시 시작 ]");
            Console.SetCursorPosition(50, 20);
            Console.WriteLine(" ");
            Console.SetCursorPosition(50, 21);
            Console.WriteLine("[ 0. 게임 종료 ]");
            Console.SetCursorPosition(45, 25);
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(57, 27);
            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 1:
                    Console.WriteLine("게임을 다시 시작합니다");
                    ResetGame();
                    story();
                    Gamescene();
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
            Console.ReadLine();

        }

        public static void StartGameSet()
        {
            ItemList.ItmeListUpdate();
            inventory.StartItemSetting();
        }

        public static void ResetGame()
        {
            Character _player = new Character();
            Inventory inventory = new Inventory();
        }


        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }
            return length;
        }

        public static string PadRightForMixedtext(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

        static void storyLetter()
        {
            Console.SetCursorPosition(20, 2);
            int x = 20; // 초기 X 위치
            int y = 2; // 초기 Y 위치

            string sentence = "여행을 마치고 집으로 돌아가는 길" +
                          "\n어디선가 종소리가 들린다 갑자기 안개가 주변을 감싸며 눈앞이 흐려졌다.. " +
                          "\n다시 눈을 떠보니 안개 가득한 동네.. " +
                          "\n들리는 소리는 내 발소리 하나... \n주변을 둘러보니 여긴 '텔레마' 라는 동네인 것 같다" +
                          "\n들어본 적 있는 이름 인데? '텔레마' 악마를 숭배 하던 사이비 종교의 예배당 같은 곳이다." +
                          "\n여긴 폐허가 된 마을 일텐데..? 갑자기 누군가 나를 노리고 따라오고 있다!! " +
                          "\n!! 나를 노리는 자들을 헤치우고 이 마을을 탈출해야해 !!\n";


            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y = y + 3; // 초기 Y 위치로 재설정
                    x = 20; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);

                }
                else
                {

                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }

                Thread.Sleep(1); // msec 지연

            }
            RedBorder();
            storyLetterRed();
        }


        static void storyLetterRed()
        {
            Console.SetCursorPosition(20, 2);
            int x = 20; // 초기 X 위치
            int y = 2; // 초기 Y 위치

            Console.ForegroundColor = ConsoleColor.DarkRed;
            string sentence = "여행을 마치고 집으로 돌아가는 길" +
                          "\n어디선가 종소리가 들린다 갑자기 안개가 주변을 감싸며 눈앞이 흐려졌다.. " +
                          "\n다시 눈을 떠보니 안개 가득한 동네.. " +
                          "\n들리는 소리는 내 발소리 하나... \n주변을 둘러보니 여긴 '텔레마' 라는 동네인 것 같다" +
                          "\n들어본 적 있는 이름 인데? '텔레마' 악마를 숭배 하던 사이비 종교의 예배당 같은 곳이다." +
                          "\n여긴 폐허가 된 마을 일텐데..? 갑자기 누군가 나를 노리고 따라오고 있다!! " +
                          "\n!! 나를 노리는 자들을 헤치우고 이 마을을 탈출해야해 !!\n";


            foreach (char letter in sentence)
            {
                if (letter == '\n') // 새 줄 문자 확인
                {
                    y = y + 3; // 초기 Y 위치로 재설정
                    x = 20; // 초기 X 위치로 재설정
                    Console.SetCursorPosition(x, y);
                }
                else
                {

                    Console.Write(letter);
                    x++; // 다음 문자 위치로 이동
                }
            }
        }

        public static void RedBorder()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8; //.cs
            Console.SetCursorPosition(0, 0);
            Console.Write('╔');
            for (int i = 0; i < 118; i++)
            {
                Console.Write('═');
            }
            Console.SetCursorPosition(119, 0);
            Console.Write('╗');

            for (int i = 0; i < 28; i++) // 양사이드 테두리
            {

                Console.SetCursorPosition(0, (int)i + 1);
                Console.Write('║');
                Console.SetCursorPosition(119, (int)i + 1);
                Console.Write('║');
            }

            Console.SetCursorPosition(0, 29); // 모서리
            Console.Write('╚');

            for (int i = 0; i < 118; i++) // 중간테두리
            {
                Console.Write('═');
            }

            Console.SetCursorPosition(119, 29);
            Console.Write('╝'); //모서리
        }
    }
}