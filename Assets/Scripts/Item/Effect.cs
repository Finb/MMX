namespace MMX
{

    //特效
    public class Effect
    {
        public virtual string name {get;}
    }
    //主动特效
    public class ActiveEffect : Effect { }
    //被动特效
    public class PassiveEffect : Effect
    {
        public virtual void take(IEffect obj) { }
    }


    //被动能力特效，可以提升对象能力
    public class AbilityPassiveEffect : PassiveEffect { }

    //被动防御特效，在被攻击时自动触发
    public class DefensePassiveEffect : PassiveEffect { }

    //被动攻击特效，在攻击时触发
    public class AttackPassiveEffect : PassiveEffect { }

    //人类能力特效
    public class HumanAbilityPassiveEffect : AbilityPassiveEffect
    {
        public virtual float damagePercent { get; }
        public virtual float maxHpPercent { get; }
        public override void take(IEffect obj)
        {
            var target = obj as IHumanAbilityPassiveEffect;
            if (target == null)
            {
                return;
            }
            var property = target.propertyForIncrease();
            property.maxHp += (int)(property.maxHp * damagePercent);
        }
    }



    //战车能力特效
    public class VehicleAbilityPassiveEffect : AbilityPassiveEffect { }
}

namespace MMX
{
    //提升攻击力特效
    public class HumanDamageAbilityEffect : HumanAbilityPassiveEffect
    {
        public override float damagePercent
        {
            get
            {
                return 0;
            }
        }
    }

    //提升 HP 特效
    public class HumanMaxHpAbilityEffect : HumanAbilityPassiveEffect
    {
        public override float maxHpPercent
        {
            get
            {
                return 0;
            }
        }
    }


}

namespace MMX
{
    public interface IEffect
    {
        
    }

    //人类属性提升
    public interface IHumanAbilityPassiveEffect : IEffect
    {
        HumanProperty propertyForIncrease();
    }
}
