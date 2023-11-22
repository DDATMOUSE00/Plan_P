using System;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace Plan_P
{
    enum Job
    {
        None = 0,
        Warrior,
        Archer,
        Mage
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
        public int MaxHP { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int LevelUpExp { get; set; }

        public bool IsDead = false;

        public bool IsPlayerTurn = true;

        public bool IsPlayerEscape = false;

        public void GetName()
        {
            Console.WriteLine("이름을 입력해주세요.");
            string name = Console.ReadLine();
            Name = name;
        }
        public void GetJob()
        {
            Console.Clear();

            Console.WriteLine("직업을 선택하세요.");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 궁수");
            Console.WriteLine("3. 마법사");

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
                            MaxHP = 100;
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
                            MaxHP = 80;
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
                            Hp = 50;
                            MaxHP = 50;
                            Gold = 0;
                            Exp = 0;
                            LevelUpExp = 10;
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Task.Delay(500).Wait();
                GetJob();
            }
        }

        public void DisPlayMyInfo()
        {
            Console.Clear();
            int bonusDmg = EquipmentItem.GetEquipDmg();
            int bonusDef = EquipmentItem.GetEquipDef();
            int bonusMaxHp = EquipmentItem.GetEquipMaxHp();
            Console.WriteLine($"이 름 : {Name}");
            Console.WriteLine($"레 벨 : {Level} ({((double)Exp * 100 / (double)LevelUpExp).ToString("N2")}%)");
            Console.WriteLine($"경험치 : {Exp} / {LevelUpExp}");
            Console.WriteLine($"직 업 : {Job}");
            Console.WriteLine($"공격력 : {Dmg + bonusDmg}");
            Console.WriteLine($"방어력 : {Def + bonusDef}");
            Console.WriteLine($"체 력 : {Hp} / {MaxHP}");
            Console.WriteLine($"Gold : {Gold}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. 나가기");
            Console.ResetColor();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = MainScene.CheckValidInput(0);
            switch (input)
            {
                case 0:
                    // 메인 화면
                    MainScene.DisplayGameIntro();
                    break;
            }
        }

        public void LevelUp()
        {
            if (Exp >= LevelUpExp)
            {
                Console.WriteLine("레벨 업");
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

        public void PlayerEscapePiont()
        {
            float playerEscape = 1.33f;

            if (playerEscape >= 1 + (new Random().NextDouble() * 1))
            {
                IsPlayerEscape = true;
            }
        }

        public void PlayerEscape()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" 성공적으로 도망쳤다!");
            Thread.Sleep(1000);
            Console.ResetColor();
            IsPlayerEscape = false;
            MainScene.DisplayGameIntro();
        }
    }
}