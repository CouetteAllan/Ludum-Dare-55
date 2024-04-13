using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{


    void Awake()
    {
        QTEManagerDataHandler.OnCircleClicked += OnCircleClicked;
    }

    private void OnCircleClicked(PrecisionState precision)
    {
        Debug.Log(precision.ToString());
    }

    private void OnDisable()
    {
        QTEManagerDataHandler.OnCircleClicked -= OnCircleClicked;
    }
}
