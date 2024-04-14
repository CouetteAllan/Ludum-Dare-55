using System;

public static class EnemyManagerDataHandler 
{
    public static event Action<EnemySO> OnInitEnemy;
    public static void InitEnemy(EnemySO enemy) => OnInitEnemy?.Invoke(enemy);
}
