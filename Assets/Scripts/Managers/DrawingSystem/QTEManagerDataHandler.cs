using System;

public static class QTEManagerDataHandler
{
    public static event Action OnStartSpawnPattern;
    public static event Action<PrecisionState> OnCircleClicked;
    public static void StartSpawnPattern(this QTEManager manager) => OnStartSpawnPattern?.Invoke();
    public static void CircleClicked(this CircleQTE circle, PrecisionState precision) => OnCircleClicked?.Invoke(precision);
}
