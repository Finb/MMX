using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace MMX
{
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

        public void use()
        {

        }
    }
}

namespace MMX
{
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
    public enum AttackRangeType
    {
        //全体
        all = 0,
        //单体伤害
        oneEnemy = 1,
        //小贯通
        smallLine = 2,
        //大贯通
        LargeLine = 3,
        //扇形小
        smallSector = 4,
        //扇形大
        LargeFan = 5,
        //扇形可调节
        adjustableFan = 6,
    }
    public enum HumanArmorEquipmentType : int
    {
        //头用防具
        head = 0,
        //体用防具
        body,
        //手用防具
        arms,
        //足用防具
        legs,
        //装饰品
        accessory,
    }

    public static class TypeExtension
    {
        public static string getName(this AttackRangeType type)
        {
            switch (type)
            {
                case AttackRangeType.all: return "全体";
                case AttackRangeType.oneEnemy: return "单体";
                case AttackRangeType.smallLine: return "贯通小";
                case AttackRangeType.LargeLine: return "贯通大";
                case AttackRangeType.smallSector: return "扇形小";
                case AttackRangeType.LargeFan: return "扇形大";
                case AttackRangeType.adjustableFan: return "扇形可调节";
            }
            return "";
        }
        public static string getName(this HumanArmorEquipmentType type)
        {
            switch (type)
            {
                case HumanArmorEquipmentType.head: return "头用防具";
                case HumanArmorEquipmentType.body: return "体用防具";
                case HumanArmorEquipmentType.arms: return "手用防具";
                case HumanArmorEquipmentType.legs: return "足用防具";
                case HumanArmorEquipmentType.accessory: return "装饰品";
            }
            return "";
        }
        public static string getName(this AttackProperty type)
        {
            switch (type)
            {
                case AttackProperty.normal: return "通常";
                case AttackProperty.beam: return "光束";
                case AttackProperty.fire: return "火炎";
                case AttackProperty.ice: return "冰冻";
                case AttackProperty.electric: return "电气";
                case AttackProperty.gas: return "瓦斯";
                case AttackProperty.sonic: return "音波";
            }
            return "";
        }
    }
}