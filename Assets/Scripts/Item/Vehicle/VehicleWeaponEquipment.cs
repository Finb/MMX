namespace MMX
{
    public enum VehicleWeaponEquipmentType : int
    {
        //主炮
        cannon = 0,
        //副炮
        autogun = 1,
        //SE
        special = 2
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
        public int attack;
        //最大攻击力
        public int maxAttack;
        //最小攻击力
        public int minAttack;
        //攻击力改造次数
        public int attackModifiedCount;

        //弹仓
        public int ammo;
        //最大弹仓
        public int maxAmmo;
        //最小弹仓
        public int minAmmo;
        //弹仓改造次数
        public int ammoModifiedCount;
    }
}