using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameView : UIBaseView
{
    public UIBaseText timerText;

    public void PauseGame()
    {
        UIController.Instance.OpenPopup("Pause");
        GameRuleController.Instance.PauseGame();
    }

    public void UpdatePlayTime(float currentTime, float maxTime)
    {
        timerText?.SetText(TimeSpan.FromSeconds(currentTime).ToString(@"mm\:ss"));
    }
}
