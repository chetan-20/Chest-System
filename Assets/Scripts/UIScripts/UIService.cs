
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
    [SerializeField] private TextMeshProUGUI gemsToGainText;
    [SerializeField] private TextMeshProUGUI coinsToGainText;
    [SerializeField] private TextMeshProUGUI timeLimitText;
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
    }
    public void OnChestClick(ChestView chestView)
    {
        generateButtonObject.SetActive(false);
        ShowChestDataObject.SetActive(true);
        gemsToGainText.text = chestView.chestController.GetGemsRange();
        coinsToGainText.text = chestView.chestController.GetCoinsRange();
        timeLimitText.text = "Time To Open "+chestView.chestController.GetChestTimer()+" Mins";
    }
    public void OnChestTabClose()
    {
        ShowChestDataObject.SetActive(false);
        generateButtonObject.SetActive(true);
    }
}
