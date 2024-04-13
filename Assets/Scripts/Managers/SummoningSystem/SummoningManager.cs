using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SummoningUI[] _summoningUIs;
    private SummoningSO _selectedSummoningData;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        SummoningUI.OnClick += SummoningUI_OnClick;
    }

    private void SummoningUI_OnClick(SummoningUI obj)
    {
        _selectedSummoningData = obj.GetSummoningDatas();
    }

    private void OnGameStateChanged(GameState newState)
    {
        if(newState == GameState.StartGame)
            OpenSelection();
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
}
