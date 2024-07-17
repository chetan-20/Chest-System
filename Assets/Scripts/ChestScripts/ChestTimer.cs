using UnityEngine;
public class ChestTimer 
{
    private ChestDataSO chestData;
    private ChestStateMachine chestStateMachine;
    private ChestView chestView;
    
    public ChestTimer(ChestDataSO chestData, ChestStateMachine chestStateMachine,ChestView chestView)
    {
        this.chestData = chestData;
        this.chestStateMachine = chestStateMachine;
        this.chestView = chestView;
    }    
    public void StartTimer()
    {
        float elapseTime = Time.time - chestData.startTime;
        chestData.currentTimeInSeconds = chestData.timerInMinutes * 60;
        if (chestData.currentTimeInSeconds > 0)
        {
            chestData.currentTimeInSeconds -= elapseTime;
            UpdateTimerText();
            SetBuyButtonTextOnChest(chestData.currentTimeInSeconds);
        }
        if (chestData.currentTimeInSeconds <= 0)
        {
            chestStateMachine.ChangeState(chestStateMachine.unlockNotCollectedState);
        }
    }
    private void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(chestData.currentTimeInSeconds / 3600);
        int remainingSeconds = Mathf.FloorToInt(chestData.currentTimeInSeconds % 3600);
        int minutes = Mathf.FloorToInt(remainingSeconds / 60);
        int seconds = Mathf.FloorToInt(remainingSeconds % 60);

        if (hours > 0)
        {
            chestView.chestStatusText.text = hours + " : " + minutes.ToString("00") + " : " + seconds.ToString("00");
        }
        else
        {
            chestView.chestStatusText.text = minutes + " : " + seconds.ToString("00");
        }
    }
    private void SetBuyButtonTextOnChest(float remainingTime) => chestView.unlockAfterTimerText.text = "OPEN NOW " + new ChestValueCalculator(chestData).GetOpeningWithGemCost(remainingTime);
}
