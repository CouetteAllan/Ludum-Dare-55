using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SummoningCardUI[] _summoningUis;
    [Header("Summonings")]
    [SerializeField] private Summoning _summoning;
    [SerializeField] private Summoning _Enemy;
    private SummoningSO _selectedSummoningData;

    private void Awake()
    {
        TurnBasedManager.OnChangePhase += OnChangePhase;
    }

    private void OnSendScore(Score finalPatternScore)
    {
        //Summon with the according accuracy
        //Instantiate with the _selectedSumoningData if more than a certain accuracy
        _summoning.gameObject.SetActive(true);
        _summoning.Init(this, _selectedSummoningData);
        TurnBasedManager.Instance.ChangePhase(CombatPhase.AllyAttack);
        Invoke("AttackPhase", 2.0f); //2sec delay 

    }

    private void AttackPhase() => SummoningManagerDataHandler.AttackPhase(_selectedSummoningData);

    private void SummoningUI_OnClick(SummoningCardUI obj)
    {
        _selectedSummoningData = (SummoningSO)obj.GetCardDatas();
    }

    private void OnChangePhase(CombatPhase newPhase)
    {
        if(newPhase == CombatPhase.PickSummoning)
        {
            OpenSelection();
            _Enemy.transform.position = new Vector2(12, 0);
        }
        else
        {
            QTEManagerDataHandler.OnSendScore -= OnSendScore;
        }
    }

    private void OpenSelection()
    {
        SummoningCardUI.OnClick += SummoningUI_OnClick;
        QTEManagerDataHandler.OnSendScore += OnSendScore;

        _canvas.gameObject.SetActive(true);
        foreach (var _summoningUI in _summoningUis)
        {
            _summoningUI.Init();
        }
    }

    public void ConfirmSelectSummoning()
    {
        SummoningCardUI.OnClick -= SummoningUI_OnClick;
        //Close selection Screen
        _canvas.gameObject.SetActive(false);
        //Send datas
        QTEManagerDataHandler.SendPatternAndStart(_selectedSummoningData.Pattern);
    }

    private void OnDisable()
    {
        TurnBasedManager.OnChangePhase -= OnChangePhase;

    }
}
