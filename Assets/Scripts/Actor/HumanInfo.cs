using UnityEngine;
using System.Collections.Generic;

[HideInInspector]
public partial class HumanInfo : MonoBehaviour
{
    public string nick {
        get {
            return gameObject.GetComponent<RoleInfo>().nick;
        }
    }
    //等级
    public int level;
    //战斗等级
    public int combatLevel;
    //驾驶等级
    public int driverLevel;
    //修理等级
    // public int repairLevel;

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
    public List<MMX.Effect> effects = new List<MMX.Effect>();
    public HumanInfo(){
        equipments.equipmentInfoChanged = (info) => {
            refreshProperty();
        };
    }
    ///根据当前装备，重新计算属性值
    public void refreshProperty()
    {
        property = baseProperty.clone();
        //加上防具的属性
        foreach (var item in equipments.armors.Values)
        {
            property.attack += item.attack;
            property.defend += item.defense;
            property.agility += item.speed;
            property.macho += item.manliness;

            property.resistance[MMX.AttackProperty.ice] += item.iceResistance;
            property.resistance[MMX.AttackProperty.fire] += item.fireResistance;
            property.resistance[MMX.AttackProperty.electric] += item.electricResistance;
            property.resistance[MMX.AttackProperty.sonic] += item.sonicResistance;
            property.resistance[MMX.AttackProperty.gas] += item.gasResistance;
            property.resistance[MMX.AttackProperty.beam] += item.laserResistance;
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
    //武器
    public MMX.HumanWeaponEquipment[] weapons = new MMX.HumanWeaponEquipment[3];
    //防具
    public Dictionary<MMX.HumanArmorEquipmentType, MMX.HumanArmorEquipment> armors = new Dictionary<MMX.HumanArmorEquipmentType, MMX.HumanArmorEquipment>();
    
    //装备改动事件
    public System.Action<EquipmentInfo> equipmentInfoChanged;
    
    public void setWeapon(MMX.HumanWeaponEquipment weaponEquipment, int index){
        weapons[index] = weaponEquipment;
        if(equipmentInfoChanged != null){
            equipmentInfoChanged(this);
        }
    }
    public void setArmor(MMX.HumanArmorEquipment armorEquipment, MMX.HumanArmorEquipmentType type){
        armors[type] = armorEquipment;
        if(equipmentInfoChanged != null){
            equipmentInfoChanged(this);
        }
    }
}

public class HumanProperty : MMX.IEffect
{
    //HP
    public int hp = 0;
    public int maxHp = 0;

    //攻击力
    public int attack = 0;
    //防御力
    public int defend = 0;
    
    //腕力
    public int strength = 0;
    //体力
    public int vitality = 0;

    //男子气概值
    public int macho = 0;
    //速度
    public int agility = 0;
    public HumanProperty(){
        resistance[MMX.AttackProperty.ice] = 0;
        resistance[MMX.AttackProperty.fire] = 0;
        resistance[MMX.AttackProperty.electric] = 0;
        resistance[MMX.AttackProperty.sonic] = 0;
        resistance[MMX.AttackProperty.gas] = 0;
        resistance[MMX.AttackProperty.beam] = 0;
    }
    public Dictionary<MMX.AttackProperty, int> resistance = new Dictionary<MMX.AttackProperty, int>();

    public HumanProperty clone(){
        var prop = new HumanProperty();
        prop.hp = hp;
        prop.maxHp = maxHp;
        prop.attack = attack;
        prop.defend = defend;
        prop.strength = strength;
        prop.vitality = vitality;
        prop.macho = macho;
        prop.agility = agility;
        prop.resistance = new Dictionary<MMX.AttackProperty, int>(resistance);
        return prop;
    }
}