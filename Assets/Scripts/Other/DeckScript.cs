using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckScript : MonoBehaviour, IPointerClickHandler
{
    public bool _backButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        SummoningManagerDataHandler.DeckClicked(!_backButton);
        //Play feedback
    }
}
