using UnityEngine;
using System.Collections.Generic;

[HideInInspector]
public class HumanInfo : MonoBehaviour
{
    //等级
    public int level;
    //战斗等级
    public int fightLevel;
    //驾驶等级
    public int driveLevel;

    //HP
    public int hp;
    public int maxHp;

    //腕力
    public int wrist;
    //体力
    public int vit;
    //速度
    public int dex;
    //男子气概值
    public int macho;


    //当前乘坐的车辆
    public VehicleInfo currentTakedVehicle;

    //总经验值
    public long exp;
    //当前等级经验值
    public long currentExp;


    //career
    //SecondaryCareer

    public EquipmentInfo equipments = new EquipmentInfo();
}

public class EquipmentInfo
{
    List<MMX.HumanWeaponEquipment> weapons = new List<MMX.HumanWeaponEquipment>();
    Dictionary<MMX.HumanArmorEquipmentType, MMX.HumanArmorEquipment> armors = new Dictionary<MMX.HumanArmorEquipmentType, MMX.HumanArmorEquipment>();
}


