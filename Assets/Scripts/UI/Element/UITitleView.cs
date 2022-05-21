using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITitleView : UIBaseView
{
    public void EnterGame() { 
        SceneController.Instance.LoadScene("InGame");
    }

    public void OpenOption() {
        UIController.Instance.OpenPopup("Option");
    }

    public void ExitGame() {
        Application.Quit();
    }

}
