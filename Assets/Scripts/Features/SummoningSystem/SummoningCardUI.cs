using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SummoningCardUI : MonoBehaviour, IPointerClickHandler
{
    public static event Action<SummoningCardUI> OnClick;

    [SerializeField] private CardSO _cardDatas;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private GameObject _selectionObject;
    [SerializeField] private GameObject _hideCardObject;
    [SerializeField] private PatternUI _patternUI;


    private SummoningSO.BattleResult _battleResult;

    public void Init()
    {
        if(_battleResult?.RemainingHealth <= 0)
        {
            //HideCard
            _hideCardObject.SetActive(true);
            return;
        }
        _image.sprite = _cardDatas.CardImage;
        _desc.text = _cardDatas.CardName;
        _selectionObject.SetActive(false);
        _hideCardObject.SetActive(false);
        _patternUI.SetPattern(_cardDatas.Pattern);
        if (_cardDatas is SummoningSO)
        {
            SummoningSO summoningDatas = _cardDatas as SummoningSO;
            _battleResult = summoningDatas.GetResults();
            _patternUI.SetAmount(_battleResult.RemainingHealth);
            _patternUI.GetRadialBar().gameObject.SetActive(true);
        }
        else
        {
            _patternUI.GetRadialBar().gameObject.SetActive(false);
        }
        SummoningCardUI.OnClick += SummoningUI_OnClick;
    }

    public void Init(SpellSO spellDatas)
    {
        _cardDatas = spellDatas;
        Init();
    }

    private void SummoningUI_OnClick(SummoningCardUI obj)
    {
        if (obj == this)
            return;

        _selectionObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Selected state
        _selectionObject.SetActive(true);
        OnClick?.Invoke(this);
    }

    public CardSO GetCardDatas()
    {
        return _cardDatas;
    }

    public void OnDisable()
    {
        SummoningCardUI.OnClick -= SummoningUI_OnClick;

    }
}
