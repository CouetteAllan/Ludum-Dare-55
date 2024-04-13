using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PrecisionState
{
    Perfect,
    Good,
    Missed
}
public class CircleQTE : MonoBehaviour, IPointerDownHandler
{ 
    [SerializeField] private TextMeshProUGUI _indexText;
    [SerializeField] private ParticleSystem[] _particleSystemsFeedback;
    [SerializeField] private RectTransform _edgeRectTransform;
    public int Index => _index;
    private int _index;

    private float _circleDuration = 2.0f;

    private bool _missed = false;

    private QTEManager _manager;
    
    public void InitCircle(int index, QTEManager manager,float duration)
    {
        _index = index;
        _circleDuration = duration;
        _indexText.text = (index + 1).ToString();
        _manager = manager;
        StartCoroutine(ShrinkBorder());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var cam = Camera.main;

        StopAllCoroutines();
        //Trigger action 
        if (_manager.IsNextIndex(_index) || !_missed)
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

        _manager.RemoveCircle(this);
        Destroy(this.gameObject);
    }

    private IEnumerator ShrinkBorder()
    {
        float startTime = Time.time;
        var maxScale = _edgeRectTransform.localScale * 4;
        var minScale = _edgeRectTransform.localScale;
        float duration = 0.0f;
        _edgeRectTransform.localScale = maxScale;
        while (Time.time < startTime + _circleDuration)
        {
            duration += Time.deltaTime;
            _edgeRectTransform.localScale = Vector3.Lerp(maxScale, minScale, duration);
            yield return null;
        }

        yield return null;
        _missed = true;
    }
}
