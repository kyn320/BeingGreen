using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    [SerializeField]
    protected Image gaugeFillImage;

    public void UpdateFillAmount(float curent, float max)
    {
        gaugeFillImage.fillAmount = curent / max;
    }
}
