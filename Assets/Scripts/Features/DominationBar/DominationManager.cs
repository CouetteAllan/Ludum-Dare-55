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
    private EnemySO currentEnemyData;
    private int _remainingCandles = 5;
    private int _evilCandles = 5;

    private void Awake()
    {
        EnemyManagerDataHandler.OnInitEnemy += OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar += OnUpdateDominationBar;
    }

    private void OnUpdateDominationBar(int candleChangeNb)
    {
        _remainingCandles += candleChangeNb;
        //Update visuals
        RefreshCandles(_remainingCandles);
    }

    private void OnInitEnemy(EnemySO currentEnemyData)
    {
        _remainingCandles = _maxCandle - currentEnemyData.StartDomination;
        _evilCandles = _maxCandle - _remainingCandles;

        RefreshCandles(_remainingCandles);

    }

    private void RefreshCandles(int remainingCandles)
    {
        foreach (var child in _dominationBarParent.GetComponentsInChildren<Image>())
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < remainingCandles; i++)
        {
            Image candleImage = Instantiate(_candleGO, _dominationBarParent);
            candleImage.sprite = _candleImages[0];
        }
        for (int i = 0; i < _maxCandle - remainingCandles; i++)
        {
            Image candleImage = Instantiate(_candleGO, _dominationBarParent);
            candleImage.sprite = _candleImages[1];
        }
    }


    private void OnDisable()
    {
        EnemyManagerDataHandler.OnInitEnemy -= OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar -= OnUpdateDominationBar;


    }
}
