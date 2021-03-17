using UnityEngine;
using System.Collections.Generic;
using MMX;
class ItemPack
{
    static public ItemPack shared = new ItemPack();
    //有多少钱
    public int money;
    //人类道具
    public List<ToolItem> toolItems = new List<ToolItem>();
    //回复道具
    public List<MedicineItem> medicineItems = new List<MedicineItem>();
    //战斗道具
    public List<BattleItem> battleItems = new List<BattleItem>();
    //人类装备
    public List<HumanEquipment> equipmentItems = new List<HumanEquipment>();
    private ItemPack()
    {
        foreach (var item in ItemStorage.shared.items.Values)
        {
            if (item is ToolItem)
            {
                (item as ToolItem).count = System.Convert.ToInt32(Random.Range(1, 99));
                toolItems.Add(item as ToolItem);
            }
            else if (item is MedicineItem)
            {
                (item as MedicineItem).count = System.Convert.ToInt32(Random.Range(1, 99));
                medicineItems.Add(item as MedicineItem);
            }
            else if (item is BattleItem)
            {
                (item as BattleItem).count = System.Convert.ToInt32(Random.Range(1, 99));
                battleItems.Add(item as BattleItem);
            }
            else if (item is HumanWeaponEquipment)
            {
                (item as HumanWeaponEquipment).count = System.Convert.ToInt32(Random.Range(1, 99));
                equipmentItems.Add(item as HumanWeaponEquipment);
            }
            else if (item is HumanArmorEquipment)
            {
                (item as HumanArmorEquipment).count = System.Convert.ToInt32(Random.Range(1, 99));
                equipmentItems.Add(item as HumanArmorEquipment);
            }
        }
    }
}