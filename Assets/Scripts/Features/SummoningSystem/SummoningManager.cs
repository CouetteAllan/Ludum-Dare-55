using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SummoningCardUI[] _summoningUis;
    [SerializeField] private Canvas _deckCanvas;
    [Header("Summonings")]
    [SerializeField] private Summoning _summoning;
    private SummoningSO _selectedSummoningData;

    private bool _deckIsOpen = false;

    private void Awake()
    {
        TurnBasedManager.OnChangePhase += OnChangePhase;
        SummoningManagerDataHandler.OnDeckClicked += OnDeckClicked;
    }

    private void OnDeckClicked()
    {
        _deckIsOpen = !_deckIsOpen;
        if (_deckIsOpen == true)
            TurnBasedManager.Instance.ChangePhase(CombatPhase.ChosingInDeck);
        else
            TurnBasedManager.Instance.ChangePhase(TurnBasedManager.Instance.PreviousPhase);
    }

    private void OnSendScore(Score finalPatternScore)
    {
        //Summon with the according accuracy
        //Instantiate with the _selectedSumoningData if more than a certain accuracy
        _summoning.gameObject.SetActive(true);
        Debug.Log("Init ALly");
        _summoning.Init(this, _selectedSummoningData);
        TurnBasedManager.Instance.ChangePhase(CombatPhase.AllyAttack,true);
        //2sec delay 

    }

    public void AttackPhase(float delay = 0)
    {
        StartCoroutine(DelayAttack(delay));
    }

    private IEnumerator DelayAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        SummoningManagerDataHandler.AttackPhase(_selectedSummoningData);
    }


    private void SummoningUI_OnClick(SummoningCardUI obj)
    {
        _selectedSummoningData = (SummoningSO)obj.GetCardDatas();
    }

    private void OnChangePhase(CombatPhase newPhase)
    {

        _deckCanvas.gameObject.SetActive(newPhase == CombatPhase.AllyAttack || newPhase == CombatPhase.ChosingInDeck);
        if(newPhase == CombatPhase.PickSummoning)
        {
            _deckCanvas.gameObject.SetActive(false);
            _summoning.ChangeSummonning();
            _summoning.gameObject.SetActive(false);
            OpenSelection(true);
        }
        else if(newPhase == CombatPhase.ChosingInDeck)
        {
            _summoning.ChangeSummonning();
            _summoning.gameObject.SetActive(false);
            OpenSelection(true);
        }
        else
        {
            QTEManagerDataHandler.OnSendScore -= OnSendScore;
            SummoningCardUI.OnClick -= SummoningUI_OnClick;

        }
    }

    private void OpenSelection(bool open)
    {
        if (open)
        {
            SummoningCardUI.OnClick += SummoningUI_OnClick;
            QTEManagerDataHandler.OnSendScore += OnSendScore;

            _canvas.gameObject.SetActive(true);
            foreach (var _summoningUI in _summoningUis)
            {
                _summoningUI.Init();
            }
        }
        else
        {
            SummoningCardUI.OnClick -= SummoningUI_OnClick;
            QTEManagerDataHandler.OnSendScore -= OnSendScore;

            _canvas.gameObject.SetActive(false);
        }
        
    }

    public void ConfirmSelectSummoning()
    {
        SummoningCardUI.OnClick -= SummoningUI_OnClick;
        //Close selection Screen
        _canvas.gameObject.SetActive(false);
        //Send datas
        QTEManagerDataHandler.SendPatternAndStart(_selectedSummoningData.Pattern);
        _deckIsOpen = false;
    }

    private void OnDisable()
    {
        TurnBasedManager.OnChangePhase -= OnChangePhase;

    }
}
