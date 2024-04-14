using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternUI : MonoBehaviour
{
    [Range (0f,1f)]
    [SerializeField] private float _amount = 1f;
    [SerializeField] private PatternSO _pattern;

    [Header("UI sub-components")]
    [SerializeField] private RadialBar _radialBar;
    [SerializeField] private PatternLineRenderer _patternLineRenderer;

    private void Start()
    {
        _patternLineRenderer.SetPattern(_pattern);
    }

    private void Update()
    {
        _radialBar.Amount = _amount;
    }

}
