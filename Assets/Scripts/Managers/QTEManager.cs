using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private GameObject _pattern;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private LineRenderer _line;

    private List<CircleQTE> _circles = new List<CircleQTE>();
    private int _previousIndex = -1;

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
        var patternSpawned = Instantiate(_pattern, _canvas.transform);
        foreach(CircleQTE circle in patternSpawned.GetComponentsInChildren<CircleQTE>())
        {
            _circles.Add(circle);
            circle.InitCircle(index,this);
            index++;
        }

    }

    public void StartDrawing(Vector2 firstPoint)
    {
        _line.positionCount = 1;
        _line.SetPosition(0,firstPoint);
    }

    public void AddPointToLine(Vector2 point)
    {
        _line.positionCount++;
        _line.SetPosition(_line.positionCount - 1, point);
    }

    public void SetPreviousIndex(int index)
    {
        _previousIndex = index;
    }

    public bool IsNextIndex(int currentIndex) => currentIndex - 1 == _previousIndex;

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }
}
