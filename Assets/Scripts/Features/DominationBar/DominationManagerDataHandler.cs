using System;

public static class DominationManagerDataHandler 
{
    public static event Action<float> OnUpdateDominationBar;
    public static event Action<bool> OnDominationComplete;
    public static void UpdateDominationBar(float candleChangeNb) => OnUpdateDominationBar?.Invoke(candleChangeNb);
    public static void DominationComplete(bool allyVictory) => OnDominationComplete?.Invoke(allyVictory);
}
