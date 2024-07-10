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
    [SerializeField] public GenerateChest genChest;
    private PlayerDataScript playerData;
    private ChestView currentChestView;
    private static UIService instance;
    public static UIService Instance{ get { return instance; } }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        SetButtons();
        playerData = new PlayerDataScript();
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
        DisableAllChests();
        currentChestView.chestController.OnOpenForFree();        
        OnChestTabClose();
    }
    public void SetCurrentChestView(ChestView chestView)
    {
        currentChestView = chestView;
    }
    private void DisableAllChests()
    {
        InputHandler[] inputHandlers = chestsMainPanel.GetComponentsInChildren<InputHandler>();
        foreach(InputHandler handler in inputHandlers)
        {
            handler.SetClickStatus(false);
        }
    }
    public void EnableAllChests()
    {
        InputHandler[] inputHandlers = chestsMainPanel.GetComponentsInChildren<InputHandler>();
        foreach (InputHandler handler in inputHandlers)
        {
            handler.SetClickStatus(true);
        }
    }
    private void SetPlayerUI()
    {
        playerGemsText.text = "" + playerData.playerGems;
        playerCoinText.text = "" + playerData.playerCoins;
    }
    public void UpdatePlayerCoinsAndGems(ChestController chestController)
    {
        playerData.playerCoins += chestController.GetRandomCoins();
        playerData.playerGems += chestController.GetRandomGems();
        SetPlayerUI();
    }
}
