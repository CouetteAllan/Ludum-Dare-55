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
    [Range(0.1f,0.9f)]public float StartDominationInPercent = .5f;

    public string[] AttackNames = new string[2];

    [TextArea(3, 10)] public string PreEncounter;
    [TextArea(3, 10)] public string PostEncounter;
}
