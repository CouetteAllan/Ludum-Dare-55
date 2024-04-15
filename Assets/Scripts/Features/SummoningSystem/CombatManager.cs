using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Affinity
{
    VeryWeak,
    Weak,
    Neutral,
    Good,
    Efficient
}
public class CombatManager : MonoBehaviour
{
    [SerializeField] private GameObject _canva;
    [SerializeField] private SummoningCardUI[] _spellCards;
    private SummoningSO _allySummoningData;
    private SpellSO _currentSpell;

    private bool _enemyStillHere = true;
    private bool _deckIsClosed = true;

    private void Awake()
    {
        SummoningManagerDataHandler.OnAttackPhase += OnAttackPhase;
        TurnBasedManager.OnChangePhase += TurnBasedManager_OnChangePhase;
        DominationManagerDataHandler.OnDominationComplete += OnDominationComplete;
        SummoningManagerDataHandler.OnAllySummoningSpawn += OnAllySummoningSpawn;
    }

    private void OnAllySummoningSpawn(SummoningSO summoningDatas)
    {
        _allySummoningData = summoningDatas;
    }

    private void OnDominationComplete(bool allyVictory)
    {
        _enemyStillHere = !allyVictory;
    }

    private void TurnBasedManager_OnChangePhase(CombatPhase newPhase)
    {
        _deckIsClosed = true;
        switch (newPhase)
        {
            case CombatPhase.PickSummoning:
                _enemyStillHere = true;
                _allySummoningData = null;
                QTEManagerDataHandler.OnSendScore -= OnSendScore;
                SummoningCardUI.OnClick -= SummoningCardUI_OnClick;
                break;
            case CombatPhase.EnemyAttack:
                SummoningCardUI.OnClick -= SummoningCardUI_OnClick;
                QTEManagerDataHandler.OnSendScore -= OnSendScore;
                _enemyStillHere = true;
                break;
            case CombatPhase.AllyAttack when _allySummoningData != null:
                Debug.Log("Ally phase is called");
                OnAttackPhase(_allySummoningData);
                break;
            case CombatPhase.ChosingInDeck:
                QTEManagerDataHandler.OnSendScore -= OnSendScore;
                SummoningCardUI.OnClick -= SummoningCardUI_OnClick;
                _deckIsClosed = false;
                _canva.gameObject.SetActive(false);
                break;
        }
    }

    private void OnSendScore(Score finalScore)
    {
        //DoAttack
        if (finalScore.accuracy > 84.0f)
            SummoningManagerDataHandler.AllySummoningAttack(finalScore, _currentSpell, EndAllyTurn);
        else
        {
            Debug.Log("AttackMissed");
            EndAllyTurn();
        }
        //Do HealthChange
    }

    private void EndAllyTurn()
    {
        if (!_enemyStillHere)
            return;
        TurnBasedManager.Instance.ChangePhase(CombatPhase.EnemyAttack);
    }


    private void SummoningCardUI_OnClick(SummoningCardUI card)
    {
        _currentSpell = (SpellSO)card.GetCardDatas();
    }

    private void OnAttackPhase(SummoningSO summoning)
    {
        //Display spell cards
        if (_deckIsClosed)
        {
            _canva.SetActive(true);
            _enemyStillHere = true;
            int index = 0;
            foreach (var card in _spellCards)
            {
                card.Init(_allySummoningData.Spells[index]);
                index++;
            }
            SummoningCardUI.OnClick += SummoningCardUI_OnClick;
            QTEManagerDataHandler.OnSendScore += OnSendScore;
        }
        else
        {
            _canva.SetActive(false);
            SummoningCardUI.OnClick -= SummoningCardUI_OnClick;
            QTEManagerDataHandler.OnSendScore -= OnSendScore;
        }

    }

    public void ConfirmSelectSummoning()
    {
        //Close selection Screen
        _canva.gameObject.SetActive(false);
        //Send datas
        SummoningCardUI.OnClick -= SummoningCardUI_OnClick;
        QTEManagerDataHandler.SendPatternAndStart(_currentSpell.Pattern);
        _deckIsClosed = true;
    }

    private void OnDisable()
    {
        SummoningManagerDataHandler.OnAttackPhase -= OnAttackPhase;
        TurnBasedManager.OnChangePhase -= TurnBasedManager_OnChangePhase;
        SummoningCardUI.OnClick -= SummoningCardUI_OnClick;
        QTEManagerDataHandler.OnSendScore -= OnSendScore;

        SummoningManagerDataHandler.OnAllySummoningSpawn -= OnAllySummoningSpawn;

    }
}
