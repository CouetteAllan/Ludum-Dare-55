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

    public void Init()
    {
        _image.sprite = _cardDatas.CardImage;
        _desc.text = _cardDatas.CardName;
        _selectionObject.SetActive(false);
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
