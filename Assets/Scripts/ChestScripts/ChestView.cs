using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ChestView : MonoBehaviour
{
    [SerializeField] public ChestView chestViewPrefab;
    [SerializeField] public TextMeshProUGUI chestTypeText;
    [SerializeField] public Image chestImage;
    [SerializeField] public TextMeshProUGUI chestStatusText;   
    [SerializeField] public Button unlockAfterTimerButton;
    [SerializeField] public TextMeshProUGUI unlockAfterTimerText;    
    [SerializeField] public Button undoChestButton;
    [SerializeField] public Button chestDetailButton;
    public Transform parentTransform;
    private ChestController chestController;
    public ChestController ChestController {  get { return chestController; } }
    
    public void SetViewController(ChestController chestController)
    {
        this.chestController = chestController;
    }
    private void Start()
    {              
        parentTransform = transform.parent;
        SetButon();
        DisableButtons();
    }
    private void Update()
    {        
        chestController.Update();        
    }
    private void SetButon()
    {
        unlockAfterTimerButton.onClick.AddListener(chestController.SetBuyButtonOnChest);
        chestDetailButton.onClick.AddListener(chestController.ChestClicked);
        undoChestButton.onClick.AddListener(chestController.UndoChestState);
    }
    private void DisableButtons()
    {
        chestController.EnableBuyButtonOnChest(false);
        chestController.EnableUndoButton(false);
    }
    
}
