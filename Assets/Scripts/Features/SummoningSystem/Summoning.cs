using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private bool _isAlly;
    [SerializeField] private Animator _animator;

    private SummoningManager _manager;
    private SummoningSO _datas;
    private Action _callBackOnFinishAnim;
    private SpellSO _lastRegisteredSpell;
    public void Init(SummoningManager manager, SummoningSO datas)
    {
        _manager = manager;
        _datas = datas;
        _sprite.sprite = datas.CardImage;
        SummoningManagerDataHandler.OnAllySummoningAttack += OnAllySummoningAttack;
    }

    public void ChangeSummonning()
    {
        SummoningManagerDataHandler.OnAllySummoningAttack -= OnAllySummoningAttack;
        this.gameObject.SetActive(false);
    }

    private void OnAllySummoningAttack(Score finalPatternScore,SpellSO lastRegisteredSpell, Action callback)
    {
        _callBackOnFinishAnim = callback;
        _lastRegisteredSpell = lastRegisteredSpell;
        //Do anim attack
        _animator.SetTrigger("Attack");
        //Do damage and change progress bar
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
