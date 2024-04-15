using System;

public static class SummoningManagerDataHandler
{
    public static event Action<SummoningSO> OnAttackPhase;
    public static event Action<SummoningSO> OnAllySummoningSpawn;
    public static event Action<Score,SpellSO,Action> OnAllySummoningAttack;
    public static event Action OnAllySummoningDies;
    public static event Action OnDeckClicked;
    public static void AttackPhase(SummoningSO summoning) => OnAttackPhase?.Invoke(summoning);
    public static void AllySummoningAttack(Score score,SpellSO lastRegisteredSpell,Action callback) => OnAllySummoningAttack?.Invoke(score,lastRegisteredSpell,callback);
    public static void DeckClicked() => OnDeckClicked?.Invoke();
    public static void AllySummoningDies() => OnAllySummoningDies?.Invoke();
    public static void AllySummoningSpawn(SummoningSO datas) => OnAllySummoningSpawn?.Invoke(datas);
}
