using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemySO[] _enemies;
    [SerializeField] private Enemy _enemy;
    private int _indexEncounter = 0;
    private void Awake()
    {
        TurnBasedManager.OnChangePhase += TurnBasedManager_OnChangePhase;
    }

    private void TurnBasedManager_OnChangePhase(CombatPhase newPhase)
    {
        switch (newPhase)
        {
            case CombatPhase.Encounter:
                _enemy.gameObject.SetActive(true);
                _enemy.transform.position = Vector3.zero;
                _enemy.Init(_enemies[_indexEncounter],this);
                _indexEncounter++;
                break;
            case CombatPhase.PickSummoning:
                _enemy.transform.position = new Vector2(12, 0);

                break;
            case CombatPhase.AllyAttack:
                break;
            case CombatPhase.EnemyAttack:
                _enemy.EnemyAttack();
                break;
        }

    }

    private void OnDisable()
    {
        TurnBasedManager.OnChangePhase -= TurnBasedManager_OnChangePhase;
    }
}
