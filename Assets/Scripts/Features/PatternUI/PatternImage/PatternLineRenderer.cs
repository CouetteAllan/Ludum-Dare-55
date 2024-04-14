using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Radishmouse;

public class PatternLineRenderer : MonoBehaviour
{
    [SerializeField] private PatternSO _pattern;

    [Range(0f,1f)]
    [SerializeField] private float _size = 1f;

    private void Awake()
    {
        lineRenderer = GetComponent<UILineRenderer>();
    }

    public void SetPattern(PatternSO pattern)
    {
        _pattern = pattern;
        DrawPattern();
    }

    private void DrawPattern()
    {
        GameObject first = _pattern.PatternPrefabs[0];

        lineRenderer.points = new Vector2[first.transform.childCount];

        for (int i = 0; i < lineRenderer.points.Length; i++)
        {
            Transform child = first.transform.GetChild(i);
            lineRenderer.points[i] = child.position * _size;
        }
    }

    private UILineRenderer lineRenderer;
}
