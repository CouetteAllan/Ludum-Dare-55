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

    private bool _hasBeenClicked = false;

    private QTEManager _manager;

    
    
    public void InitCircle(int index, QTEManager manager,float duration)
    {
        _hasBeenClicked = false;

        this.gameObject.SetActive(true);
        _index = index;
        _circleDuration = duration;
        _indexText.text = (index + 1).ToString();
        _manager = manager;
        StartCoroutine(ShrinkBorder());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_hasBeenClicked)
            return;
        var cam = Utils.MainCamera;

        _hasBeenClicked = true;
        _edgeRectTransform.gameObject.SetActive(false);
        
        if (!_manager.IsDrawing)
            _manager.StartDrawing(this.transform.position);
        else
            _manager.AddPointToLine(this.transform.position);

        _manager.SetPreviousIndex(_index);

        //_manager.RemoveCircle(this);
    }

    private IEnumerator ShrinkBorder()
    {
        float startTime = Time.time;
        var maxScale = _edgeRectTransform.localScale * 4;
        var minScale = _edgeRectTransform.localScale;
        float duration = 0.0f;
        _edgeRectTransform.gameObject.SetActive(true);
        _edgeRectTransform.localScale = maxScale;
        while (Time.time < startTime + _circleDuration)
        {
            if (_hasBeenClicked)
                break;
            duration = Mathf.Clamp(duration + Time.deltaTime, 0.0f, _circleDuration);
            _edgeRectTransform.localScale = Vector3.Lerp(maxScale, minScale, duration / _circleDuration);
            yield return null;
        }

        while (Time.time < startTime + _circleDuration * 2)
        {
            if (_hasBeenClicked)
                break;
            duration += Time.deltaTime;
            yield return null;
        }
        bool isRight = true;
        PrecisionState precision = EvaluatePrecision(_circleDuration - duration, out isRight);
        QTEManagerDataHandler.CircleClicked(precision);
        if (isRight)
            this.gameObject.SetActive(false);
        else
            LeanTween.moveX(gameObject, 3.0f, .5f).setEasePunch().setOnComplete(() => this.gameObject.SetActive(false));
    }

    private PrecisionState EvaluatePrecision(float clickTime, out bool isRight)
    {
        isRight = true;
        float absTime = Mathf.Abs(clickTime);
        absTime = absTime / _circleDuration;
        switch (absTime)
        {
            case < .05f:
                var perfectParticles = Instantiate(_particleSystemsFeedback[2], this.transform.position + Vector3.forward * -3.0f, Quaternion.identity);
                perfectParticles.Play();
                return PrecisionState.Perfect;
            case < .25f:
                var goodParticle = Instantiate(_particleSystemsFeedback[0], this.transform.position + Vector3.forward * -3.0f, Quaternion.identity);
                goodParticle.Play();
                return PrecisionState.Good;
            default:
                var missedParticles = Instantiate(_particleSystemsFeedback[1], this.transform.position + Vector3.forward * -3.0f, Quaternion.identity);
                missedParticles.Play();
                isRight = false;
                return PrecisionState.Missed;
        }
    }
}
