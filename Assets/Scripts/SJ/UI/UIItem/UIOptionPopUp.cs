using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOptionPopUp : UIBase
{
    public override void Init()
    {
        NavType = UINavType.PopUp;
    }

    public override void InitEvent()
    {
        UIButton["Btn_Close"].SetListener(this, () => UIManager.Instance.Close(this.UI));
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
