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
    [SerializeField] public TextMeshProUGUI gemsToGainText;
    [SerializeField] public TextMeshProUGUI coinsToGainText;
    [SerializeField] public TextMeshProUGUI timeLimitText;
    [SerializeField] private TextMeshProUGUI playerGemsText;
    [SerializeField] private TextMeshProUGUI playerCoinText;
    [SerializeField] private TextMeshProUGUI buyWithGemsText;          
    private ChestView currentChestView;
    
    private void Start()
    {
        SetButtons();                      
        SetPlayerUI();
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
        GameService.Instance.ChestEnablerScript.DisableLockedChests();
        currentChestView.ChestController.OnOpenForFree();        
        OnChestTabClose();
        GameService.Instance.SetTimerStatus(true);
    }
    public void SetCurrentChestView(ChestView chestView)
    {
        currentChestView = chestView;
        SetInstantBuyButton();
    }
    private void SetInstantBuyButton()
    {       
        openWithGemButton.onClick.RemoveAllListeners();
        openWithGemButton.onClick.AddListener(() => 
        {
            InstantBuyButton();
            openWithGemButton.onClick.RemoveAllListeners();
        });        
    }   
    public void SetPlayerUI()
    {
        playerGemsText.text = GameService.Instance.playerData.GetPlayerGems().ToString();
        playerCoinText.text = GameService.Instance.playerData.GetPlayerCoins().ToString();
    }   
    public void SetInstantBuyWithGemsText(int cost) => buyWithGemsText.text = "Buy Now For " + cost;   
    private void InstantBuyButton() => currentChestView.ChestController.InstantBuy();
    
}
