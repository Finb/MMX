using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace MMX
{
    class ItemStorage
    {
        static public ItemStorage shared = new ItemStorage();

        public Dictionary<int, Item> items = new Dictionary<int, Item>();

        private ItemStorage()
        {
            //加载人类道具json
            // var item = JsonUtility.FromJson<MMX.HumanItem>("",List<MMX.HumanItem>);

            Debug.Log(items.Count);
        }
        public void addItems(string json)
        {
            var itemArray = JsonConvert.DeserializeObject<List<HumanItem>>(json);
            foreach(var item in itemArray) {
                if (items.ContainsKey(item.id)) {
                    Debug.LogError("重复的Item");
                }
                items.Add(item.id, item);
            }
        }
    }

    class Item
    {
        public int id;

        public string name;

        //卖价
        public int buyPrice;
        //售价
        public int sellPrice;
        //道具介绍
        public string desc;
        //星星数
        public int star;

        public void run()
        {

        }
    }

    //人类道具
    class HumanItem : Item
    {

        //剩余数量
        public int count;
    }

    //回复道具
    class RecoverItem : HumanItem
    {

    }

    //战斗道具
    class FightItem : HumanItem
    {

    }

    //战车道具
    class VehicleItem : Item
    {

    }


    //装备
    class EquipmentItem : Item
    {

    }

    //人类装备
    class HumanEquipment : EquipmentItem
    {

    }

    //人类武器装备
    class HumanWeaponEquipment : HumanEquipment
    {

    }
    //人类防具装备
    class HumanArmorEquipment : HumanEquipment
    {

    }


    //战车装备
    class VehicleEquipment : EquipmentItem
    {

    }

    //战车武器
    class VehicleWeaponEquipment : VehicleEquipment
    {

    }

    //战车C装置
    class VehicleCEquipment : VehicleEquipment
    {

    }

    // 战车引擎
    class VehicleEngineEquipment : VehicleEquipment
    {

    }
}
