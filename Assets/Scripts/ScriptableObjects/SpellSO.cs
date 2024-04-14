using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell",menuName = "Card/Spell")]
public class SpellSO : CardSO
{
    [Header("Affinites with enemies")]
    public Affinity FrankieAffinity = Affinity.Weak;
    public Affinity NoorAffinity = Affinity.Weak;
    public Affinity VadimAffinity = Affinity.Weak;
}
