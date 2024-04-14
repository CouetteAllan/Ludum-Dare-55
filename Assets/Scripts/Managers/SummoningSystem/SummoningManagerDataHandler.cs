using System;

public static class SummoningManagerDataHandler
{
    public static event Action<SummoningSO> OnAttackPhase;
    public static void AttackPhase(SummoningSO summoning) => OnAttackPhase?.Invoke(summoning);
}
