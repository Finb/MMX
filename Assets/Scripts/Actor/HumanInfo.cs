using UnityEngine;
using System.Collections.Generic;

[HideInInspector]
public partial class HumanInfo : MonoBehaviour
{
    //等级
    public int level;
    //战斗等级
    public int fightLevel;
    //驾驶等级
    public int driveLevel;

    //基础属性
    public HumanProperty baseProperty = new HumanProperty();
    //整体属性，加上装备属性或其他增益
    public HumanProperty property = new HumanProperty();
    //当前乘坐的车辆
    public VehicleInfo currentTakedVehicle;

    //总经验值
    public long exp;
    //当前等级经验值
    public long currentExp;


    //career
    //SecondaryCareer

    public EquipmentInfo equipments = new EquipmentInfo();
    public List<MMX.Effect> effects;
    ///根据当前装备，重新计算属性值
    public void refreshProperty()
    {
        property = baseProperty;
        //加上防具的属性
        foreach (var item in equipments.armors.Values)
        {
            property.damage += item.damage;
            property.defense += item.defense;
            property.velocity += item.velocity;
            property.macho += item.macho;
        }

        //应用特效
        effects.ForEach(item =>
        {
            if (item is MMX.HumanAbilityPassiveEffect)
            {
                (item as MMX.HumanAbilityPassiveEffect).take(this);
            }
        });
    }
}

//支持能力提升特效
public partial class HumanInfo: MMX.IHumanAbilityPassiveEffect {
    public HumanProperty propertyForIncrease() {
        return this.property;
    }

}

public class EquipmentInfo
{
    public MMX.HumanWeaponEquipment[] weapons = new MMX.HumanWeaponEquipment[3];
    public Dictionary<MMX.HumanArmorEquipmentType, MMX.HumanArmorEquipment> armors = new Dictionary<MMX.HumanArmorEquipmentType, MMX.HumanArmorEquipment>();
}

public struct HumanProperty : MMX.IEffect
{
    //HP
    public int hp;
    public int maxHp;

    //攻击力
    public int damage;
    //防御力
    public int defense;
    //速度
    public int velocity;

    //腕力
    public int wrist;
    //体力
    public int con;
    //速度
    //男子气概值
    public int macho;
}