using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Summoning",menuName = "Card/Summoning")]
public class SummoningSO : CardSO
{
    [Header("Base Summoning Stat")]
    [Range(1,10)] public int Health = 5;

    [Header("Spells")]
    public SpellSO[] Spells;
}
