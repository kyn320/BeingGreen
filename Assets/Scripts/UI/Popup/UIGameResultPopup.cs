using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIGameResultPopup : UIBasePopup
{
    public UIBaseText[] scoreTexts;
    public UIBaseText resultText;

    public override void Init(UIData uiData)
    {
        base.Init(uiData);
        var resultData = uiData as UIGameResultPopupData;
        for (var i = 0; i < resultData.scores.Length; ++i)
        {
            scoreTexts[i].SetText(resultData.scores[i].ToString());
        }

        var resultDescription = "";

        switch (resultData.winner)
        {
            case 0:
            case 1:
                resultDescription = string.Format("Player {0} Win", resultData.winner + 1);
                break;
            case 2:
                resultDescription = string.Format("Draw");
                break;
        }

        resultText.SetText(resultDescription);
    }


    public void EnterTitle()
    {
        SceneController.Instance.LoadScene("Title");
    }


}
