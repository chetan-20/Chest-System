using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public ChestView chestViewPrefab;
    public TextMeshProUGUI chestTypeText;
    public Image chestImage;
    public TextMeshProUGUI chestStatusText;
    private ChestController chestController;

    public void SetViewController(ChestController chestController)
    {
        this.chestController = chestController;
    }
    private void Start()
    {
        chestController.SetChest();
    }
}
