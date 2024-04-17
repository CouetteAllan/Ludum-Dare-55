using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesAnimsTween : MonoBehaviour
{
    [SerializeField] private CanvasGroup _textSlowingAppear;
    [SerializeField] private TextMeshProUGUI _enemyTitleText;
    private Enemy _enemyRef;
    private EnemySO enemyDatas;

    public void DisplayEnemies(Enemy enemy, EnemySO enemyDatas)
    {

        _textSlowingAppear.gameObject.SetActive(false);
        var sequence = LeanTween.sequence();
        sequence.append(LeanTween.alpha(enemy.gameObject, 0, 0.0f));
        sequence.append(LeanTween.alpha(enemy.gameObject, 1, 2.0f).setEaseOutBounce());

        sequence.append(delay: 1.0f);
        //Display Name
        sequence.append(() => {
            _textSlowingAppear.gameObject.SetActive(true);
            _enemyTitleText.text = enemyDatas.EnemyFullName;
        });
        sequence.append(LeanTween.alphaCanvas(_textSlowingAppear, 1.0f, .5f).setEaseOutSine());
        sequence.append(LeanTween.scaleX(_textSlowingAppear.gameObject, 1.0f, .5f).setEaseInOutCubic());
        sequence.append(LeanTween.LeanAlphaText(_enemyTitleText, 1, .7f));
        sequence.append(delay: 1.0f);
        sequence.append(() =>
        {
            TurnBasedManager.Instance.ChangePhase(CombatPhase.PickSummoning);
            _textSlowingAppear.gameObject.SetActive(false);
        });
    }
}
