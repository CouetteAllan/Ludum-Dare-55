using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SummoningUI[] _summoningUIs;
    [Header("Summonings")]
    [SerializeField] private Summoning _summoning;
    [SerializeField] private Summoning _Enemy;
    private SummoningSO _selectedSummoningData;

    private void Awake()
    {
        TurnBasedManager.OnChangePhase += OnChangePhase;
        SummoningUI.OnClick += SummoningUI_OnClick;
        QTEManagerDataHandler.OnSendScore += OnSendScore;
    }

    private void OnSendScore(Score finalPatternScore)
    {
        //Summon with the according accuracy
        //Instantiate with the _selectedSumoningData if more than a certain accuracy
        _summoning.gameObject.SetActive(true);
        _summoning.Init(this, _selectedSummoningData);
        TurnBasedManager.Instance.ChangePhase(CombatPhase.AllyAttack);

    }

    private void SummoningUI_OnClick(SummoningUI obj)
    {
        _selectedSummoningData = obj.GetSummoningDatas();
    }

    private void OnChangePhase(CombatPhase newPhase)
    {
        if(newPhase == CombatPhase.PickSummoning)
        {
            OpenSelection();
            _Enemy.transform.position = new Vector2(12, 0);
        }
    }

    private void OpenSelection()
    {
        _canvas.gameObject.SetActive(true);
        foreach (var _summoningUI in _summoningUIs)
        {
            _summoningUI.Init();
        }
    }

    public void ConfirmSelectSummoning()
    {
        //Close selection Screen
        _canvas.gameObject.SetActive(false);
        //Send datas
        QTEManagerDataHandler.SendPatternAndStart(_selectedSummoningData.Pattern);
    }

    private void OnDisable()
    {
        TurnBasedManager.OnChangePhase -= OnChangePhase;
        SummoningUI.OnClick -= SummoningUI_OnClick;
        QTEManagerDataHandler.OnSendScore -= OnSendScore;

    }
}