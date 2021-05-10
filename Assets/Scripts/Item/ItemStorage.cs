using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MMX
{
    public class ItemStorage
    {
        static public ItemStorage shared = new ItemStorage();

        public Dictionary<string, Item> items = new Dictionary<string, Item>();

        private ItemStorage()
        {
            //加载道具
            addItems<ToolItem>(Resources.Load<TextAsset>("Items/HumanItem").text);
            //加载恢复道具
            addItems<MedicineItem>(Resources.Load<TextAsset>("Items/RecoverItem").text);
            //加载战斗道具
            addItems<BattleItem>(Resources.Load<TextAsset>("Items/FightItem").text);
            //加载人类武器
            addItems<HumanWeaponEquipment>(Resources.Load<TextAsset>("Items/HumanWeaponEquipment").text);
            //加载人类防具
            addItems<HumanArmorEquipment>(Resources.Load<TextAsset>("Items/HumanArmorEquipment").text);
            Debug.Log(items.Count);
        }
        public void addItems<T>(string json) where T : Item
        {
            var itemArray = JsonConvert.DeserializeObject<List<T>>(json);
            foreach (var item in itemArray)
            {
                if (items.ContainsKey(item.id))
                {
                    Debug.LogError("重复的Item");
                }
                item.orderNumber = items.Count;
                items.Add(item.id, item);
            }
        }
    }
}