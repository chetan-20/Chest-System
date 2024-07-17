public class ChestUIUpdater 
{ 
    private ChestView chestView;
    private ChestDataSO chestData;
    private ChestValueCalculator chestValueCalculator;
    public ChestUIUpdater(ChestView chestView, ChestDataSO chestData, ChestValueCalculator chestValueCalculator)
    {
        this.chestView = chestView;
        this.chestData = chestData;
        this.chestValueCalculator = chestValueCalculator;
    }
    public void SetChestImage() => chestView.chestImage.sprite = chestData.chestSprite;
    public void SetChestRarity() => chestView.chestTypeText.text = chestData.chestType.ToString();   
    private void SetGemsToGainText() => GameService.Instance.UIService.SetGemsToGainText(GetGemsRange());
    private void SetCoinsToGainText() => GameService.Instance.UIService.SetCoinsToGainText(GetCoinsRange());
    private void SetTimeLimitText() => GameService.Instance.UIService.SetTimeLimitText("Time To Open " + chestData.timerInMinutes + " Mins");
    private void SetInstantBuyWithGemsText() => GameService.Instance.UIService.SetInstantBuyWithGemsText(chestValueCalculator.GetInstantOpeningCost());
    private string GetGemsRange() => $"{chestData.gemsMinRange}-{chestData.gemsMaxRange}";
    private string GetCoinsRange() => $"{chestData.coinsMinRange}-{chestData.coinsMaxRange}";
    public void SetChestStatusText(string text) => chestView.chestStatusText.text = text;   
    public void ShowChestData()
    {
        SetGemsToGainText();
        SetCoinsToGainText();
        SetTimeLimitText();
        SetInstantBuyWithGemsText();        
    }
}
