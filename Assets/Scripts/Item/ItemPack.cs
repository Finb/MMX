using UnityEngine;
using System.Collections.Generic;
using MMX;
using System.Linq;

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

    public T findItem<T>(string itemId) where T : HumanItem
    {
        IEnumerable<T> allItems = null;
        if (typeof(T) == typeof(HumanItem))
        {
            allItems = new[]
            {
                ItemPack.shared.toolItems.Cast<T>(),
                ItemPack.shared.medicineItems.Cast<T>(),
                ItemPack.shared.battleItems.Cast<T>(),
                ItemPack.shared.equipmentItems.Cast<T>()
            }.SelectMany(item => item);
        }
        else if (typeof(T) == typeof(ToolItem))
        {
            allItems = ItemPack.shared.toolItems.Cast<T>();
        }
        else if (typeof(T) == typeof(MedicineItem))
        {
            allItems = ItemPack.shared.medicineItems.Cast<T>();
        }
        else if (typeof(T) == typeof(BattleItem))
        {
            allItems = ItemPack.shared.battleItems.Cast<T>();
        }
        else if (typeof(T) == typeof(HumanEquipment))
        {
            allItems = ItemPack.shared.equipmentItems.Cast<T>();
        }

        var selectedItem = allItems?.First((item) =>
        {
            return item.id == itemId;
        });

        return selectedItem;
    }

    public HumanItem findItem(string itemId)
    {
        return this.findItem<HumanItem>(itemId);
    }

    public void setItem(string itemId, int count)
    {
        var selectItem = findItem(itemId);
        if (selectItem == null)
        {
            var item = ItemStorage.shared.items[itemId] as HumanItem;
            if (item == null)
            {
                return;
            }
            if (item is ToolItem)
            {
                toolItems.Add(item as ToolItem);
            }
            else if (item is MedicineItem)
            {
                medicineItems.Add(item as MedicineItem);
            }
            else if (item is BattleItem)
            {
                battleItems.Add(item as BattleItem);
            }
            else if (item is HumanWeaponEquipment)
            {
                equipmentItems.Add(item as HumanWeaponEquipment);
            }
            else if (item is HumanArmorEquipment)
            {
                equipmentItems.Add(item as HumanArmorEquipment);
            }
            selectItem = item;
        }
        selectItem.count = count;
    }
}