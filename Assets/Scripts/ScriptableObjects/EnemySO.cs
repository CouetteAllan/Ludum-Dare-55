using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType //Dirty but it's because we have a small game
{
    Frankie,
    Noor,
    Vadim
}

[CreateAssetMenu(fileName = "New Enemy",menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string EnemyFullName;
    public EnemyType EnemyName;
    public Sprite EnemyImage;
    [Range(0.1f,0.9f)] public float StartDominationInPercent = .5f;
    [Range(0.0f, 1.0f)] public float BaseDamage = 0.2f;

    [Header("Affinities against Summonings")]
    [Range(-1.0f, 1.0f)] public float BonusDamageAgainstLion = 0.0f;
    [Range(-1.0f, 1.0f)] public float BonusDamageAgainstDeer = 0.0f;
    [Range(-1.0f, 1.0f)] public float BonusDamageAgainstRedPanda = 0.0f;

    public string[] AttackNames = new string[2];

    [TextArea(3, 10)] public string PreEncounter;
    [TextArea(3, 10)] public string PostEncounter;
}
