using System.Collections;
using TMPro;
using UnityEngine;
public class PopUpService : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private TextMeshProUGUI popUpMsgText;
    private float popUpDelay = 3f;
    private void Start()
    {
        popUpPanel.SetActive(false);
    }
    public void DisplayPopUp(string msg)
    {
        popUpPanel.SetActive(true);
        popUpMsgText.text = msg;       
        StartCoroutine(DisableAfterDealy());      
    }
    private IEnumerator DisableAfterDealy()
    {              
        yield return new WaitForSeconds(popUpDelay);
        popUpPanel.SetActive(false);        
    }
}
