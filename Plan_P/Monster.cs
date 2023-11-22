using System;
using System.Threading;
/*
using System.ComponentModel;​
*/
namespace Plan_P
{
    enum MonsterType
    {
        None = 0,
        Slime = 1,
        Bubbling = 2,
        KingSlime = 3,
    }

    internal class Monster
    {
        public string Name;
        public int Dmg;
        public int Hp;
        public int Exp;

        public bool IsDead = false;
        public bool IsMonsterTurn = false;

        MonsterType Type;

        public Monster(string name, int dmg, int hp, int exp, MonsterType type)
        {
            Name = name;
            Dmg = dmg;
            Hp = hp;
            Exp = exp;

            Type = type;
        }

        public void AttackPlayer(Character _player)
        {
            if (!IsDead && IsMonsterTurn)
            {
                int monsterDmg = Dmg;

                _player.Hp -= monsterDmg;
                Console.Write($"  {Name}의 공격 ! ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  받은 피해 : {monsterDmg} ");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"  [ {_player.Name} ]  HP : {_player.Hp} ");
                Console.WriteLine("");
            }
        }


        public void MonsterIsDead(List<Monster> monsters)
        {
            IsDead = true;
            IsMonsterTurn = false;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("");
            Console.WriteLine($" {Name}의 체력이 0이 되어 죽었습니다.");
            Console.ResetColor();
            //_player.LevelUp();
            bool monsterAllDead = AllMonstersDead(monsters);

            if(monsterAllDead)
            {
                Console.WriteLine("");
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine(" 모든 몬스터를 처치했습니다 !");
                Console.ResetColor();
                int totalExperience = 0;


                foreach (var monster in monsters)
                {
                    totalExperience += monster.Exp;
                }
                MainScene._player.Exp += totalExperience;
                if (MainScene._player.Exp >= MainScene._player.LevelUpExp)
                {
                    MainScene._player.LevelUp();
                }
                Console.ReadKey();
                MainScene.DisplayGameIntro();
            }
        
        }

        public static void MonstersTurn(Character _player, List<Monster> monsters)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [ 몬스터의 공격 ]");
            Console.ResetColor();
            Console.WriteLine("");

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
                        Battle.PlayerIsDead(_player, monster);
                    }
                }
            }
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

                MonsterType type = (MonsterType)random.Next(1, 4);

                switch (type)
                {

                    case MonsterType.Slime:
                        name = $"슬라임";
                        dmg = 3;
                        hp = 10;
                        exp = 10;
                        break;
                    case MonsterType.Bubbling:
                        name = $"버블링";
                        dmg = 5;
                        hp = 20;
                        exp = 30;
                        break;
                    case MonsterType.KingSlime:
                        name = $"킹 슬라임";
                        dmg = 10;
                        hp = 50;
                        exp = 50;
                        break;
                    default:
                        name = $"{i + 1}";
                        break;
                }
                monsters.Add(new Monster(name, dmg, hp, exp, type));
            }
            return monsters;
        }
    }
}
