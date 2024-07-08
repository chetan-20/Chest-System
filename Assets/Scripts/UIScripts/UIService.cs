
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
        currentChestView.chestController.OnOpenForFree();
        DisableAllChests();
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
}
