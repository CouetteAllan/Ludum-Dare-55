using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternUI : MonoBehaviour
{
    [Header("UI sub-components")]
    [SerializeField] private RadialBar _radialBar;
    [SerializeField] private PatternLineRenderer _patternLineRenderer;

    public void SetPattern(PatternSO pattern)
    {
        _patternLineRenderer.SetPattern(pattern);
    }

    public void SetAmount(float amount)
    {
        _radialBar.Amount = amount;
    }

}
