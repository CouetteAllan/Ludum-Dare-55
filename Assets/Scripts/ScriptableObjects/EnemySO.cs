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
    [Range(2,9)]public int StartDomination = 5;

    public string[] AttackNames = new string[2];
}
