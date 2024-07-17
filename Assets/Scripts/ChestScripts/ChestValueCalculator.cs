using UnityEngine;
public class ChestValueCalculator 
{
    private ChestDataSO chestData;

    public ChestValueCalculator(ChestDataSO chestData)
    {
        this.chestData = chestData;
    }
    public int GetOpeningWithGemCost(float remainingTime)
    {
        return Mathf.CeilToInt((remainingTime / 60) / 10);
    }
    public int GetInstantOpeningCost() => GetOpeningWithGemCost(chestData.timerInMinutes * 60);
    public int GetRandomCoins() => Random.Range(chestData.coinsMinRange, chestData.coinsMaxRange);
    public int GetRandomGems() => Random.Range(chestData.gemsMinRange, chestData.gemsMaxRange);
    public int GetOpeningCost(float remainingTime )=> Mathf.CeilToInt((remainingTime / 60) / 10);    
}
