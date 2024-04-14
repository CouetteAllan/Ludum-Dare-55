using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    private EnemySO _datas;

    public void Init(EnemySO datas)
    {
        _datas = datas;
        _sprite.sprite = datas.EnemyImage;
        EnemyManagerDataHandler.InitEnemy(datas);
    }

    public void EnemyAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public void DealDamage()
    {
        DominationManagerDataHandler.UpdateDominationBar(-.1f);
    }

    public void EndEnemyAttack()
    {
        TurnBasedManager.Instance.ChangePhase(CombatPhase.AllyAttack);
    }
}
