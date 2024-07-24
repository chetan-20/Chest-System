using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ChestView : MonoBehaviour
{
    [SerializeField] private ChestView chestViewPrefab;
    public TextMeshProUGUI chestTypeText;
    public Image chestImage;
    public TextMeshProUGUI chestStatusText;   
    public Button unlockAfterTimerButton;
    public TextMeshProUGUI unlockAfterTimerText;    
    public Button undoChestButton;
    public Button chestDetailButton;
    private Transform parentTransform;
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
        chestDetailButton.onClick.AddListener(chestController.ChestClicked);
        undoChestButton.onClick.AddListener(chestController.UndoChestState);       
    }
    private void DisableButtons()
    {
        chestController.EnableBuyButtonOnChest(false);
        chestController.EnableUndoButton(false);
    }
    public Transform GetParentTransform()
    {
        return parentTransform;
    }
}
