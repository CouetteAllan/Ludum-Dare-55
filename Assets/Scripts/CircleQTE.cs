using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleQTE : MonoBehaviour, IPointerDownHandler
{ 
    [SerializeField] private TextMeshProUGUI _indexText;
    private QTEManager _manager;


    public void InitCircle(int index, QTEManager manager)
    {
        _indexText.text = index.ToString();
        _manager = manager;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Trigger action 

        //Trigger Feedback

        Destroy(this.gameObject);
    }
}
