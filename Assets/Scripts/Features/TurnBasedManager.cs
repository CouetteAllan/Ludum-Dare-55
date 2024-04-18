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
    AfterEncounter,
    ChosingInDeck,
}

public class TurnBasedManager : Singleton<TurnBasedManager>
{
    public static event Action<CombatPhase> OnChangePhase;

    [SerializeField] private TextMeshProUGUI _textDebug;
    [SerializeField] private Canvas _encounterCanvas;
    [SerializeField] private TextMeshProUGUI _longEncounterText;

    public CombatPhase CurrentPhase { get; private set; } = CombatPhase.EnemyAttack;
    public CombatPhase PreviousPhase { get; private set; } = CombatPhase.EnemyAttack;

    private bool _allyIsAlive = true;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        DominationManagerDataHandler.OnDominationComplete += OnDominationComplete;
        SummoningManagerDataHandler.OnAllySummoningDies += OnAllySummoningDies;
        SummoningManagerDataHandler.OnAllySummoningSpawn += OnAllySummoningSpawn;
    }

    private void OnAllySummoningSpawn(SummoningSO datas)
    {
        _allyIsAlive = true;
    }

    private void OnAllySummoningDies()
    {
        _allyIsAlive = false;
    }

    private void OnDominationComplete(bool allyVictory)
    {
        if(!allyVictory)
            GameManager.Instance.ChangeGameState(GameState.GameOver);
    }

    private void GameManager_OnGameStateChanged(GameState newPhase)
    {
        if(newPhase == GameState.StartGame)
        {
            ChangePhase(CombatPhase.BeforeEncounter);
        }
    }

    public void ChangePhase(CombatPhase newPhase,bool forceChange = false)
    {
        if (newPhase == CurrentPhase && !forceChange)
            return;

        var temp = PreviousPhase;
        PreviousPhase = CurrentPhase;
        CurrentPhase = newPhase;
        _textDebug.text = newPhase.ToString();
        switch (newPhase)
        {
            case CombatPhase.BeforeEncounter:
                _encounterCanvas.gameObject.SetActive(true);
                _allyIsAlive = true;
                EnemyManagerDataHandler.RegisterEncounterText(_longEncounterText);
                break;
            case CombatPhase.Encounter:
                //Play Encounter;
                _encounterCanvas.gameObject.SetActive(false);
                //Invoke("ChangeToPickPhase", 3.0f);
                break;
            case CombatPhase.PickSummoning:
                break;
            case CombatPhase.AllyAttack:
                //If ally stil alive, keepgoing
                if (!_allyIsAlive)
                {
                    CurrentPhase = PreviousPhase;
                    _textDebug.text = PreviousPhase.ToString();
                    PreviousPhase = temp;
                    return;
                }
                break;
            case CombatPhase.EnemyAttack:
                //If Enemy stil alive, keepgoing
                break;
            case CombatPhase.AfterEncounter:
                _encounterCanvas.gameObject.SetActive(true);
                EnemyManagerDataHandler.RegisterEncounterText(_longEncounterText);
                break;

            case CombatPhase.ChosingInDeck:
                if (PreviousPhase != CombatPhase.AllyAttack)
                {
                    CurrentPhase = PreviousPhase;
                    _textDebug.text = PreviousPhase.ToString();
                    PreviousPhase = temp;
                    return;
                }
                break;
        }

        OnChangePhase?.Invoke(newPhase);
    }

    private void ChangeToPickPhase()
    {
        SummoningManagerDataHandler.Encounter();
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
        DominationManagerDataHandler.OnDominationComplete -= OnDominationComplete;
        SummoningManagerDataHandler.OnAllySummoningDies -= OnAllySummoningDies;
        SummoningManagerDataHandler.OnAllySummoningSpawn -= OnAllySummoningSpawn;

    }
}
