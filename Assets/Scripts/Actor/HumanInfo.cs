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

    //腕力
    public int wrist;
    //体力
    public int vit;
    //速度
    public int dex;
    //男子气概值
    public int manValue;


    //当前乘坐的车辆
    public VehicleInfo currentTakedVehicle;

    //总经验值
    public long exp;
    //当前等级经验值
    public long currentExp;


    //career
    //SecondaryCareer

    public ItemInfo itemInfo = new ItemInfo();
}

public class ItemInfo
{
    List<MMX.HumanItem> humanItems = new List<MMX.HumanItem>();
    List<MMX.RecoverItem> recoverItems = new List<MMX.RecoverItem>();
    List<MMX.FightItem> fightItems = new List<MMX.FightItem>();
}


