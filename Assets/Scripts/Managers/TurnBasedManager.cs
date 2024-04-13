using System;

public enum CombatPhase
{
    Encounter,
    PickSummoning,
    AllyAttack,
    EnemyAttack
}

public class TurnBasedManager : Singleton<TurnBasedManager>
{
    public static event Action<CombatPhase> OnChangePhase;

    public CombatPhase CurrentPhase { get; private set; } = CombatPhase.EnemyAttack;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState obj)
    {
        if(obj == GameState.StartGame)
        {
            ChangePhase(CombatPhase.Encounter);
        }
    }

    public void ChangePhase(CombatPhase newPhase)
    {
        if (newPhase == CurrentPhase)
            return;

        CurrentPhase = newPhase;
        switch (newPhase)
        {
            case CombatPhase.Encounter:
                break;
            case CombatPhase.PickSummoning:
                break;
            case CombatPhase.AllyAttack:
                break;
            case CombatPhase.EnemyAttack:
                break;
        }

        OnChangePhase?.Invoke(newPhase);
    }
}
