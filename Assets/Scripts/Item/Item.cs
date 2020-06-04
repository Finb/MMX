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


    public enum AttackRangeType
    {
        //全体
        all = 0,
        //单体伤害
        single = 1,
        //小贯通
        smallThrough = 2,
        //大贯通
        bigThrough = 3,
        //扇形小
        smallSector = 4,
        //扇形大
        bigSector = 5,
        //扇形可调节
        adjustableSector = 6,
    }
    public static class AttackRangeTypeExtension {
        public static string getName(this AttackRangeType type){
            switch (type){
                case AttackRangeType.all: return "全体";
                case AttackRangeType.single: return "单体";
                case AttackRangeType.smallThrough: return "贯通小";
                case AttackRangeType.bigThrough: return "贯通大";
                case AttackRangeType.smallSector: return "扇形小";
                case AttackRangeType.bigSector: return "扇形大";
                case AttackRangeType.adjustableSector: return "扇形可调节";
            }
            return "";
        }
    }
    public enum AttackProperty
    {
        //通常
        normal = 0,
        //光束
        beam = 1,
        //火炎
        fire = 2,
        //冰
        ice = 3,
        //电
        electric = 4,
        //毒气
        gas = 5,
        //音波
        sonic = 6
    }

    //装备
    public class EquipmentItem : NormalItem
    {

    }

    //人类装备
    public class HumanEquipment : EquipmentItem
    {

    }

    //人类武器装备
    public class HumanWeaponEquipment : HumanEquipment
    {

        //攻击力
        public int damage;
        //攻击范围
        public AttackRangeType attackRangeType;
        ///攻击次数
        public int attacksNum;
        //攻击伤害属性
        public AttackProperty attackProperty;

    }
    //人类防具装备
    public class HumanArmorEquipment : HumanEquipment
    {
        public enum HumanArmorEquipmentType : int
        {
            //头用防具
            head = 0,
            //体用防具
            body,
            //手用防具
            hand,
            //足用防具
            foot,
            //装饰品
            decoration,
        }

        HumanArmorEquipmentType type;
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
