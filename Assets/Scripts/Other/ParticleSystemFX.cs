using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemFX : MonoBehaviour
{

    private Action _callBackMethodOnFinished;

    public void InitAndPlayParticle(Action onFinishiedParticleSystem = null)
    {
        _callBackMethodOnFinished = onFinishiedParticleSystem;
        this.gameObject.GetComponent<ParticleSystem>().Play();
    }

    private void OnParticleSystemStopped()
    {
        _callBackMethodOnFinished?.Invoke();
    }
}
