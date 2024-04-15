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


    public enum SummoningType
    {
        Lion,
        Deer,
        RedPanda
    }

    public SummoningType type;
    public class BattleResult
    {
        public float RemainingHealth;

        public BattleResult(float remainingHealth)
        {
            RemainingHealth = remainingHealth;
        }
    }

    private BattleResult _results = null;

    public BattleResult GetResults()
    {
        if(_results == null )
            _results = new BattleResult(1.0f);
        return _results;
    }
}
