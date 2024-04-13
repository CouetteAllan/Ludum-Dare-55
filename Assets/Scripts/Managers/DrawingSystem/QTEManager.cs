using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private PatternSO[] _patterns;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private LineRenderer _line;

    [Header("Circle Parameter")]
    [SerializeField] private float _circleDuration;
    [SerializeField] private float _circleInterval = 2.0f;

    private List<CircleQTE> _circles = new List<CircleQTE>();
    private int _previousIndex = -1;
    private bool _isDrawing = false;
    public bool IsDrawing => _isDrawing;
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
        this.StartSpawnPattern();
        var patternSpawned = Instantiate(_patterns[0].PatternPrefab, _canvas.transform);
        foreach(var circle in patternSpawned.GetComponentsInChildren<CircleQTE>())
        {
            circle.gameObject.SetActive(false);
        }

        StartCoroutine(SpawnCirclePattern(patternSpawned.GetComponentsInChildren<CircleQTE>(true)));

    }

    private IEnumerator SpawnCirclePattern(CircleQTE[] circles)
    {
        int index = 0;
        foreach (CircleQTE circle in circles)
        {
            circle.gameObject.SetActive(true);
            _circles.Add(circle);
            circle.InitCircle(index, this, _circleDuration);
            index++;
            yield return new WaitForSeconds(_circleInterval);
        }
    }

    public void RemoveCircle(CircleQTE circle)
    {
        _circles.Remove(circle);
    }

    public void StartDrawing(Vector2 firstPoint)
    {
        _line.positionCount = 1;
        _line.SetPosition(0,firstPoint);
        _isDrawing = true;
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
