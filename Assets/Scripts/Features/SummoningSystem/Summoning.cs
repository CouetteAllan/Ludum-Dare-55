using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _isAlly;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystemFX _particleSystemFX;

    private SummoningManager _manager;
    private SummoningSO _datas;
    private Action _callBackOnFinishAnim;
    private SpellSO _lastRegisteredSpell;
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
            _manager.AttackPhase(2.0f);
        });
    }

    public void ChangeSummonning()
    {
        SummoningManagerDataHandler.OnAllySummoningAttack -= OnAllySummoningAttack;
        _datas = null;
        this.gameObject.SetActive(false);
    }

    private void OnAllySummoningAttack(Score finalPatternScore,SpellSO lastRegisteredSpell, Action callback)
    {
        _callBackOnFinishAnim = callback;
        _lastRegisteredSpell = lastRegisteredSpell;
        //Do anim attack
        _animator.SetTrigger("Attack");
        //Do damage and change progress bar

        Debug.Log("ally attacks");
        DominationManagerDataHandler.UpdateDominationBar(.4f);
    }

    public void FinishedAnime()
    {
        _callBackOnFinishAnim?.Invoke();
    }

    public void OnDisable()
    {
        SummoningManagerDataHandler.OnAllySummoningAttack -= OnAllySummoningAttack;

    }
}
