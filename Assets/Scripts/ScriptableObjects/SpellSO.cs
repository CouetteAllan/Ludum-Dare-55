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

    public float GetBonusDamageWithAffinity(Affinity affinity)
    {
        switch (affinity)
        {
            case Affinity.VeryWeak:
                return -0.2f;
            case Affinity.Weak:
                return -0.1f;
            case Affinity.Neutral:
                return 0.0f;
            case Affinity.Good:
                return 0.1f;
            case Affinity.Efficient:
                return 0.2f;
            default:
                return 0.0f;
        }
    }
}
