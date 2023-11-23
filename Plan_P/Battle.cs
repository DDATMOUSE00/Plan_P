using System;
using System.Numerics;
using System.Xml.Linq;

namespace Plan_P
{
    internal class Battle
    {
        static Battle battle = new Battle();

        public static void BattleMenu(Character _player)
        {
            Console.Clear();

            // Monster 리스트에서 랜덤 몬스터 생성 [ CreateMonster(); 랜덤몬스터 함수 ]
            List<Monster> monsters = Monster.CreateMonster();


            // 조건을 충족할때 까지 반복
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
            MainScene.Border();
            for (int i = 0; i < 19; i++) // 콘솔창 양사이드 테두리
            {


                Console.SetCursorPosition(70, (int)i + 1);
                Console.Write('│');
            }

            Console.SetCursorPosition(1, 19);
            for (int i = 2; i < 120; i++) // 콘솔창 중간테두리
            {

                Console.Write('─');
            }
            Console.SetCursorPosition(43, 21);
            _player.CriticalPoint = false;

            Console.WriteLine("");
            Console.SetCursorPosition(5, 20);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" [ 플레이어 ]");
            Console.ResetColor();
            Console.WriteLine("");
            Console.SetCursorPosition(20, 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" [ 필드 몬스터 정보 ]");
            Console.ResetColor();
            Console.WriteLine("");
            Console.SetCursorPosition(87, 3);
            Console.WriteLine(" [ 전투 정보 ]");


            int startingRow = 5;

            // 몬스터 Count 숫자에 맞춰 Write의 y값을 ++ 
            // y축의 첫 시작값은 ㄴtartingRow = 5
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.SetCursorPosition(15, startingRow + i); // (x, y) x = 15, y = 5 ++

                // 필드의 몬스터들중에 죽은 몬스터가 있다면
                if (monsters[i].IsDead)
                {
                    // 이름색을 변화
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(PadRightForMixedtext($"[{i + 1}]Lv:{monsters[i].Level} {monsters[i].Name}", 15));
                    Console.Write(" │ ");

                    // 죽은 몬스터의 Hp가 마이너스까지 떨어진다면 Max 값 0으로 주고 변환
                    int IsDeadMonsterHp = Math.Max(0, monsters[i].Hp);
                    Console.WriteLine(PadRightForMixedtext($" 체력 :  {IsDeadMonsterHp}", 18));
                    Console.ResetColor();
                }
                else if (monsters[i].IsDead == false) // 죽지않았다면
                {
                    Console.Write(PadRightForMixedtext($"[{i + 1}] Lv:{monsters[i].Level}{monsters[i].Name}", 15));
                    Console.Write(" │ ");
                    Console.WriteLine(PadRightForMixedtext($" 체력 :  {monsters[i].Hp}", 18));
                }
            }
            int bonusDmg = EquipmentItem.GetEquipDmg();

            Console.SetCursorPosition(3, 22);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" {_player.Name} [ {_player.Job} ] Lv : ( {_player.Level} )   ◈ 체력 : {_player.Hp} / {_player.MaxHp}  ◈ 공격력 : {_player.Dmg + bonusDmg} (+ {bonusDmg})");
            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.SetCursorPosition(10, 24);
            Console.WriteLine(" [  1.일반공격  ]");
            Console.WriteLine("");
            Console.SetCursorPosition(10, 26);
            Console.WriteLine(" [  2. R U N    ]");
            Console.WriteLine("");
            Console.SetCursorPosition(80, 21);
            Console.WriteLine(" 원하시는 행동을 입력해주세요 ");
            Console.SetCursorPosition(80, 22);
            Console.Write(" >> ");





            while (true)
            {
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("");
                    Console.SetCursorPosition(80, 24);
                    Console.WriteLine(" 공격하실 몬스터를 선택해주세요 ");
                    Console.SetCursorPosition(80, 25);
                    Console.Write(" >> ");

                    string attackInput = Console.ReadLine();

                    // 공격할 몬스터를 입력을 받았을때 
                    if (int.TryParse(attackInput, out int selectedMonster) && selectedMonster >= 1 && selectedMonster <= monsters.Count)
                    {

                        // attackInput 은 ReadLine()  string 값을 받아 int로 변환   && 
                        // selectedMonster (선택값)이 >= 1  과 같거나 크다면           &&
                        // selectedMonster <= monsters.Count (선택값)이 monsters.Count에 들어 있는 값과 같거나 작다면


                        // 몬스터의 targetMonster = monsters[-1]
                        // 리스트에 할당된 값의 첫 숫자는 "0"
                        // 플레이어가 1 ~ Count 값을 입력 플레이어의 "1" 은 -1 = 몬스터 [0]번에 할당
                        Monster targetMonster = monsters[selectedMonster - 1];

                        // if 타겟된 몬스터가 죽었다면
                        if (targetMonster.IsDead)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.SetCursorPosition(80, 6);
                            Console.WriteLine(" 이미 사망한 몬스터입니다.");
                            Console.SetCursorPosition(80, 8);
                            Console.WriteLine(" 다른 몬스터를 선택해주세요 ");
                            Console.SetCursorPosition(80, 22);
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            Monster.GroundReset();
                        }
                        else // 아니라면 몬스터가 공격 
                        {
                            battle.AttackMonster(_player, targetMonster);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(78, 26);
                        Console.Write(" 잘못된 입력입니다 다시 입력해주세요 : ");
                    }
                }
                else if (input == "2") // 도망 확률 33퍼 PlayerEscape
                {
                    _player.PlayerEscapePiont();

                    if (_player.IsPlayerEscape == true)
                    {
                        _player.PlayerEscape();
                        break;
                    }
                    else if (_player.IsPlayerEscape == false) // 도망못감
                    {
                        Console.SetCursorPosition(88, 6);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(" 비겁한 놈");
                        Console.SetCursorPosition(80, 8);
                        Console.WriteLine(" 실패의 패널티로 기회를 잃었다! ");
                        Console.ResetColor();
                        Thread.Sleep(1000);
                        break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(78, 26);
                    Console.Write(" 잘못된 입력입니다 다시 입력해주세요 :  ");
                }
            }
        }

        // 플레이어 -> 몬스터 공격 함수
        public void AttackMonster(Character _player, Monster targetMonster)
        {
            int bonusDmg = EquipmentItem.GetEquipDmg();
            int bonusDef = EquipmentItem.GetEquipDef();

            int baseDamage = _player.Dmg + bonusDmg;
            double randomDamage = 1.0 + (new Random().NextDouble() * 0.2 - 0.1);
            int damage = (int)(baseDamage * randomDamage);

            // 크리티컬 ==
            double critical = 1.6;
            double criticalper = 1.5;
            int criticalDamage = ((int)(damage * critical));


            if (criticalper >= 1 + (new Random().NextDouble() * 1))
            {
                _player.CriticalPoint = true;
            }
            // 크리티컬 ==


            if (!_player.IsDead && targetMonster != null && !targetMonster.IsDead)
            {

                if (_player.CriticalPoint == true)
                {
                    targetMonster.Hp -= criticalDamage; // 크리티컬 ture 일때

                    Console.WriteLine("");
                    Console.SetCursorPosition(80, 6);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{_player.Name}의 크리티컬 공격 !");
                    Console.ResetColor();
                    Console.SetCursorPosition(80, 7);
                    Console.Write($" {targetMonster.Name}에게 가한피해 : ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{criticalDamage} ");
                    Console.ResetColor();
                    Console.SetCursorPosition(79, 9);
                    int targetMonsterIsDeadHp = Math.Max(0, targetMonster.Hp);
                    Console.WriteLine($" {targetMonster.Name}의 남은 체력 : {targetMonsterIsDeadHp} ");
                    Thread.Sleep(1000);
                    _player.CriticalPoint = false;
                }
                else if (!_player.CriticalPoint)  // 일반 공격
                {
                    targetMonster.Hp -= damage;
                    Console.WriteLine("");
                    Console.SetCursorPosition(80, 6);
                    Console.WriteLine($"{_player.Name}의 공격 ! ");
                    Console.SetCursorPosition(80, 7);
                    Console.Write($" {targetMonster.Name}에게 가한피해 : ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{damage}");
                    Console.ResetColor();
                    Console.SetCursorPosition(79, 9);
                    int targetMonsterIsDeadHp = Math.Max(0, targetMonster.Hp);
                    Console.WriteLine($" {targetMonster.Name}의 남은 체력 : {targetMonsterIsDeadHp} ");
                    Thread.Sleep(1000);
                }



                // 플레이어 턴 false 
                // 몬스터 턴 true
                _player.IsPlayerTurn = false;
                targetMonster.IsMonsterTurn = true;

            }
        }

        // 플레이어가 몬스터에게 죽었을 때
        public static void PlayerIsDead(Character _player, Monster targetMonster)
        {
            if (_player.IsDead = true)
            {
                Monster.GroundReset();
                Console.SetCursorPosition(75, 10);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {targetMonster.Name}의 공격으로 사망하였습니다 ! ");

                Console.SetCursorPosition(85, 12);
                Console.WriteLine($" {_player.Name}님은 죽었습니다.");
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