using TMPro;
using UnityEngine;
public class ChestTimer 
{
    //private ChestController chestController;    
    public float currentTimeInSeconds { get; private set; }
    private float startTime;   
    public void StartTimer(TextMeshProUGUI chestStatusText,TextMeshProUGUI unlockOnChestText,int timerInMinutes)
    {        
        float elapseTime = Time.time - startTime;
        currentTimeInSeconds = timerInMinutes * 60;
        if (currentTimeInSeconds > 0)
        {
           currentTimeInSeconds -= elapseTime;
            UpdateTimerText(chestStatusText);
            SetBuyButtonTextOnChest(currentTimeInSeconds,unlockOnChestText);
        }        
    }
    public void SetStartTime()
    {
        startTime = Time.time;
    }
    private void UpdateTimerText(TextMeshProUGUI text)
    {
        int hours = Mathf.FloorToInt(currentTimeInSeconds / 3600);
        int remainingSeconds = Mathf.FloorToInt(currentTimeInSeconds % 3600);
        int minutes = Mathf.FloorToInt(remainingSeconds / 60);
        int seconds = Mathf.FloorToInt(remainingSeconds % 60);

        if (hours > 0)
        {
           text.text = hours + " : " + minutes.ToString("00") + " : " + seconds.ToString("00");
        }
        else
        {
           text.text = minutes + " : " + seconds.ToString("00");
        }
    }
    private void SetBuyButtonTextOnChest(float remainingTime,TextMeshProUGUI text) => text.text = "OPEN NOW " + new ChestValueCalculator().GetOpeningWithGemCost(remainingTime);
}
