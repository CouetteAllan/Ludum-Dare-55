using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningDisplayHealth : MonoBehaviour
{
    private SpriteRenderer[] _spriteHealth = new SpriteRenderer[5];

    private void Awake()
    {
        _spriteHealth = GetComponentsInChildren<SpriteRenderer>();
    }

    private void InitHealth(Summoning summoning)
    {
        foreach (var sprite in _spriteHealth)
        {
            sprite.color = Color.yellow;
        }

        
    }

    private void OnDestroy()
    {
        
    }
}
