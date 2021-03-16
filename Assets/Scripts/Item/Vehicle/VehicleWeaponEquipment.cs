namespace MMX
{
    public enum VehicleWeaponEquipmentType : int
    {
        //主炮
        artillery = 0,
        //副炮
        machineGun = 1,
        //SE
        se = 2
    }
    //战车武器
    public class VehicleWeaponEquipment : VehicleEquipment
    {

        public VehicleWeaponEquipmentType equipmentType;
        //攻击范围
        public AttackRangeType attackRangeType;
        //攻击属性
        public AttackProperty attackProperty;
        //弹药价格
        public int ammoPrice;

        //攻击力
        public int damage;
        //最大攻击力
        public int maxDamage;
        //最小攻击力
        public int minDamage;
        //攻击力改造次数
        public int damageModifiedCount;

        //弹仓
        public int magazine;
        //最大弹仓
        public int maxMagazine;
        //最小弹仓
        public int minMagazine;
        //弹仓改造次数
        public int magazineModifiedCount;
    }
}