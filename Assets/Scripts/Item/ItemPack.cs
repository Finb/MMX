using UnityEngine;
using System.Collections.Generic;
using MMX;
class ItemPack
{
    static public ItemPack shared = new ItemPack();
    //有多少钱
    public int money;
    //人类道具
    public List<HumanItem> humanItems = new List<HumanItem>();
    //回复道具
    public List<RecoverItem> recoverItems = new List<RecoverItem>();
    //战斗道具
    public List<FightItem> fightItems = new List<FightItem>();
    //人类装备
    public List<HumanEquipment> equipmentItems = new List<HumanEquipment>();
    private ItemPack()
    {
        foreach (var item in ItemStorage.shared.items.Values)
        {
            if (item is HumanItem)
            {
                (item as HumanItem).count = System.Convert.ToInt32(Random.Range(1, 99));
                humanItems.Add(item as HumanItem);
            }
            else if (item is RecoverItem)
            {
                (item as RecoverItem).count = System.Convert.ToInt32(Random.Range(1, 99));
                recoverItems.Add(item as RecoverItem);
            }
            else if (item is FightItem)
            {
                (item as FightItem).count = System.Convert.ToInt32(Random.Range(1, 99));
                fightItems.Add(item as FightItem);
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