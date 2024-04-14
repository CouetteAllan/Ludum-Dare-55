using System;

public static class DominationManagerDataHandler 
{
    public static event Action<float> OnUpdateDominationBar;
    public static void UpdateDominationBar(float candleChangeNb) => OnUpdateDominationBar?.Invoke(candleChangeNb);
}
