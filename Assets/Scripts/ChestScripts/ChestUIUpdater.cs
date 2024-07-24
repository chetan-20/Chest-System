public class ChestUIUpdater 
{ 
    private ChestView chestView;
    private ChestController chestController;   
    public ChestUIUpdater(ChestView chestView, ChestController chestController)
    {
        this.chestView = chestView;
        this.chestController = chestController;       
    }
    public void SetChestImage() => chestView.chestImage.sprite = chestController.chestData.chestSprite;
    public void SetChestRarity() => chestView.chestTypeText.text = chestController.chestData.chestType.ToString();   
    private void SetGemsToGainText() => GameService.Instance.UIService.SetGemsToGainText(GetGemsRange());
    private void SetCoinsToGainText() => GameService.Instance.UIService.SetCoinsToGainText(GetCoinsRange());
    private void SetTimeLimitText() => GameService.Instance.UIService.SetTimeLimitText("Time To Open " + chestController.chestData.timerInMinutes + " Mins");
    private void SetInstantBuyWithGemsText() => GameService.Instance.UIService.SetInstantBuyWithGemsText(chestController.GetOpeningCost());
    private string GetGemsRange() => $"{chestController.chestData.gemsMinRange}-{chestController.chestData.gemsMaxRange}";
    private string GetCoinsRange() => $"{chestController.chestData.coinsMinRange}-{chestController.chestData.coinsMaxRange}";
    public void SetChestStatusText(string text) => chestView.chestStatusText.text = text;   
    public void ShowChestData()
    {
        SetGemsToGainText();
        SetCoinsToGainText();
        SetTimeLimitText();
        SetInstantBuyWithGemsText();        
    }
}
