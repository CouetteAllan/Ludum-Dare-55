using System;
using TMPro;
using UnityEngine;

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

    [SerializeField] private TextMeshProUGUI _textDebug;

    public CombatPhase CurrentPhase { get; private set; } = CombatPhase.EnemyAttack;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState newPhase)
    {
        if(newPhase == GameState.StartGame)
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
                //Play Encounter;
                Invoke("ChangeToPickPhase", 5.0f);
                break;
            case CombatPhase.PickSummoning:
                break;
            case CombatPhase.AllyAttack:
                break;
            case CombatPhase.EnemyAttack:
                break;
        }

        _textDebug.text = newPhase.ToString();
        OnChangePhase?.Invoke(newPhase);
    }

    private void ChangeToPickPhase()
    {
        ChangePhase(CombatPhase.PickSummoning);
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
}
