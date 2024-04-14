using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominationManager : MonoBehaviour
{
    [SerializeField] private int _maxCandle = 10;
    [SerializeField] private RectTransform _dominationBarParent;
    [SerializeField] private Image _candleGO;
    [SerializeField] private Sprite[] _candleImages = new Sprite[2];
    private EnemySO _currentEnemyData;
    private float _progressPercent = .5f;

    private List<Image> _candles = new List<Image>();

    private void Awake()
    {
        EnemyManagerDataHandler.OnInitEnemy += OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar += OnUpdateDominationBar;
    }

    private void OnUpdateDominationBar(float dominationPercentChange)
    {
        _progressPercent = Mathf.Clamp(_progressPercent + dominationPercentChange,0.0f,1.0f);
        if (_progressPercent <= 0.0f)
            DominationManagerDataHandler.DominationComplete(allyVictory: false);
        else if(_progressPercent >= 1.0f)
            DominationManagerDataHandler.DominationComplete(allyVictory: true);

        //Update visuals
        RefreshCandles(_progressPercent);
    }

    private void OnInitEnemy(EnemySO currentEnemyData)
    {
        _progressPercent = 1.0f - currentEnemyData.StartDominationInPercent;
        InitProgressBar();

    }

    private void InitProgressBar()
    {
        if(_candles.Count > 0)
        {
            foreach (var candle in _candles)
            {
                Destroy(candle.gameObject);
            }
            _candles.Clear();
        }

        Debug.Log("Init Progress");
        int remainingCandles = Mathf.FloorToInt(_progressPercent * _maxCandle);
        int evilCandles = _maxCandle - remainingCandles;

        for (int i = 0; i < remainingCandles; i++)
        {
            Image candle = Instantiate(_candleGO, _dominationBarParent);
            candle.sprite = _candleImages[0];
            _candles.Add(candle);
        }
        for (int i = 0; i < evilCandles; i++)
        {
            Image candle = Instantiate(_candleGO, _dominationBarParent);
            candle.sprite = _candleImages[1];
            _candles.Add(candle);
        }
    }

    private void RefreshCandles(float remainingCandlesInPercent)
    {
        int remainingCandles = Mathf.FloorToInt(remainingCandlesInPercent * _maxCandle);
        int evilCandles = _maxCandle - remainingCandles;

        for (int i = 0; i < _maxCandle; i++)
        {
            _candles[i].sprite = i < remainingCandles ? _candleImages[0] : _candleImages[1];
        }
    }


    private void OnDisable()
    {
        EnemyManagerDataHandler.OnInitEnemy -= OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar -= OnUpdateDominationBar;


    }
}
