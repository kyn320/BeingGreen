using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitle : UIBase
{
    public override void Init()
    {

    }

    public override void InitEvent()
    {
        UIButton["Btn_StartGame"].SetListener(this, () => UIView.UISceneFadeView.LoadScene("SampleScene_YN", UI.InGame, true, 1));
        UIButton["Btn_Option"].SetListener(this, () => UIManager.Instance.Open(UI.OptionPopUp));
        UIButton["Btn_QuitGame"].SetListener(this, () => Application.Quit());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
