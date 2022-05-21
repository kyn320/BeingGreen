using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGame : UIBase
{
    public override void Init()
    {

    }

    public override void InitEvent()
    {
        UIButton["Btn_Pause"].SetListener(this, GamePause);
    }

    public override void Open()
    {
        base.Open();

        GameRuleController.Instance.updatePlayDelaTimeEvent.AddListener(UpdatePlayTime);
    }

    private void GamePause()
    {
        if (GameRuleController.Instance.isPlay)
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
    }

    private void UpdatePlayTime(float time)
    {
        float currentTime = GameRuleController.Instance.CurrentPlayTime;
        UIText["Text_Timer"].text = TimeSpan.FromSeconds(currentTime).ToString(@"mm\:ss");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIView.UISceneFadeView.LoadScene("Title_SJ", UI.Title, true);
        }
    }
}
