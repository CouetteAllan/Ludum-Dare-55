using System;

public static class EnemyManagerDataHandler 
{
    public static event Action<EnemySO> OnInitEnemy;
    public static event Action<EnemySO> OnEnemyAttack;
    public static void InitEnemy(EnemySO enemy) => OnInitEnemy?.Invoke(enemy);
    public static void EnemyAttack(EnemySO enemy) => OnEnemyAttack?.Invoke(enemy);
}
