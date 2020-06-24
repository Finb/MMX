namespace MMX
{
    public interface IFight
    {
        // 承受目标攻击
        FightResult attackedBy(Attacker attacker);

        //计算战斗效果
        void calculateFightResult(FightResult result);
    }
}