namespace MMX
{
    //战车装备
    public class VehicleEquipment : VehicleItem
    {
        //最大重量
        public int maxWeight;
        //最小重量
        public int minWeight;
        //总改造次数
        public virtual int modifiedCount { get; }

        //防御力
        public int defense;
        //最大防御力
        public int maxDefense;
        //最小防御力
        public int minDefense;
        //防御力改造次数
        public int defenseModifiedCount;

        //改造一次的价格
        public int modifiedPrice;
    }
}