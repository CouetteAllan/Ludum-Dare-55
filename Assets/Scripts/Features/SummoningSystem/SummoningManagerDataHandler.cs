using System;

public static class SummoningManagerDataHandler
{
    public static event Action OnAttackPhase;
    public static event Action<SummoningSO> OnAllySummoningSpawn;
    public static event Action<Score,SpellSO,Action> OnAllySummoningAttack;
    public static event Action<SummoningSO,Score,SpellSO> OnAllySummoningDealDamage;
    public static event Action OnAllySummoningDies;
    public static event Action<bool> OnDeckClicked;
    public static event Action OnEncounter;
    public static void DisplaySummoningSpells() => OnAttackPhase?.Invoke();
    public static void AllySummoningAttack(Score score,SpellSO lastRegisteredSpell,Action callback) => OnAllySummoningAttack?.Invoke(score,lastRegisteredSpell,callback);
    public static void DeckClicked(bool _isDeck) => OnDeckClicked?.Invoke(_isDeck);
    public static void AllySummoningDies() => OnAllySummoningDies?.Invoke();
    public static void AllySummoningSpawn(SummoningSO datas) => OnAllySummoningSpawn?.Invoke(datas);
    public static void AllySummoningDealDamage(SummoningSO datas,Score score, SpellSO spellDatas ) => OnAllySummoningDealDamage?.Invoke(datas,score,spellDatas);
    public static void Encounter() => OnEncounter?.Invoke();
}
