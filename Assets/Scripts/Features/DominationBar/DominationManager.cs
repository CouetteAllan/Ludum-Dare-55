using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominationManager : MonoBehaviour
{
    [SerializeField] private int _maxCandle = 10;
    private EnemySO currentEnemyData;
    private int _remainingCandles = 5;

    private void Awake()
    {
        EnemyManagerDataHandler.OnInitEnemy += OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar += OnUpdateDominationBar;
    }

    private void OnUpdateDominationBar(int candleChangeNb)
    {
        _remainingCandles += candleChangeNb;
        //Update visuals
    }

    private void OnInitEnemy(EnemySO currentEnemyData)
    {
        _remainingCandles = _maxCandle - currentEnemyData.StartDomination;
    }


    private void OnDisable()
    {
        EnemyManagerDataHandler.OnInitEnemy -= OnInitEnemy;
        DominationManagerDataHandler.OnUpdateDominationBar -= OnUpdateDominationBar;


    }
}
