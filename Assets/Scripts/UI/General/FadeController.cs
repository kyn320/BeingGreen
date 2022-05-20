using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


public class FadeController : Singleton<FadeController>
{



    public DOTweenAnimation fadeInAnimation;
    public DOTweenAnimation fadeOutAnimation;

    public void FadeIn(UnityAction fadeEndAction = null) {
        fadeInAnimation.DORestartById("FadeIn");
        fadeInAnimation.onComplete.AddListener(fadeEndAction);
    }

    public void FadeOut(UnityAction fadeEndAction = null)
    {
        fadeInAnimation.DORestartById("FadeOut");
        fadeOutAnimation.onComplete.AddListener(fadeEndAction);
    }

}
