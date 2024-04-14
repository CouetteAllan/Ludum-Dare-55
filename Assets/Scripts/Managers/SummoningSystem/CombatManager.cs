using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Affinity
{
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

    private void Awake()
    {
        SummoningManagerDataHandler.OnAttackPhase += OnAttackPhase;
        TurnBasedManager.OnChangePhase += TurnBasedManager_OnChangePhase;
    }

    private void TurnBasedManager_OnChangePhase(CombatPhase newPhase)
    {
        if (newPhase == CombatPhase.PickSummoning)
            QTEManagerDataHandler.OnSendScore -= OnSendScore;
        else if (newPhase == CombatPhase.AllyAttack && _allySummoningData != null)
            OnAttackPhase(_allySummoningData);
    }

    private void OnSendScore(Score finalScore)
    {
        //DoAttack
        TurnBasedManager.Instance.ChangePhase(CombatPhase.EnemyAttack);
    }

    private void SummoningCardUI_OnClick(SummoningCardUI card)
    {

        _currentSpell = (SpellSO)card.GetCardDatas();
    }

    private void OnAttackPhase(SummoningSO summoning)
    {
        //Display spell cards
        _allySummoningData = summoning;
        _canva.SetActive(true);
        int index = 0;
        foreach (var card in _spellCards)
        {
            card.Init(_allySummoningData.Spells[index]);
            index++;
        }
        SummoningCardUI.OnClick += SummoningCardUI_OnClick;
        QTEManagerDataHandler.OnSendScore += OnSendScore;

    }

    public void ConfirmSelectSummoning()
    {
        //Close selection Screen
        _canva.gameObject.SetActive(false);
        //Send datas
        SummoningCardUI.OnClick -= SummoningCardUI_OnClick;
        QTEManagerDataHandler.SendPatternAndStart(_currentSpell.Pattern);
    }

    private void OnDisable()
    {
        SummoningManagerDataHandler.OnAttackPhase -= OnAttackPhase;
        TurnBasedManager.OnChangePhase -= TurnBasedManager_OnChangePhase;


    }
}
