using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private PatternSO _pattern;
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
        TurnBasedManager.OnChangePhase += TurnBasedManager_OnChangePhase;
        QTEManagerDataHandler.OnSendPatternAndStart += OnSendPatternAndStart;
    }

    private void TurnBasedManager_OnChangePhase(CombatPhase newPhase)
    {
        if(newPhase != CombatPhase.PickSummoning)
        {
            _canvas.gameObject.SetActive(false);
            _line.positionCount = 1;
            _line.gameObject.SetActive(false);
        }
    }

    private void OnSendPatternAndStart(PatternSO pattern)
    {
        _pattern = pattern;
        SpawnPattern();
    }

    private void GameManager_OnGameStateChanged(GameState newState)
    {
        /*if(newState == GameState.StartGame)
            SpawnPattern();*/
    }

    private void SpawnPattern()
    {
        _canvas.gameObject.SetActive(true);
        this.StartSpawnPattern();
        

        StartCoroutine(SpawnCirclePattern());

    }

    private IEnumerator SpawnCirclePattern()
    {
        int index = 0;
        for(int i = 0; i < _pattern.PatternPrefabs.Length; i++)
        {
            var patternSpawned = Instantiate(_pattern.PatternPrefabs[i], _canvas.transform);
            foreach (var circle in patternSpawned.GetComponentsInChildren<CircleQTE>())
            {
                circle.gameObject.SetActive(false);
            }
            foreach (CircleQTE circle in patternSpawned.GetComponentsInChildren<CircleQTE>(true))
            {
                circle.gameObject.SetActive(true);
                _circles.Add(circle);
                circle.InitCircle(index, this, _circleDuration);
                index++;
                yield return new WaitForSeconds(_circleInterval);
            }
            index = 0;
            yield return new WaitForSeconds(_circleInterval + _circleDuration);
            Destroy(patternSpawned.gameObject);
            _isDrawing = false;
        }

        QTEManagerDataHandler.PatternFinished();
    }

    public void RemoveCircle(CircleQTE circle)
    {
        _circles.Remove(circle);
    }

    public void StartDrawing(Vector2 firstPoint)
    {
        _line.gameObject.SetActive(true);
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
