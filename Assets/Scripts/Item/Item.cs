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

        public void run()
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

    public static class TypeExtension
    {
        public static string getName(this AttackRangeType type)
        {
            switch (type)
            {
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
        public static string getName(this HumanArmorEquipmentType type)
        {
            switch (type)
            {
                case HumanArmorEquipmentType.head: return "头用防具";
                case HumanArmorEquipmentType.body: return "体用防具";
                case HumanArmorEquipmentType.hand: return "手用防具";
                case HumanArmorEquipmentType.foot: return "足用防具";
                case HumanArmorEquipmentType.decoration: return "装饰品";
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