using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleQTE : MonoBehaviour, IPointerDownHandler
{ 
    [SerializeField] private TextMeshProUGUI _indexText;
    [SerializeField] private ParticleSystem[] _particleSystemsFeedback;
    private int _index;
    public int Index => _index;
    private QTEManager _manager;


    public void InitCircle(int index, QTEManager manager)
    {
        _index = index;
        _indexText.text = (index + 1).ToString();
        _manager = manager;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var cam = Camera.main;

        //Trigger action 
        if (_manager.IsNextIndex(_index))
        {
            var particle = Instantiate(_particleSystemsFeedback[0], cam.ScreenToWorldPoint(this.transform.position + Vector3.forward * 10.0f), Quaternion.identity) ;
            particle.Play();

        }
        else
        {
            var particle = Instantiate(_particleSystemsFeedback[1], cam.ScreenToWorldPoint(this.transform.position + Vector3.forward * 10.0f), Quaternion.identity);
            particle.Play();
        }

        if (_index == 0)
            _manager.StartDrawing(cam.ScreenToWorldPoint(this.transform.position));
        else
            _manager.AddPointToLine(cam.ScreenToWorldPoint(this.transform.position));

        _manager.SetPreviousIndex(_index);
        //Trigger Feedback

        Destroy(this.gameObject);
    }
}
