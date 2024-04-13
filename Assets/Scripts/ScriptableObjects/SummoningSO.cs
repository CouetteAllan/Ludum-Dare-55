using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Summoning",menuName = "Entity/Ally")]
public class SummoningSO : ScriptableObject
{
    public string SummoningName;
    public PatternSO Pattern;
    public Sprite SummoningImage;

    [Header("Summoning Stats")]
    public float MaxHealth;
    public float Damage;
}
