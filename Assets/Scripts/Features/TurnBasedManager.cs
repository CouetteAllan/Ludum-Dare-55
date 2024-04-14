using System;
using TMPro;
using UnityEngine;

public enum CombatPhase
{
    BeforeEncounter,
    Encounter,
    PickSummoning,
    AllyAttack,
    EnemyAttack,
    AfterEncounter
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
        DominationManagerDataHandler.OnDominationComplete += OnDominationComplete;
    }

    private void OnDominationComplete(bool allyVictory)
    {
        if (allyVictory)
            Invoke("ChangeToEncounter", 2.0f);
        else
            GameManager.Instance.ChangeGameState(GameState.GameOver);
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
        _textDebug.text = newPhase.ToString();
        switch (newPhase)
        {
            case CombatPhase.Encounter:
                //Play Encounter;
                Invoke("ChangeToPickPhase", 2.0f);
                break;
            case CombatPhase.PickSummoning:
                break;
            case CombatPhase.AllyAttack:
                //If ally stil alive, keepgoing

                break;
            case CombatPhase.EnemyAttack:
                //If Enemy stil alive, keepgoing

                break;
        }

        OnChangePhase?.Invoke(newPhase);
    }

    private void ChangeToPickPhase()
    {
        ChangePhase(CombatPhase.PickSummoning);
    }

    private void ChangeToAllyPhase()
    {
        ChangePhase(CombatPhase.AllyAttack);
    }

    private void ChangeToEncounter()
    {
        ChangePhase(CombatPhase.Encounter);
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
}
