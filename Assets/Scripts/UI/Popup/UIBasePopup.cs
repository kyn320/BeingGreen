using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class UIBasePopup : UIBaseView
{

    [Button("Open")]
    public virtual new void Open()
    {
        base.Open();
    }

    [Button("Close")]
    public virtual new void Close() {
        UIController.Instance.ClosePopup(this);
    }

    public override void EndClose()
    {
        Destroy(this.gameObject);
    }

}
