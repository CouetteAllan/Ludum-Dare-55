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
    [SerializeField] private PatternUI _patternUI;

    // should not be assigned in the inspector but I put it here until we plug the real health data
    [Range(0f,1f)]
    [SerializeField] private float _health = 1f;

    private SummoningSO.BattleResult _battleResult;

    public void Init()
    {
        _image.sprite = _cardDatas.CardImage;
        _desc.text = _cardDatas.CardName;
        _selectionObject.SetActive(false);
        _patternUI.SetPattern(_cardDatas.Pattern);
        _patternUI.SetAmount(_health);
        if (_cardDatas.Equals(typeof(SummoningSO)))
        {
            SummoningSO summoningDatas = _cardDatas as SummoningSO;
            _battleResult = summoningDatas.GetResults();
            _patternUI.SetAmount(_battleResult.RemainingHealth);
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
