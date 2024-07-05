using UnityEngine;

public class ChestController 
{
    private ChestDataSO chestData;
    private ChestView chestView;
    public ChestController(ChestDataSO chestDataSO,ChestView chestView)
    {
        this.chestData = chestDataSO;
        this.chestView = chestView;
        chestView.SetViewController(this);       
    }
    public void SetChest()
    {
        SetChestImage();
        SetChestRarity();
        SetChestStatus();
    }
    private void SetChestImage() => chestView.chestImage.sprite = chestData.chestSprite;
    private void SetChestRarity() => chestView.chestTypeText.text = chestData.chestType.ToString();
    private void SetChestStatus() => chestView.chestStatusText.text = ChestStates.LOCKED.ToString();
}
