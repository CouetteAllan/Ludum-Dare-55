using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystemFX _particleSystemFX;
    [SerializeField] private ParticleSystem _spawnParticleSystemFX;
    [SerializeField] private ParticleSystem _attackFX;
    [SerializeField] private ParticleSystem _chargeAttackFX;

    private SummoningManager _manager;
    private SummoningSO _datas;
    private Action _callBackOnFinishAnim;
    private SpellSO _lastRegisteredSpell;
    private Score _lastRegisteredScore;
    private SummoningSO.BattleResult _battleResults;
    public void Init(SummoningManager manager, SummoningSO datas)
    {
        _manager = manager;
        _sprite.sprite = datas.CardImage;
        _sprite.color = new Color(1, 1, 1, 0);
        _datas = datas;
        SummoningManagerDataHandler.OnAllySummoningAttack += OnAllySummoningAttack;
        _particleSystemFX.InitAndPlayParticle(() => 
        {
            _sprite.color = new Color(1, 1, 1, 1);
            _spawnParticleSystemFX.Play();
            _manager.AttackPhase(2.0f);
            SummoningManagerDataHandler.AllySummoningSpawn(_datas);
        });
        if(_battleResults == null)
            _battleResults = _datas.GetResults();

        EnemyManagerDataHandler.OnEnemyAttack += OnEnemyAttack;
    }

    private void OnEnemyAttack(EnemySO enemyDatas)
    {
        float baseDamage = enemyDatas.BaseDamage;
        float additionnalDamage = 0.0f;
        //Take damage depending on the enemy type

        switch (_datas.type)
        {
            case SummoningSO.SummoningType.Lion:
                additionnalDamage = enemyDatas.BonusDamageAgainstLion;
                break;
            case SummoningSO.SummoningType.Deer:
                additionnalDamage = enemyDatas.BonusDamageAgainstDeer;
                break;
            case SummoningSO.SummoningType.RedPanda:
                additionnalDamage = enemyDatas.BonusDamageAgainstRedPanda;
                break;
        }

        float finalDamage = baseDamage + additionnalDamage;
        _battleResults.RemainingHealth -= finalDamage;
        //Play feedback depending on the damage taken
        //Check if summoning dies
        if(_battleResults.RemainingHealth <= 0)
        {
            SummoningManagerDataHandler.AllySummoningDies();
        }
    }


    public void ChangeSummonning()
    {
        SummoningManagerDataHandler.OnAllySummoningAttack -= OnAllySummoningAttack;
        EnemyManagerDataHandler.OnEnemyAttack -= OnEnemyAttack;
        _datas = null;
        _battleResults = null;
        this.gameObject.SetActive(false);
    }

    private void OnAllySummoningAttack(Score finalPatternScore,SpellSO lastRegisteredSpell, Action callback)
    {
        _callBackOnFinishAnim = callback;
        _lastRegisteredSpell = lastRegisteredSpell;
        _lastRegisteredScore = finalPatternScore;
        //Do anim attack
        Debug.Log("ally attacks");
        _animator.SetTrigger("Attack");
        //Do damage and change progress bar
        
    }

    public void DealDamage()
    {
        SummoningManagerDataHandler.AllySummoningDealDamage(_datas, _lastRegisteredScore, _lastRegisteredSpell);
        _attackFX.Play();
    }

    public void FinishedAnime()
    {
        _callBackOnFinishAnim?.Invoke();
    }

    public void DebutAnim()
    {
        _chargeAttackFX.Play();
    }

    public void OnDisable()
    {
        SummoningManagerDataHandler.OnAllySummoningAttack -= OnAllySummoningAttack;
        EnemyManagerDataHandler.OnEnemyAttack -= OnEnemyAttack;


    }
}
