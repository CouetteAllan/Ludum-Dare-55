using System;

public static class QTEManagerDataHandler
{
    public static event Action OnSpawnCircle;
    public static event Action<PrecisionState> OnCircleClicked;
    public static void SpawnCircle() => OnSpawnCircle?.Invoke();
    public static void CircleClicked(this CircleQTE circle, PrecisionState precision) => OnCircleClicked?.Invoke(precision);
}
