using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIDivideGauge : MonoBehaviour
{
    public UIGauge[] gauges;

    public UIBaseText currentInfoText;

    public void UpdateGauge(int left, int right, int max)
    {
        gauges[0].UpdateFillAmount(left, max);
        gauges[1].UpdateFillAmount(right, max);
        currentInfoText?.SetText(string.Format("{0}:{1}", left, right));
    }

    public void UpdateGauge(float left, float right, float max)
    {
        gauges[0].UpdateFillAmount(left, max);
        gauges[1].UpdateFillAmount(right, max);
        currentInfoText?.SetText(string.Format("{0}:{1}", left, right));
    }
}
