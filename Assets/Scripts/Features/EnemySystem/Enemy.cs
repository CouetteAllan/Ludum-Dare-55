using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    private EnemySO _datas;
    private EnemyManager _manager;

    public void Init(EnemySO datas,EnemyManager manager)
    {
        _datas = datas;
        _sprite.sprite = datas.EnemyImage;
        EnemyManagerDataHandler.InitEnemy(datas);
        _manager = manager;
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
}
