using System;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Plan_P
{
    enum Job
    {
        None = 0,
        퇴마사,
        경찰,
        학생
    }
    public class Character
    {
        public string? Name { get; set; }
        public string? Job { get; set; }
        public int Level { get; set; }
        public int Dmg { get; set; }
        public bool CriticalPoint { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int LevelUpExp { get; set; }

        public bool IsDead = false;

        public bool IsPlayerTurn = true;

        public bool IsPlayerEscape = false;

        public void GetName()
        {
            MainScene.Border();
            Console.SetCursorPosition(37, 8);
            Console.Write("╔⊶⊶⊶⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊶⊷⊶✞⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷╗");
            Console.SetCursorPosition(42, 13);
            Console.Write("    당신의  이름은 무엇입니까?            ");
            Console.SetCursorPosition(37, 18);
            Console.Write("╚⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶✞⊷⊷⊷⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶╝");
            Console.SetCursorPosition(54, 15);
            string name = Console.ReadLine();
            Name = name;
            GetJob();

        }

        public void GetJob()
        {

            MainScene.Border();
            Console.SetCursorPosition(37, 2);
            Console.Write("╔⊶⊶⊶⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊶⊷⊶✞⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷╗");
            Console.SetCursorPosition(42, 4);
            Console.Write("    당신의  직업은 무엇입니까?            ");
            Console.SetCursorPosition(37, 6);
            Console.Write("╚⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶✞⊷⊷⊷⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶╝");

            Console.SetCursorPosition(40, 10);
            Console.Write("[            1. 퇴 마 사             ]");
            Console.SetCursorPosition(40, 13);
            Console.Write("[            2. 경    찰             ]");
            Console.SetCursorPosition(40, 16);
            Console.Write("[            3. 학    생             ]");
            Console.SetCursorPosition(45, 25);
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(1, 23);
            for (int i = 2; i < 120; i++) // 중간테두리
            {

                Console.Write('─');
            }
            Console.SetCursorPosition(57, 27);

            bool isCorrect = int.TryParse(Console.ReadLine(), out int num);
            if (isCorrect && num > 0 && num < Enum.GetValues(typeof(Job)).Length)
            {
                Job job = (Job)num;

                switch (num)
                {
                    case 1:
                        {
                            Job = job.ToString();
                            Level = 1;
                            Dmg = 10;
                            Def = 5;
                            Hp = 100;
                            MaxHp = 100;
                            Gold = 0;
                            Exp = 0;
                            LevelUpExp = 10;
                            break;
                        }
                    case 2:
                        {
                            Job = job.ToString();
                            Level = 1;
                            Dmg = 20;
                            Def = 2;
                            Hp = 80;
                            MaxHp = 80;
                            Gold = 0;
                            Exp = 0;
                            LevelUpExp = 10;
                            break;
                        }
                    case 3:
                        {
                            Job = job.ToString();
                            Level = 1;
                            Dmg = 10;
                            Def = 0;
                            Hp = 80;
                            MaxHp = 80;
                            Gold = 0;
                            Exp = 0;
                            LevelUpExp = 10;
                            break;
                        }
                }
                MainScene.StartGameSet();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Task.Delay(500).Wait();
                GetJob();
            }

            MainScene.DisplayGameIntro();
        }


        // 플레이어 상태창
        public void DisPlayMyInfo()
        {

            // 플레이어에게 장비 아이템 능력치 부여 
            int bonusDmg = EquipmentItem.GetEquipDmg();
            int bonusDef = EquipmentItem.GetEquipDef();
            int bonusMaxHp = EquipmentItem.GetEquipMaxHp();

            MainScene.Border();
            Console.SetCursorPosition(50, 4);
            Console.WriteLine($"이 름  : {Name}");
            Console.SetCursorPosition(50, 6);
            Console.WriteLine($"레 벨 : {Level} ({((double)Exp * 100 / (double)LevelUpExp).ToString("N2")}%)");
            Console.SetCursorPosition(50, 8);
            Console.WriteLine($"경험치 : {Exp} / {LevelUpExp}");
            Console.SetCursorPosition(50, 10);
            Console.WriteLine($"직 업  : {Job}");
            Console.SetCursorPosition(50, 12);
            Console.WriteLine($"공격력 : {Dmg + bonusDmg} (+ {bonusDmg})");
            Console.SetCursorPosition(50, 14);
            Console.WriteLine($"방어력 : {Def + bonusDmg} (+ {bonusDef})");
            Console.SetCursorPosition(50, 16);
            Console.WriteLine($"체 력  : {Hp} / {MaxHp}");
            Console.SetCursorPosition(50, 18);
            Console.WriteLine($"Gold   : {Gold}");

            Console.WriteLine();
            Console.SetCursorPosition(40, 20);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[            0. 나가기              ]");
            Console.ResetColor();
            Console.SetCursorPosition(1, 23);
            for (int i = 2; i < 120; i++) // 중간테두리
            {

                Console.Write('─');
            }
            Console.SetCursorPosition(43, 25);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(43, 26);
            int input = MainScene.CheckValidInput(0);
            switch (input)
            {
                case 0:
                    // 메인 화면
                    MainScene.DisplayGameIntro();
                    break;
            }
            Console.ReadLine();

        }

        // 레벨업
        public void LevelUp()
        {
            if (Exp >= LevelUpExp)
            {

                Console.WriteLine("");
                Console.SetCursorPosition(92, 15);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("레벨 업");
                Console.ResetColor();
                Console.ReadKey();
                Level += 1;
                Dmg = Dmg + ((Level * 1) - 1);
                Def = Def + ((Level * 1) - 1);
                Exp = Exp - LevelUpExp;
                LevelUpExp += 15 + (Level * 5);
                if (Exp >= LevelUpExp)
                {
                    LevelUp();
                }
            }
        }
        public void UseConsumptionItem(ConsumptionItem consumptionItem)
        {
            Hp += consumptionItem.RecoveredHp;
        }

        // 전투에서 도망치기 확률
        public void PlayerEscapePiont()
        {
            float playerEscape = 1.33f;

            if (playerEscape >= 1 + (new Random().NextDouble() * 1))
            {
                IsPlayerEscape = true;
            }
        }

        // 전투에서 도망치기
        public void PlayerEscape()
        {
            Console.SetCursorPosition(83, 6);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 성공적으로 도망쳤다!");
            Thread.Sleep(1000);
            Console.ResetColor();
            IsPlayerEscape = false;
            MainScene.DisplayGameIntro();
        }
    }
}