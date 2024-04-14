using System;

public static class SummoningManagerDataHandler
{
    public static event Action<SummoningSO> OnAttackPhase;
    public static event Action<Score,SpellSO,Action> OnAllySummoningAttack;
    public static void AttackPhase(SummoningSO summoning) => OnAttackPhase?.Invoke(summoning);
    public static void AllySummoningAttack(Score score,SpellSO lastRegisteredSpell,Action callback) => OnAllySummoningAttack?.Invoke(score,lastRegisteredSpell,callback);
}
