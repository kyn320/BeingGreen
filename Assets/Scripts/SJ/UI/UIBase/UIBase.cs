using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIBase : MonoBehaviour
{
    private UI m_UI = UI.None;
    private UINavType m_NavType = UINavType.Normal;

    private CanvasGroup m_Cg = null;
    private RectTransform m_RectT = null;

    public List<UIAnimationData> openAnimationList;
    public List<UIAnimationData> closeAnimationList;

    private int currentAnimationPlayCount = 0;
    private Coroutine animationCoroutine;

    public UnityEvent openEvent;
    public UnityEvent closeEvent;

    #region Binding Field

    protected Dictionary<string, Text> UIText = new Dictionary<string, Text>();
    protected Dictionary<string, Button> UIButton = new Dictionary<string, Button>();
    protected Dictionary<string, Image> UIImage = new Dictionary<string, Image>();

    #endregion

    public RectTransform RectT { get => m_RectT ?? this.GetOrAddComponent<RectTransform>(ref m_RectT); }
    protected CanvasGroup Cg { get => m_Cg ?? this.GetOrAddComponent<CanvasGroup>(ref m_Cg); }
    public UINavType NavType { get => m_NavType; set => m_NavType = value; }
    public UI UI { get => m_UI; set => m_UI = value; }

    private void Awake()
    {
        Init();
        BindAll();
        InitEvent();
    }

    public abstract void Init();
    public abstract void InitEvent();

    public virtual void Open()
    {
        gameObject.SetActive(true);
        PlayAnimation(openAnimationList, EndOpen);
    }

    public virtual void EndOpen()
    {
        openEvent?.Invoke();
    }

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
                    tween = RectT.DOLocalMove(animationData.DestinationVector, animationData.Duration);
                    break;
                case UIAnimationType.Rotate:
                    tween = RectT.DOLocalRotate(animationData.DestinationVector, animationData.Duration);
                    break;
                case UIAnimationType.Scale:
                    tween = RectT.DOScale(Vector3.one * animationData.DestinationFloat, animationData.Duration);
                    break;
                case UIAnimationType.Color:
                    tween = Cg.DOFade(animationData.DestinationColor.a, animationData.Duration);
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

    #region Binding

    private void BindAll()
    {
        UIText.BindObject(this, "Text_");
        UIImage.BindObject(this, "Img_");
        UIButton.BindObject(this, "Btn_");
    }

    #endregion
}

