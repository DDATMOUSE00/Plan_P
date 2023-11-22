using System;
using System.Numerics;
using System.Xml.Linq;
/*
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;​
*/

namespace Plan_P
{
    internal class Battle
    {
        static Battle battle = new Battle();

        public static void BattleMenu(Character _player)
        {
            Console.Clear();

            List<Monster> monsters = Monster.CreateMonster();

            while (!Monster.AllMonstersDead(monsters) && _player.Hp > 0)
            {
                PlayerTurn(_player, monsters);

                if (Monster.AllMonstersDead(monsters) || _player.Hp <= 0)
                {
                    break;
                }

                Monster.MonstersTurn(_player, monsters);

            }
        }

        // 플레이어 턴에 무엇을 할 것인지 
        public static void PlayerTurn(Character _player, List<Monster> monsters)
        {
            _player.CriticalPoint = false;

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" {_player.Name} 의 턴입니다.");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [ 몬스터 정보 ]");
            Console.ResetColor();
            Console.WriteLine("");

            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(PadRightForMixedtext($"[{i + 1}]  {monsters[i].Name}", 15));
                    Console.Write(" │ ");

                    int IsDeadMonsterHp = Math.Max(0, monsters[i].Hp);
                    Console.WriteLine(PadRightForMixedtext($" HP : {IsDeadMonsterHp}", 18));
                    Console.ResetColor();
                }
                else if (monsters[i].IsDead == false)
                {
                    Console.Write(PadRightForMixedtext($"[{i + 1}]  {monsters[i].Name}", 15));
                    Console.Write(" │ ");
                    Console.WriteLine(PadRightForMixedtext($" HP : {monsters[i].Hp}", 18));
                }
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write(" 플레이어 : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" [ {_player.Name} ]  체력 [ {_player.Hp} ]  공격력 [ {_player.Dmg} ]");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" 1. 일반공격");
            Console.WriteLine("");
            Console.WriteLine(" 2. 스킬공격");
            Console.WriteLine("");
            Console.WriteLine(" 3. R U N");
            Console.WriteLine("");
            Console.WriteLine(" 원하시는 행동을 입력해주세요 ");
            Console.Write(" >> ");


            

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("");
                    Console.WriteLine(" 공격하실 몬스터를 선택해주세요 ");
                    Console.Write(" >> ");
                    
                    string attackInput = Console.ReadLine();

                    if (int.TryParse(attackInput, out int selectedMonster) && selectedMonster >= 1 && selectedMonster <= monsters.Count)
                    {

                        Monster targetMonster = monsters[selectedMonster - 1];

                        if (targetMonster.IsDead)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(" 이미 사망한 몬스터입니다. 다른 대상을 선택해주세요");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($" 공격 대상은 {targetMonster.Name}");
                            battle.AttackMonster(_player, targetMonster);

                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                        }
                    }
                    else
                    {
                        Console.Write(" 잘못된 입력입니다 다시 입력해주세요. ");
                    }
                }
                else if (input == "2")
                {
                    Console.Clear();
                    Console.WriteLine(" 아직 미구현입니다. 턴을 사용했으니 맞아야합니다. ");
                    Thread.Sleep(1000);
                    break;
                }
                else if (input == "3") // 도망 확률 33퍼 PlayerEscape
                {
                    _player.PlayerEscapePiont();

                    if (_player.IsPlayerEscape == true)
                    {
                        _player.PlayerEscape();
                        break;
                    }
                    else if (_player.IsPlayerEscape == false)
                    {
                        Console.WriteLine(" 도망에 실패했다. 턴을 사용했으니 맞아야합니다.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    }
                }
                else
                {
                    Console.Write(" 잘못된 입력입니다 다시 입력해주세요. ");
                }
            }
        }

        // 플레이어 -> 몬스터 공격 함수
        public void AttackMonster(Character _player, Monster targetMonster)
        {

            int baseDamage = _player.Dmg;
            double randomDamage = 1.0 + (new Random().NextDouble() * 0.2 - 0.1);
            int damage = (int)(baseDamage * randomDamage);
            double critical = 1.6;
            double criticalper = 1.5;
            int criticalDamage = ((int)(damage * critical));

            if (criticalper >= 1 + (new Random().NextDouble() * 1))
            {
                _player.CriticalPoint = true;
            }



            if (!_player.IsDead && targetMonster != null && !targetMonster.IsDead)
            {

                if (_player.CriticalPoint == true)
                {
                    targetMonster.Hp -= criticalDamage; // 크리티컬 ture 일때

                    Console.WriteLine("");
                    Console.WriteLine(" [데미지 리포트]");
                    Console.WriteLine("");
                    Console.Write($"[ {_player.Name} ]의 크리티컬 공격 ! [ {targetMonster.Name} ]에게 입힌피해 : ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{criticalDamage} ");
                    Console.ResetColor();
                    Console.WriteLine($" [ {targetMonster.Name} ]의 남은 HP : {targetMonster.Hp} ");
                    Thread.Sleep(1000);
                    _player.CriticalPoint = false;
                    Console.ReadKey();
                }
                else if (!_player.CriticalPoint)  // 일반 공격
                {
                    targetMonster.Hp -= damage;
                    Console.WriteLine("");
                    Console.WriteLine(" [데미지 리포트]");
                    Console.WriteLine("");
                    Console.Write($" [ {_player.Name} ]의 공격 ! [ {targetMonster.Name} ]에게 입힌피해 : ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{damage}");
                    Console.ResetColor();
                    Console.WriteLine($" [ {targetMonster.Name} ]의 남은 HP : {targetMonster.Hp} ");
                    Thread.Sleep(1000);
                    Console.ReadKey();
                }

                // 플레이어 턴 false 
                // 몬스터 턴 true
                _player.IsPlayerTurn = false;
                targetMonster.IsMonsterTurn = true;
                Console.Clear();

            }
        }

        // 플레이어가 몬스터에게 죽었을 때
        public static void PlayerIsDead(Character _player, Monster targetMonster)
        {
            if (_player.IsDead = true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {targetMonster.Name}의 공격으로 캐릭터가 사망하였습니다 ! ");
                Console.WriteLine($"  {_player.Name}가 죽었습니다.");
                Console.WriteLine("");
                Console.ResetColor();
                Thread.Sleep(2000);
                Console.ReadKey();
                MainScene.GameOverScene();

            }
        }


        // 텍스트 줄맞춤용 함수
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
        // 텍스트 줄맞춤용 함수
    }
}