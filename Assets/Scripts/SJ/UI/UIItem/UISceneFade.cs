using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UISceneFade : UIBase
{
    public override void Init()
    {
        NavType = UINavType.Top;
    }

    public override void InitEvent()
    {

    }

    public override void Open()
    {
        base.Open();
        StartCoroutine(SceneFade());

    }

    private IEnumerator SceneFade()
    {
        Cg.alpha = 0;
        Cg.DOFade(1, UIView.UISceneFadeView.FadeTime);

        yield return new WaitUntil(() => Cg.alpha == 1);

        if (!UIView.UISceneFadeView.IsUIOpenOrClose)
        {
            UIManager.Instance.Close(UIView.UISceneFadeView.UI);
        }

        UIManager.Instance.CloseAll();

        if (UIView.UISceneFadeView.IsUIOpenOrClose)
        {
            UIManager.Instance.Open(UIView.UISceneFadeView.UI);
        }

        yield return StartCoroutine(UIView.UISceneFadeView.LoadScene());

        Cg.DOFade(0, UIView.UISceneFadeView.FadeTime);

        yield return new WaitUntil(() => Cg.alpha == 0);
        UIManager.Instance.Close(UI.SceneFade);
    }

}
