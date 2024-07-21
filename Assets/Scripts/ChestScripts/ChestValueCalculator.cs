using UnityEngine;
public class ChestValueCalculator 
{
    private ChestController chestController;

    public ChestValueCalculator(ChestController chestController)
    {
        this.chestController = chestController;
    }
    public int GetOpeningWithGemCost(float remainingTime)
    {
        return Mathf.CeilToInt((remainingTime / 60) / 10);
    }
    public int GetInstantOpeningCost() => GetOpeningWithGemCost(chestController.chestData.timerInMinutes * 60);
    public int GetRandomCoins() => Random.Range(chestController.chestData.coinsMinRange, chestController.chestData.coinsMaxRange);
    public int GetRandomGems() => Random.Range(chestController.chestData.gemsMinRange, chestController.chestData.gemsMaxRange);
    public int GetOpeningCost(float remainingTime )=> Mathf.CeilToInt((remainingTime / 60) / 10);    
}
