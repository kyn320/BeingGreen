using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;


public class UISceneFade : UIBaseView
{
    [FoldoutGroup("Fade")]
    public List<UIAnimationData> fadeInAnimationList;
    [FoldoutGroup("Fade")]
    public List<UIAnimationData> fadeOutAnimationList;

    public void FadeIn(UnityAction fadeEndAction = null)
    {
        PlayAnimation(fadeInAnimationList, fadeEndAction);
    }

    public void FadeOut(UnityAction fadeEndAction = null)
    {
        PlayAnimation(fadeOutAnimationList, fadeEndAction);
    }

}
