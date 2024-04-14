using System;

public static class DominationManagerDataHandler 
{
    public static event Action<int> OnUpdateDominationBar;
    public static void UpdateDominationBar(int candleChangeNb) => OnUpdateDominationBar?.Invoke(candleChangeNb);
}
