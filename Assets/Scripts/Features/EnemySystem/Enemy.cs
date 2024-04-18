using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private ParticleSystemFX _winParticles;
    private EnemySO _datas;
    private EnemyManager _manager;

    public void Init(EnemySO datas,EnemyManager manager)
    {
        UnSubscribeEvent();



        _datas = datas;
        _sprite.sprite = datas.EnemyImage;
        EnemyManagerDataHandler.InitEnemy(datas);
        _manager = manager;
        SummoningManagerDataHandler.OnAllySummoningDealDamage += OnAllySummoningDealDamage;
        DominationManagerDataHandler.OnDominationComplete += OnDominationComplete;
    }

    private void OnDominationComplete(bool allyWon)
    {
        if (allyWon)
        {
            LeanTween.alpha(gameObject, .5f, 4).setEaseInBounce();
            _winParticles.InitAndPlayParticle(() => LeanTween.alpha(gameObject, .0f, 1.0f).setEaseOutBounce().setOnComplete(() => TurnBasedManager.Instance.ChangePhase(CombatPhase.AfterEncounter)));
        }
    }

    private void OnAllySummoningDealDamage(SummoningSO summoningDatas, Score spellScore, SpellSO spellDatas)
    {
        float baseDamage = .1f;
        float bonusDamage = 0f;


        bonusDamage += Mathf.Lerp(0.0f, 0.2f, Mathf.InverseLerp(84.0f,100.0f,spellScore.accuracy));

        switch (_datas.EnemyName)
        {
            case EnemyType.Frankie:
                bonusDamage += spellDatas.GetBonusDamageWithAffinity(spellDatas.FrankieAffinity);
                break;
            case EnemyType.Noor:
                bonusDamage += spellDatas.GetBonusDamageWithAffinity(spellDatas.NoorAffinity);
                break;
            case EnemyType.Vadim:
                bonusDamage += spellDatas.GetBonusDamageWithAffinity(spellDatas.VadimAffinity);
                break;
        }
        float finalDamage = baseDamage + bonusDamage;
        Debug.Log("Final Damages are: " + finalDamage);
        DominationManagerDataHandler.UpdateDominationBar(finalDamage);
    }

    public void EnemyAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public void DealDamage()
    {
        DominationManagerDataHandler.UpdateDominationBar(-.1f);
        EnemyManagerDataHandler.EnemyAttack(_datas);
    }

    public void EndEnemyAttack()
    {
        TurnBasedManager.Instance.ChangePhase(CombatPhase.AllyAttack);
    }

    private void UnSubscribeEvent()
    {
        SummoningManagerDataHandler.OnAllySummoningDealDamage -= OnAllySummoningDealDamage;
        DominationManagerDataHandler.OnDominationComplete -= OnDominationComplete;
    }

}
