using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private GameObject generateButtonObject;
    [SerializeField] private GameObject ShowChestDataObject;
    [SerializeField] private Button openWithGemButton;
    [SerializeField] private Button openForFreeButton;
    [SerializeField] private Button closeChestDataButton;
    [SerializeField] private GameObject chestsMainPanel;
    [SerializeField] public TextMeshProUGUI gemsToGainText;
    [SerializeField] public TextMeshProUGUI coinsToGainText;
    [SerializeField] public TextMeshProUGUI timeLimitText;
    [SerializeField] private TextMeshProUGUI playerGemsText;
    [SerializeField] private TextMeshProUGUI playerCoinText;
    [SerializeField] private TextMeshProUGUI buyWithGemsText;    
    public bool istimerActive;
    public PlayerDataScript playerData;
    private ChestView currentChestView;
    
    private void Start()
    {
        SetButtons();
        playerData = new PlayerDataScript();
        SetPlayerUI();
        istimerActive = false;
    }
    private void SetButtons()
    {
        closeChestDataButton.onClick.AddListener(OnChestTabClose);
        openForFreeButton.onClick.AddListener(OnOpenForFreeButtonClick);      
    }
    public void OnChestClick()
    {
        generateButtonObject.SetActive(false);
        ShowChestDataObject.SetActive(true);        
    }
    public void OnChestTabClose()
    {
        ShowChestDataObject.SetActive(false);
        generateButtonObject.SetActive(true);
    }
    public void OnOpenForFreeButtonClick()
    {
        DisableLockedChests();
        currentChestView.chestController.OnOpenForFree();        
        OnChestTabClose();
        istimerActive = true;
    }
    public void SetCurrentChestView(ChestView chestView)
    {
        currentChestView = chestView;
        if (currentChestView.chestController.currentChestState == ChestStates.LOCKED|| currentChestView.chestController.currentChestState == ChestStates.NOTCREATED)
        {
            openWithGemButton.onClick.RemoveAllListeners();
            openWithGemButton.onClick.AddListener(() => {
                SetInstantBuyButton();
                openWithGemButton.onClick.RemoveAllListeners(); 
            });
        }
    }
    private void DisableLockedChests()
    {       
        ChestView[] chestView = chestsMainPanel.GetComponentsInChildren<ChestView>();
        foreach (ChestView chest in chestView)
        {
            if (chest.chestController.currentChestState == ChestStates.NOTCREATED || chest.chestController.currentChestState == ChestStates.LOCKED)
            {
                chest.chestController.DisableClickingCurrentChest();
            }
            else
            {
                chest.chestController.EnableClickingCurrentChest();
            }
        }
    }
    public void EnableAllChests()
    {
        ChestView[] chestView = chestsMainPanel.GetComponentsInChildren<ChestView>();
        foreach (ChestView chest in chestView)
        {            
            chest.chestController.EnableClickingCurrentChest();           
        }
    }
    public void SetPlayerUI()
    {
        playerGemsText.text = "" + playerData.playerGems;
        playerCoinText.text = "" + playerData.playerCoins;
    }
    public void UpdatePlayerCoinsAndGems(ChestController chestController)
    {
        int randomCoins = chestController.GetRandomCoins();
        int randomGems = chestController.GetRandomGems();
        GameService.Instance.PopUpService.DisplayPopUp("+"+randomGems + " Gems"+" +"+randomCoins+" Coins");
        playerData.playerCoins += randomCoins;
        playerData.playerGems += randomGems;
        SetPlayerUI();
    }
    public void SetInstantBuyWithGemsText(int cost)
    {
        buyWithGemsText.text = "Buy Now For " + cost;
    }
    public void SetInstantBuyButton()
    {
        currentChestView.chestController.InstantBuy();
    }
}
