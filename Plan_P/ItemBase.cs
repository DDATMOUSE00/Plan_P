using System.Numerics;

namespace Plan_P
{
    public class EquipmentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemType { get; set; }
        /// <summary>
        /// 0 : 무기, 1 : 방어구
        /// </summary>
        public bool IsEquiped { get; set; }
        public int Dmg { get; set; }
        public int Def { get; set; }
        public int MaxHp { get; set; }
        public static int ItemCnt = 0;


        public EquipmentItem(int id, string name, string description, int type, int dmg, int def, int maxHp, bool isEquiped = false)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemType = type;
            Dmg = dmg;
            Def = def;
            MaxHp = maxHp;
            IsEquiped = isEquiped;
        }
        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write(" 　　　- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (IsEquiped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
            }
            Console.Write(MainScene.PadRightForMixedtext(Name, 18));
            Console.Write("     ");

            //(Atk >= 0 ? "+" : "") [조건 ? 조건이 참이면 : 조건이 거짓이면] 삼항연산자
            if (Dmg != 0) Console.Write($" 공격력 {(Dmg >= 0 ? "+" : "")}{Dmg}   ");
            if (Def != 0) Console.Write($" 방어력 {(Def >= 0 ? "+" : "")}{Def}   ");
            if (MaxHp != 0) Console.Write($" 체력 {(MaxHp >= 0 ? "+" : "")}{MaxHp}    ");

            Console.Write("     ");

            Console.WriteLine(Description);
        }
        public static int GetEquipDmg()
        {
            int sum = 0;
            for (int i = 0; i < EquipmentItem.ItemCnt; i++)
            {
                if (Inventory.haveEquipmentItems[i].IsEquiped)
                {
                    sum += Inventory.haveEquipmentItems[i].Dmg;
                }
            }
            return sum;
        }
        public static int GetEquipDef()
        {
            int sum = 0;
            for (int i = 0; i < EquipmentItem.ItemCnt; i++)
            {
                if (Inventory.haveEquipmentItems[i].IsEquiped)
                {
                    sum += Inventory.haveEquipmentItems[i].Def;
                }
            }
            return sum;
        }
        public static int GetEquipMaxHp()
        {
            int sum = 0;
            for (int i = 0; i < EquipmentItem.ItemCnt; i++)
            {
                if (Inventory.haveEquipmentItems[i].IsEquiped)
                {
                    sum += Inventory.haveEquipmentItems[i].MaxHp;
                }
            }
            return sum;
        }

    }

    public class ConsumptionItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecoveredHp { get; set; }
        public int Cnt { get; set; } //아이템의 갯수
        public static int ItemCnt = 0; //아이템 종류의 갯수
        public ConsumptionItem(int id, string name, string description, int recoveredHp, int cnt)
        {
            Id = id;
            Name = name;
            Description = description;
            RecoveredHp = recoveredHp;
            Cnt = cnt;
        }

        public void PrintItemDescription(bool withNumber = false, int idx = 0)
        {

            Console.Write(" - ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }

            Console.Write(Name);
            //(Atk >= 0 ? "+" : "") [조건 ? 조건이 참이면 : 조건이 거짓이면] 삼항연산자
            if (RecoveredHp != 0) Console.Write($" | 회복력 {(RecoveredHp >= 0 ? "+" : "")}{RecoveredHp}");
            if (Cnt != 0) Console.Write($" | {(Cnt >= 0 ? "*" : "")}{Cnt}");
            Console.Write(" | ");
            Console.WriteLine(Description);
        }

    }

    public class Inventory
    {
        public static EquipmentItem[] haveEquipmentItems;
        public static ConsumptionItem[] haveConsumptionItems;
        public static int EquipItemCnt;
        public static int ConsumItemCnt;
        public static void InventoryScene()
        {
            Console.Clear();
            Console.SetCursorPosition(37, 2);
            Console.Write("╔⊶⊶⊶⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊶⊷⊶✞⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷╗");
            Console.SetCursorPosition(47, 4);
            Console.Write("      ■ 인벤토리 ■     ");
            Console.SetCursorPosition(37, 6);
            Console.Write("╚⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶✞⊷⊷⊷⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶╝");
            Console.SetCursorPosition(40, 8);
            Console.Write("[            아이템 목록            ]");
            Console.SetCursorPosition(40, 9);
            Console.WriteLine();


            for (EquipItemCnt = 0; EquipItemCnt < EquipmentItem.ItemCnt; EquipItemCnt++)
            {
                haveEquipmentItems[EquipItemCnt].PrintItemStatDescription();
            }
            for (ConsumItemCnt = 0; ConsumItemCnt < ConsumptionItem.ItemCnt; ConsumItemCnt++)
            {
                haveConsumptionItems[ConsumItemCnt].PrintItemDescription();
            }

            Console.WriteLine();
            Console.SetCursorPosition(40, 16);
            Console.Write("[            1. 장착관리            ]");

            Console.SetCursorPosition(40, 18);
            Console.Write("[            2. 소모품사용          ]");
            Console.SetCursorPosition(40, 20);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[            0. 나가기              ]");
            Console.ResetColor();

            Console.SetCursorPosition(1, 23);
            for (int i = 2; i < 120; i++) // 중간테두리
            {

                Console.Write('─');
            }
            Console.SetCursorPosition(45, 25);
            Console.Write("원하시는 행동을 입력해주세요.");
            Console.SetCursorPosition(57, 27);

            int input = MainScene.CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    MainScene.DisplayGameIntro();
                    break;
                case 1:
                    EquipmentManage.EquipmentManagent();
                    break;
                case 2:
                    ConsumableManage.ConsumableManagent();
                    break;
            }
            Console.ReadLine();
        }
        public static void ToggleEquipStatus(int idx)
        {
            haveEquipmentItems[idx].IsEquiped = !haveEquipmentItems[idx].IsEquiped;
        }
        public void StartItemSetting()
        {
            haveEquipmentItems = new EquipmentItem[20];
            haveConsumptionItems = new ConsumptionItem[20];
            if (MainScene._player.Job == "퇴마사")
            {
                AddEquipmentItem(0);
                AddEquipmentItem(1);
                AddEquipmentItem(6);
            }
            else if (MainScene._player.Job == "경찰")
            {
                AddEquipmentItem(2);
                AddEquipmentItem(3);
                AddEquipmentItem(6);
            }
            else if (MainScene._player.Job == "학생")
            {
                AddEquipmentItem(4);
                AddEquipmentItem(5);
                AddEquipmentItem(6);
            }

        }
        public static void AddEquipmentItem(int itemid)
        {
            if (EquipmentItem.ItemCnt >= 100)
            {
                Console.WriteLine("장비가 너무 많습니다!");
                return;
            }
            haveEquipmentItems[EquipmentItem.ItemCnt] = ItemList.EquipmentItemList[itemid];
            EquipmentItem.ItemCnt++;
        }
        public static void AddConsumptionItem(int itemid)
        {
            if (ConsumptionItem.ItemCnt >= 100)
            {
                Console.WriteLine("물건이 너무 많습니다!");
                return;
            }
            haveConsumptionItems[ConsumptionItem.ItemCnt] = ItemList.ConsumptionItemList[itemid];
            ConsumptionItem.ItemCnt++;
        }
    }

    public class EquipmentManage
    {
        public static void EquipmentManagent()
        {
            Console.Clear();
            Console.SetCursorPosition(37, 2);
            Console.Write("╔⊶⊶⊶⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊶⊷⊶✞⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷╗");
            Console.SetCursorPosition(48, 4);
            Console.Write("    ◆ 장비 관리 ◆     ");
            Console.SetCursorPosition(37, 6);
            Console.Write("╚⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶✞⊷⊷⊷⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶╝");
            Console.SetCursorPosition(35, 25);
            Console.WriteLine("장착하거나 해제할 아이템의 숫자를 입력하세요");
            Console.SetCursorPosition(40, 8);
            Console.Write("[            아이템 목록            ]");
            Console.SetCursorPosition(40, 9);
            Console.WriteLine("");
            for (Inventory.EquipItemCnt = 0; Inventory.EquipItemCnt < EquipmentItem.ItemCnt; Inventory.EquipItemCnt++)
            {
                Inventory.haveEquipmentItems[Inventory.EquipItemCnt].PrintItemStatDescription(true, Inventory.EquipItemCnt + 1);
            }
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
            Console.SetCursorPosition(57, 27);

            int input = MainScene.CheckValidInput(0, EquipmentItem.ItemCnt);
            switch (input)
            {
                case 0:
                    Inventory.InventoryScene();
                    break;
                default:
                    Inventory.ToggleEquipStatus(input - 1); //유저의 입력은 1,2,3... / 실제 배열은 0,1,2...
                    EquipmentManagent();
                    break;
            }
            Console.ReadLine();
        }
    }
    public class ConsumableManage
    {
        public static void ConsumableManagent()
        {
            Console.Clear();
            Console.SetCursorPosition(37, 2);
            Console.Write("╔⊶⊶⊶⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊶⊷⊶✞⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷⊷╗");
            Console.SetCursorPosition(48, 4);
            Console.Write("    ◆ 소모품 관리 ◆     ");
            Console.SetCursorPosition(37, 6);
            Console.Write("╚⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶✞⊷⊷⊷⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶⊶╝");
            Console.SetCursorPosition(43, 25);
            Console.WriteLine("사용할 아이템의 숫자를 입력하세요");
            Console.SetCursorPosition(40, 8);
            Console.Write("[            아이템 목록            ]");
            Console.SetCursorPosition(40, 9);
            Console.WriteLine("");

            for (Inventory.ConsumItemCnt = 0; Inventory.ConsumItemCnt < ConsumptionItem.ItemCnt; Inventory.ConsumItemCnt++)
            {
                Inventory.haveConsumptionItems[Inventory.ConsumItemCnt].PrintItemDescription(true, Inventory.ConsumItemCnt + 1);
            }
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
            Console.SetCursorPosition(57, 27);
            int input = MainScene.CheckValidInput(0, EquipmentItem.ItemCnt);
            switch (input)
            {
                case 0:
                    Inventory.InventoryScene();
                    break;
                default:
                    Inventory.ToggleEquipStatus(input - 1); //유저의 입력은 1,2,3... / 실제 배열은 0,1,2...
                    ConsumableManagent();
                    break;
            }
            Console.ReadLine();
        }
    }

    public class ItemList
    {
        public static EquipmentItem[] EquipmentItemList;
        public static ConsumptionItem[] ConsumptionItemList;

        public static void ItmeListUpdate()
        {
            EquipmentItemList = new EquipmentItem[24];
            //Id, 이름, 설명, 장비타입(무기: 0, 방어구: 1), 공격력, 방어력, 최대체력
            EquipmentItemList[0] = new EquipmentItem(0, "십자가", "   퇴마사의 무기 ", 0, 5, 0, 0);
            EquipmentItemList[1] = new EquipmentItem(1, "빛나는 성경책", "   퇴마사가 사용하는 법서", 1, 0, 8, 0);
            EquipmentItemList[2] = new EquipmentItem(2, "낡은 권총", "   경력 5년된 경찰의 무기", 0, 7, 0, 0);
            EquipmentItemList[3] = new EquipmentItem(3, "구김이 많은 경찰복", "   경찰의 바쁜업무가 보인다 ", 1, 0, 3, 0);
            EquipmentItemList[4] = new EquipmentItem(4, "대나무 단소 ", "   수행평가때 사용할 단소 ", 0, 6, 0, 0);
            EquipmentItemList[5] = new EquipmentItem(5, "나이키 가방", "   유행하는 메신저백이다  ", 1, 0, 4, 0);
            EquipmentItemList[6] = new EquipmentItem(6, "현영 펀치", "우리 애 괴롭히면 각오해 ", 1, 1000, 0, 0);
            EquipmentItemList[7] = new EquipmentItem(6, "이름", "설명", 0, 0, 0, 0);

            ConsumptionItemList = new ConsumptionItem[24];
            //id, 이름, 설명, 회복량, 개수
            ConsumptionItemList[0] = new ConsumptionItem(0, "생명의 물약", "체력을 회복시켜주는 물약", 10, 1);
            ConsumptionItemList[1] = new ConsumptionItem(1, "활력의 정수", "활력을 불어 넣어주는 물약", 13, 1);
            ConsumptionItemList[2] = new ConsumptionItem(2, "치유의 성스러운 불꽃", "다친 몸을 치유시켜주는 성스러운 불꽃", 16, 1);
            ConsumptionItemList[3] = new ConsumptionItem(3, "빛나는 미네랄", "치유의 빛을 뿜어내는 미네랄", 20, 1);
            ConsumptionItemList[4] = new ConsumptionItem(4, "황금 약초 차", "황금빛 약초를 우려낸 차", 25, 1);
            ConsumptionItemList[5] = new ConsumptionItem(5, "피로 회복의 크리스탈", "피로를 회복시켜주는 크리스탈", 30, 1);
            ConsumptionItemList[6] = new ConsumptionItem(6, "신속한 치료의 영약", "신속하게 체력을 회복시켜주는 영적인 물약", 35, 1);
        }

    }

    public class drinkpotion
    {
        public void UseConsumptionItem(Character character, ConsumptionItem consumptionItem)
        {
            character.Hp += consumptionItem.RecoveredHp;
        }
    }


}