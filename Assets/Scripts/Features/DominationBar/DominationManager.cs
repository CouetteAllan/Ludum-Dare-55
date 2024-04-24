using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominationManager : MonoBehaviour
{
    [SerializeField] private int _maxCandle = 10;
    [SerializeField] private RectTransform _dominationBarParent;
    [SerializeField] private SingleCandleUI _candleGO;
    [SerializeField] private Sprite[] _candleImages = new Sprite[2];
    private EnemySO _currentEnemyData;
    private float _progressPercent = .5f;

    private List<SingleCandleUI> _candles = new List<SingleCandleUI>();

    private void Awake()
    {
        EnemyManagerDataHandler.OnInitEnemy += OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar += OnUpdateDominationBar;
        SummoningManagerDataHandler.OnAllySummoningDies += OnAllySummoningDies;
    }

    private void OnAllySummoningDies()
    {
        _progressPercent = Mathf.Clamp(_progressPercent -.2f, 0.0f, 1.0f);
        if (_progressPercent <= 0.0f)
            DominationManagerDataHandler.DominationComplete(allyVictory: false);

        RefreshCandles(_progressPercent, false);
    }

    private void OnUpdateDominationBar(float dominationPercentChange)
    {
        _progressPercent = Mathf.Clamp(_progressPercent + dominationPercentChange,0.0f,1.0f);
        if (_progressPercent <= 0.0f)
            DominationManagerDataHandler.DominationComplete(allyVictory: false);
        else if(_progressPercent >= 1.0f)
            DominationManagerDataHandler.DominationComplete(allyVictory: true);

        //Update visuals
        RefreshCandles(_progressPercent, dominationPercentChange >= 0);
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

        StartCoroutine(BounceCandlesInit(_progressPercent));
    }

    private void RefreshCandles(float remainingCandlesInPercent, bool positiveChanges)
    {
        StartCoroutine(BounceCandles(remainingCandlesInPercent,positiveChanges));
    }
    private IEnumerator BounceCandlesInit(float remainingCandlesInPercent)
    {

        int remainingCandles = Mathf.FloorToInt(remainingCandlesInPercent * _maxCandle);
        int evilCandles = _maxCandle - remainingCandles;

        for (int i = 0; i < _maxCandle; i++)
        {
            SingleCandleUI candle = Instantiate(_candleGO, _dominationBarParent);
            _candles.Add(candle);
        }
        yield return null;
        yield return null;

        int endLoop = -1;
        for (int i = 0; i < remainingCandles; i++)
        {
            _candles[i].ChangeSprite(_candleImages[0]);
            _candles[i].Bounce();
            yield return new WaitForSeconds((float)(1.2f / _maxCandle));
            endLoop = i;
        }


        for (int i = _maxCandle - 1; i > endLoop; i--)
        {
            _candles[i].ChangeSprite(_candleImages[1]);
            _candles[i].Bounce();
            yield return new WaitForSeconds((float)(1.2f / _maxCandle));
        }


    }

    private IEnumerator BounceCandles(float remainingCandlesInPercent, bool positiveChanges)
    {

        int remainingCandles = Mathf.FloorToInt(remainingCandlesInPercent * _maxCandle);
        Debug.Log(remainingCandles);
        int evilCandles = _maxCandle - remainingCandles;

        if (positiveChanges)
        {
            for (int i = 0; i < remainingCandles; i++)
            {
                _candles[i].ChangeSprite(_candleImages[0]);
                _candles[i].Bounce();
                yield return new WaitForSeconds((float)(.7f / _maxCandle));
            }
        }
        else
        {
            for (int i = _maxCandle - 1; i >= remainingCandles; i--)
            {
                _candles[i].ChangeSprite(_candleImages[1]);
                _candles[i].Bounce();
                yield return new WaitForSeconds((float)(.8f / _maxCandle));
            }
        }
        


        

    }


    private void OnDisable()
    {
        EnemyManagerDataHandler.OnInitEnemy -= OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar -= OnUpdateDominationBar;


    }
}
