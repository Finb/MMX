using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace MMX
{
    public class ItemStorage
    {
        static public ItemStorage shared = new ItemStorage();

        public Dictionary<string, Item> items = new Dictionary<string, Item>();

        private ItemStorage()
        {
            //加载人类道具json
            // var item = JsonUtility.FromJson<MMX.HumanItem>("",List<MMX.HumanItem>);

            Debug.Log(items.Count);
        }
        public void addItems<T>(string json) where T:NormalItem
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

    public class Item
    {
        public string id;
        public int orderNumber;

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
    public class NormalItem : Item
    {

        //剩余数量
        public int count;
    }
    //人类道具
    public class HumanItem : NormalItem
    {

    }

    //回复道具
    public class RecoverItem : NormalItem
    {

    }

    //战斗道具
    public class FightItem : NormalItem
    {

    }

    //战车道具
    public class VehicleItem : Item
    {

    }


    //装备
    public class EquipmentItem : Item
    {

    }

    //人类装备
    public class HumanEquipment : EquipmentItem
    {

    }

    //人类武器装备
    public class HumanWeaponEquipment : HumanEquipment
    {

    }
    //人类防具装备
    public class HumanArmorEquipment : HumanEquipment
    {

    }


    //战车装备
    public class VehicleEquipment : EquipmentItem
    {

    }

    //战车武器
    public class VehicleWeaponEquipment : VehicleEquipment
    {

    }

    //战车C装置
    public class VehicleCEquipment : VehicleEquipment
    {

    }

    // 战车引擎
    public class VehicleEngineEquipment : VehicleEquipment
    {

    }
}
