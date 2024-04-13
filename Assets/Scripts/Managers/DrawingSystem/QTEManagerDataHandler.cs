using System;

public static class QTEManagerDataHandler
{
    public static Action OnSpawnCircle;
    public static Action<PrecisionState> OnCircleClicked;
    public static void SpawnCircle() => OnSpawnCircle?.Invoke();
    public static void CircleClicked(this CircleQTE circle, PrecisionState precision) => OnCircleClicked?.Invoke(precision);
}
