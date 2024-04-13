using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _accuracyText;

    private Score _currentScore;
    void Awake()
    {
        QTEManagerDataHandler.OnCircleClicked += OnCircleClicked;
        QTEManagerDataHandler.OnStartSpawnPattern += OnStartSpawnPattern;
        QTEManagerDataHandler.OnPatternFinished += OnPatternFinished;
    }

    private void OnPatternFinished()
    {
        QTEManagerDataHandler.SendScore(_currentScore);
    }

    private void OnStartSpawnPattern()
    {
        _currentScore = new Score(100.0f,Score.Power.Empowered);
    }

    private void OnCircleClicked(PrecisionState precision)
    {
        //Update Accuracy
        _currentScore.CalculateAccuracy(GetAccuracyWithPrecision(precision));
        _accuracyText.text = "Accuracy: " +_currentScore.ToString();
    }

    private float GetAccuracyWithPrecision(PrecisionState precision)
    {
        switch (precision)
        {
            case PrecisionState.Perfect:
                return 105.0f;
            case PrecisionState.Good:
                return 95.0f;
            case PrecisionState.Missed:
                return 10.0f;

            default:
                return 0.0f;
        }
    }

    private void OnDisable()
    {
        QTEManagerDataHandler.OnCircleClicked -= OnCircleClicked;
        QTEManagerDataHandler.OnStartSpawnPattern -= OnStartSpawnPattern;
    }
}

public class Score
{
    public float accuracy;
    public List<float> circlesAccuracy = new List<float>();
    public enum Power
    {
        Weak,
        Normal,
        Empowered
    }
    public Power power;

    public Score(float accuracy,Power power)
    {
        this.accuracy = accuracy;
        this.power = power;
    }

    public void CalculateAccuracy(float nb)
    {
        circlesAccuracy.Add(nb);
        float sum = 0.0f;
        foreach (var accur in circlesAccuracy)
        {
            sum += accur;
        }
        accuracy = sum / circlesAccuracy.Count;
    }

    public override string ToString()
    {
        string accuracyText = Mathf.Clamp(accuracy, 0.0f, 100.0f).ToString();
        return $"{accuracyText}%";
    }
}
