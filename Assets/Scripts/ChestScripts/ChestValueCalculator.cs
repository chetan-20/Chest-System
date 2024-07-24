using UnityEngine;
public class ChestValueCalculator 
{    
    public int GetOpeningWithGemCost(float remainingTime)
    {
        return Mathf.CeilToInt((remainingTime / 60) / 10);
    }
    public int GetInstantOpeningCost(int timerInMinutes) => GetOpeningWithGemCost(timerInMinutes * 60);
    public int GetRandomCoins(int coinsMinRange,int coinsMaxRange) => Random.Range(coinsMinRange, coinsMaxRange);
    public int GetRandomGems(int gemsMinRange, int gemsMaxRange) => Random.Range(gemsMinRange, gemsMaxRange);
    public int GetOpeningCost(float remainingTime )=> Mathf.CeilToInt((remainingTime / 60) / 10);    
}
