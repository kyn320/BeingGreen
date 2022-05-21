using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayHelpPopup : UIBasePopup
{
    public List<PlayHelpData> helpDataList;

    public UIBaseImage screenShotImage;
    public UIBaseText descriptionText;

    public GameObject[] nextButtons;

    private int currentViewIndex = 0;

    public override void Init(UIData uiData)
    {
        base.Init(uiData);
        NextPage(0);
    }

    public void NextPage(int dir)
    {
        currentViewIndex += dir;
        currentViewIndex = Mathf.Clamp(currentViewIndex, 0, helpDataList.Count - 1);

        screenShotImage.SetImage(helpDataList[currentViewIndex].screenShot);
        descriptionText.SetText(helpDataList[currentViewIndex].description);

        nextButtons[0].SetActive(currentViewIndex > 0);
        nextButtons[1].SetActive(currentViewIndex < helpDataList.Count - 1);
    }

}
