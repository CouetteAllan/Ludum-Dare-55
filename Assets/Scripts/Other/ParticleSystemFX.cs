using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemFX : MonoBehaviour
{

    private Action _callBackMethodOnFinished;

    public void Init(Action onFinishiedParticleSystem)
    {
        _callBackMethodOnFinished = onFinishiedParticleSystem;
    }

    private void OnParticleSystemStopped()
    {
        _callBackMethodOnFinished?.Invoke();
    }
}
