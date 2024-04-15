using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemySO[] _enemies;
    [SerializeField] private Enemy _enemy;
    private int _indexEncounter = 0;
    private TextMeshProUGUI _encounterText;
    private void Awake()
    {
        TurnBasedManager.OnChangePhase += TurnBasedManager_OnChangePhase;
        EnemyManagerDataHandler.OnRegisterEncounterText += OnRegisterEncounterText;
    }

    private void OnRegisterEncounterText(TextMeshProUGUI obj)
    {
        _encounterText = obj;
    }

    private void TurnBasedManager_OnChangePhase(CombatPhase newPhase)
    {
        switch (newPhase)
        {
            case CombatPhase.BeforeEncounter:
                if (_encounterText == null)
                    return;
                _encounterText.text = _enemies[_indexEncounter].PreEncounter;
                TextAnimFade();
                break;

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

            case CombatPhase.AfterEncounter:
                if (_encounterText == null)
                    return;
                _encounterText.text = _enemies[_indexEncounter].PostEncounter;
                TextAnimFade();
                break;
        }

    }

    private void TextAnimFade()
    {
        _encounterText.alpha = 0.0f;
        LeanTween.LeanAlphaText(_encounterText, 1.0f, 2.0f).setEaseInCirc();
    }

    private void OnDisable()
    {
        TurnBasedManager.OnChangePhase -= TurnBasedManager_OnChangePhase;
        EnemyManagerDataHandler.OnRegisterEncounterText -= OnRegisterEncounterText;

    }
}
