using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SummoningUI : MonoBehaviour, IPointerClickHandler
{
    public static event Action<SummoningUI> OnClick;

    [SerializeField] private SummoningSO _summoningData;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private GameObject _selectionObject;

    public void Init()
    {
        _image.sprite = _summoningData.SummoningImage;
        _desc.text = _summoningData.SummoningName;
        _selectionObject.SetActive(false);
        SummoningUI.OnClick += SummoningUI_OnClick;
    }

    private void SummoningUI_OnClick(SummoningUI obj)
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

    public SummoningSO GetSummoningDatas()
    {
        return _summoningData;
    }

    public void OnDisable()
    {
        
    }
}
