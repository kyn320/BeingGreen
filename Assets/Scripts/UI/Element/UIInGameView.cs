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
        if (GameRuleController.Instance.isPlay)
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
    }

    public void UpdatePlayTime(float currentTime, float maxTime)
    {
        timerText?.SetText(TimeSpan.FromSeconds(currentTime).ToString(@"mm\:ss"));
    }
}
