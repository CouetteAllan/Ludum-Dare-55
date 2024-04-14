using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Summoning",menuName = "Card/Summoning")]
public class SummoningSO : CardSO
{
    [Header("Base Summoning Stat")]
    public float Damage = 10.0f;

    [Header("Spells")]
    public SpellSO[] Spells;
}
