using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private GameObject _pattern;
    [SerializeField] private Canvas _canvas;

    private List<CircleQTE> _circles = new List<CircleQTE>();


    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState newState)
    {
        if(newState == GameState.StartGame)
            SpawnPattern();
    }

    private void SpawnPattern()
    {
        int index = 0;
        foreach(CircleQTE circle in _pattern.GetComponentsInChildren<CircleQTE>())
        {
            _circles.Add(circle);
            circle.InitCircle(index,this);
            index++;
        }

        Instantiate(_pattern, _canvas.transform);
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
}
