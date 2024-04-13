using System;

public static class QTEManagerDataHandler
{
    public static Action OnSpawnCircle;
    public static void SpawnCircle() => OnSpawnCircle?.Invoke();
}
