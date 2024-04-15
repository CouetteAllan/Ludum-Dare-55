using System;
using TMPro;

public static class EnemyManagerDataHandler 
{
    public static event Action<EnemySO> OnInitEnemy;
    public static event Action<EnemySO> OnEnemyAttack;
    public static event Action<TextMeshProUGUI> OnRegisterEncounterText;
    public static void InitEnemy(EnemySO enemy) => OnInitEnemy?.Invoke(enemy);
    public static void EnemyAttack(EnemySO enemy) => OnEnemyAttack?.Invoke(enemy);
    public static void RegisterEncounterText(TextMeshProUGUI textRef) => OnRegisterEncounterText?.Invoke(textRef);
}
