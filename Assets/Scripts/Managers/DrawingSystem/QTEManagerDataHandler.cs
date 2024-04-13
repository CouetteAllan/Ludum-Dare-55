using System;

public static class QTEManagerDataHandler
{
    public static event Action OnStartSpawnPattern;
    public static event Action<PrecisionState> OnCircleClicked;
    public static event Action OnPatternFinished;
    public static event Action<Score> OnSendScore;
    public static void StartSpawnPattern(this QTEManager manager) => OnStartSpawnPattern?.Invoke();
    public static void CircleClicked(PrecisionState precision) => OnCircleClicked?.Invoke(precision);
    public static void PatternFinished() => OnPatternFinished?.Invoke();
    public static void SendScore(Score score) => OnSendScore?.Invoke(score);
}
