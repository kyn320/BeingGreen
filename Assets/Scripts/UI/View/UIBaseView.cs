using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIBaseView : MonoBehaviour
{
    [FoldoutGroup("View")]
    public string viewName = "";
    [FoldoutGroup("View")]
    protected UIType uiType;

    protected RectTransform rectTransform;
    protected CanvasGroup canvasGroup;

    [FoldoutGroup("View")]
    public List<UIAnimationData> openAnimationList;
    [FoldoutGroup("View")]
    public List<UIAnimationData> closeAnimationList;

    private int currentAnimationPlayCount = 0;
    private Coroutine animationCoroutine;

    [FoldoutGroup("View")]
    public UnityEvent openEvent;
    [FoldoutGroup("View")]
    public UnityEvent closeEvent;

    public virtual void Init(UIData uiData) {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    [Button("Open")]
    public virtual void Open()
    {
        gameObject.SetActive(true);
        PlayAnimation(openAnimationList, EndOpen);
    }

    public virtual void EndOpen()
    {
        openEvent?.Invoke();
    }

    [Button("Close")]
    public virtual void Close()
    {
        closeEvent?.Invoke();
        PlayAnimation(closeAnimationList, EndClose);
    }

    public virtual void EndClose()
    {
        gameObject.SetActive(false);
    }

    public virtual void PlayAnimation(List<UIAnimationData> animations, UnityAction completeEvent = null)
    {
        currentAnimationPlayCount = animations.Count;
        for (var i = 0; i < animations.Count; ++i)
        {
            var animationData = animations[i];
            Tween tween = null;
            switch (animationData.AnimationType)
            {
                case UIAnimationType.Move:
                    tween = transform.DOLocalMove(animationData.DestinationVector, animationData.Duration);
                    break;
                case UIAnimationType.Rotate:
                    tween = transform.DOLocalRotate(animationData.DestinationVector, animationData.Duration);
                    break;
                case UIAnimationType.Scale:
                    tween = transform.DOScale(Vector3.one * animationData.DestinationFloat, animationData.Duration);
                    break;
                case UIAnimationType.Color:
                    tween = GetComponent<Graphic>()?.DOColor(animationData.DestinationColor, animationData.Duration);
                    break;
            }

            if (animationData.LoopCount > 0)
            {
                tween.SetLoops(animationData.LoopCount, animationData.LoopType);
            }
            tween.SetDelay(animationData.Delay);
            tween.SetEase(animationData.EaseType);
            tween.OnComplete(() => { --currentAnimationPlayCount; });
            tween.SetRelative(animationData.IsRelative);
            tween.Play();
        }

        animationCoroutine = StartCoroutine("CoWaitCompleteAnimation", completeEvent);
    }

    private IEnumerator CoWaitCompleteAnimation(UnityAction completeEvent)
    {
        while (currentAnimationPlayCount > 0)
        {
            yield return null;
        }
        completeEvent?.Invoke();
        animationCoroutine = null;
    }

}
