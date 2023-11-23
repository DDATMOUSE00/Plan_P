using System;
using System.Threading;
namespace Plan_P
{
    enum MonsterType
    {
        //변수설정 1234
        None = 0,
        Slime = 1,
        Bubbling = 2,
        KingSlime = 3,
        dog = 4,
    }

    internal class Monster
    {
        public string Name;
        public int Dmg;
        public int Hp;
        public int Exp;
        public int Level;

        public int DisplayRow { get; set; }

        public bool IsDead = false;
        public bool IsMonsterTurn = false;

        MonsterType Type;

        public Monster(string name, int dmg, int hp, int exp, int level, MonsterType type)
        {
            Name = name;
            Dmg = dmg;
            Hp = hp;
            Exp = exp;
            Level = level;
            Type = type;
        }

        // 몬스터의 공격 
        public void AttackPlayer(Character _player)
        {

            // if 몬스커가 죽지않았거나 && 몬스터의 턴 ture 값을 둘다 충족한다면
            if (!IsDead && IsMonsterTurn)
            {
                int monsterDmg = Dmg;
                GroundReset();

                _player.Hp -= monsterDmg;

                Console.SetCursorPosition(80, 7);
                Console.WriteLine("");
                Console.SetCursorPosition(85, 8);
                Console.Write("");
                Console.Write($"◀ {Name}의 공격 ▶ ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(83, 10);
                Console.Write("");
                Console.WriteLine($"몬스터에게 받은 피해량 : {monsterDmg} ");
                Console.ResetColor();
                Console.SetCursorPosition(83, 12);
                int playerIsDeadHp = Math.Max(0, _player.Hp); // Max값 0 
                Console.WriteLine($" {_player.Name}의 남은 체력 : {playerIsDeadHp} ");

            }

        }

        // 몬스터가 죽었을 때
        public void MonsterIsDead(List<Monster> monsters)
        {
            IsDead = true;
            IsMonsterTurn = false;

            GroundReset();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("");
            Console.SetCursorPosition(75, 6);
            Console.WriteLine($"  {Name}의 체력이 0이 되어 죽었습니다.");
            Thread.Sleep(1000);
            Console.WriteLine("");
            Console.ResetColor();
            bool monsterAllDead = AllMonstersDead(monsters); // 필드의 몬스터가 모두 죽었는지 확인

            // 필드의 몬스터가 모두 죽었다면
            if (monsterAllDead)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(78, 9);
                Console.WriteLine(" 필드의 모든 몬스터를 처치했습니다 !");
                Console.ResetColor();
                int totalExperience = 0;

                // 토탈 경험치를 얻는다 !
                foreach (var monster in monsters)
                {
                    totalExperience += monster.Exp;
                }
                MainScene._player.Exp += totalExperience;

                Console.SetCursorPosition(82, 12);
                Console.WriteLine($" {totalExperience}만큼의 경험치를 얻었다!");

                if (MainScene._player.Exp >= MainScene._player.LevelUpExp)
                {
                    MainScene._player.LevelUp();
                }

                Thread.Sleep(1000);
                MainScene.DisplayGameIntro();
            }

        }

        public static void MonstersTurn(Character _player, List<Monster> monsters)
        {

            foreach (Monster monster in monsters)
            {
                if (!monster.IsDead)
                {
                    if (monster.Hp <= 0)
                    {
                        monster.MonsterIsDead(monsters);
                    }
                    monster.IsMonsterTurn = true;
                    monster.AttackPlayer(_player);
                    Thread.Sleep(1000);
                    monster.IsMonsterTurn = false;

                    if (_player.Hp <= 0)
                    {
                        int bonusDmg = EquipmentItem.GetEquipDmg();
                        Console.SetCursorPosition(3, 22);
                        Console.ForegroundColor = ConsoleColor.Red;
                        int playerIsDeadHp = Math.Max(0, _player.Hp); // Max값 0 
                        Console.Write($" {_player.Name} [ {_player.Job} ] Lv : ( {_player.Level} )   ◈ 체력 : {playerIsDeadHp} / {_player.MaxHp}  ◈ 공격력 : {_player.Dmg + bonusDmg} (+ {bonusDmg})");
                        Console.ResetColor();
                        Battle.PlayerIsDead(_player, monster);
                    }
                }
            }
            Console.SetCursorPosition(82, 15);
            Console.WriteLine("몬스터의 공격이 끝났습니다");
            Console.ReadKey();
            Console.Clear();
        }
        public static bool AllMonstersDead(List<Monster> monsters)
        {
            return monsters.All(monster => monster.IsDead);
        }

        public static List<Monster> CreateMonster()
        {
            int minMonsters = 1;
            int maxMonsters = 3;

            int randomMonsters = new Random().Next(minMonsters, maxMonsters + 1);


            List<Monster> monsters = new List<Monster>();

            Random random = new Random();

            for (int i = 0; i < randomMonsters; i++)
            {
                string name = $"{i + 1}";
                int dmg = 0;
                int hp = 0;
                int exp = 0;
                int level = 0;

                MonsterType type = (MonsterType)random.Next(1, 5);

                switch (type)
                {

                    case MonsterType.Slime:
                        name = $"아픈 유령";
                        level = 1;
                        dmg = 3;
                        hp = 10;
                        exp = 10;
                        break;
                    case MonsterType.Bubbling:
                        name = $"얼굴없는간호사";
                        level = 8;
                        dmg = 8;
                        hp = 20;
                        exp = 30;
                        break;
                    case MonsterType.KingSlime:
                        name = $"사형집행자";
                        level = 10;
                        dmg = 20;
                        hp = 50;
                        exp = 50;
                        break;
                    case MonsterType.dog:
                        name = $"좀비개";
                        level = 3;
                        dmg = 5;
                        hp = 30;
                        exp = 50;
                        break;
                    default:
                        name = $"{i + 1}";
                        break;
                }
                monsters.Add(new Monster(name, dmg, hp, exp, level, type));
            }
            return monsters;
        }
        public static void GroundReset()
        {
            int startingRow = 5;

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(77, startingRow + i);
                Console.WriteLine("                                       ");

            }


        }
    }
}
