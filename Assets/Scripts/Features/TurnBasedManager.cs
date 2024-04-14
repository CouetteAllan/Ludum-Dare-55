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
    [SerializeField] private Canvas _encounterCanvas;
    [SerializeField] private TextMeshProUGUI _longEncounterText;

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
            Invoke("ChangeToAfterEncounter", 2.0f);
        else
            GameManager.Instance.ChangeGameState(GameState.GameOver);
    }

    private void GameManager_OnGameStateChanged(GameState newPhase)
    {
        if(newPhase == GameState.StartGame)
        {
            ChangePhase(CombatPhase.BeforeEncounter);
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
            case CombatPhase.BeforeEncounter:
                _encounterCanvas.gameObject.SetActive(true);
                break;
            case CombatPhase.Encounter:
                //Play Encounter;
                _encounterCanvas.gameObject.SetActive(false);
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
            case CombatPhase.AfterEncounter:
                _encounterCanvas.gameObject.SetActive(true);
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

    public void ChangeToEncounter()
    {
        if(CurrentPhase == CombatPhase.AfterEncounter)
            ChangePhase(CombatPhase.BeforeEncounter);
        else
            ChangePhase(CombatPhase.Encounter);
    }

    private void ChangeToAfterEncounter()
    {
        ChangePhase(CombatPhase.AfterEncounter);
    }


    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
}
